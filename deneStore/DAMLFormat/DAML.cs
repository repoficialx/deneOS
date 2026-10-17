using System.Collections.Concurrent;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

public sealed record DamlElement(
    string Name,
    Dictionary<string, string> Attributes,
    List<DamlElement> Children,
    string Text);

public sealed class DamlParser
{
    public List<DamlElement> Parse(string daml)
    {
        var settings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Prohibit,
            XmlResolver = null
        };

        using var reader = XmlReader.Create(
            new StringReader($"<DamlDocument>{daml}</DamlDocument>"), settings);

        var document = XDocument.Load(reader);
        return document.Root!.Elements().Select(ParseElement).ToList();
    }

    private static DamlElement ParseElement(XElement element) =>
        new(
            element.Name.LocalName,
            element.Attributes().ToDictionary(a => a.Name.LocalName, a => a.Value),
            element.Elements().Select(ParseElement).ToList(),
            string.Concat(element.Nodes().OfType<XText>().Select(t => t.Value)).Trim()
        );
}



public sealed class StoreApp
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public string DownloadUrl { get; set; } = "";
    public string Code { get; set; } = "";
}

public sealed class AppCatalog
{
    private static readonly Regex FilePattern = new(
        @"\A[a-zA-Z0-9_-]+\.json\z",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private static readonly Regex CodePattern = new(
        @"\A[a-zA-Z0-9_-]+\z",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private readonly HttpClient _http;
    private readonly Uri _baseUri;

    private readonly ConcurrentDictionary<
        string, IReadOnlyList<StoreApp>> _cache = new();

    private readonly ConcurrentDictionary<
        string, SemaphoreSlim> _fileLocks = new();

    public AppCatalog(HttpClient http, string baseUrl)
    {
        ArgumentNullException.ThrowIfNull(http);

        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp &&
             uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException(
                "La URL base debe ser una dirección HTTP o HTTPS absoluta.",
                nameof(baseUrl));
        }

        if (!string.IsNullOrEmpty(uri.Query) ||
            !string.IsNullOrEmpty(uri.Fragment))
        {
            throw new ArgumentException(
                "La URL base no debe contener query ni fragmento.",
                nameof(baseUrl));
        }

        var builder = new UriBuilder(uri);

        if (!builder.Path.EndsWith("/", StringComparison.Ordinal))
            builder.Path += "/";

        _http = http;
        _baseUri = builder.Uri;
    }

    public async Task<StoreApp> GetAsync(
        string reference,
        CancellationToken cancellationToken = default)
    {
        var (file, code) = ParseReference(reference);

        var apps = await GetFileAsync(file, cancellationToken);

        return apps.FirstOrDefault(app =>
                   string.Equals(
                       app.Code,
                       code,
                       StringComparison.OrdinalIgnoreCase))
               ?? throw new InvalidDataException(
                   $"No existe la app: {reference}");
    }

    private async Task<IReadOnlyList<StoreApp>> GetFileAsync(
        string file,
        CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(file, out var cached))
            return cached;

        var gate = _fileLocks.GetOrAdd(
            file,
            static _ => new SemaphoreSlim(1, 1));

        await gate.WaitAsync(cancellationToken);

        try
        {
            // Puede haberse cargado mientras esperábamos.
            if (_cache.TryGetValue(file, out cached))
                return cached;

            var apps = await _http.GetFromJsonAsync<List<StoreApp>>(
                new Uri(_baseUri, file),
                cancellationToken)
                ?? throw new InvalidDataException(
                    $"El catálogo {file} está vacío o contiene JSON nulo.");

            if (apps.Any(app =>
                    app is null ||
                    string.IsNullOrWhiteSpace(app.Code)))
            {
                throw new InvalidDataException(
                    $"El catálogo {file} contiene una app sin código válido.");
            }

            var duplicate = apps
                .GroupBy(
                    app => app.Code,
                    StringComparer.OrdinalIgnoreCase)
                .FirstOrDefault(group => group.Count() > 1);

            if (duplicate is not null)
            {
                throw new InvalidDataException(
                    $"Código duplicado en {file}: {duplicate.Key}");
            }

            _cache[file] = apps;
            return apps;
        }
        finally
        {
            gate.Release();
        }
    }

    private static (string File, string Code) ParseReference(
        string reference)
    {
        if (string.IsNullOrWhiteSpace(reference))
            throw new InvalidDataException("La referencia está vacía.");

        var parts = reference.Split(':', 2);

        if (parts.Length != 2 ||
            !FilePattern.IsMatch(parts[0]) ||
            !CodePattern.IsMatch(parts[1]))
        {
            throw new InvalidDataException(
                $"Referencia DAML inválida: {reference}");
        }

        return (parts[0], parts[1]);
    }

    public void ClearCache() => _cache.Clear();
}