using System.Net.Http.Json;
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
    private readonly HttpClient _http;
    private readonly Uri _baseUri;
    private readonly Dictionary<string, List<StoreApp>> _cache = [];

    public AppCatalog(HttpClient http, string baseUrl)
    {
        _http = http;
        _baseUri = new Uri(baseUrl);
    }

    public async Task<StoreApp> GetAsync(string reference)
    {
        var parts = reference.Split(':', 2);
        if (parts.Length != 2 || !parts[0].EndsWith(".json"))
            throw new InvalidDataException($"Referencia inválida: {reference}");

        var file = parts[0];
        var code = parts[1];

        if (!_cache.TryGetValue(file, out var apps))
        {
            apps = await _http.GetFromJsonAsync<List<StoreApp>>(
                new Uri(_baseUri, file))
                ?? throw new InvalidDataException($"No se pudo leer {file}");

            _cache[file] = apps;
        }

        return apps.FirstOrDefault(a => a.Code == code)
            ?? throw new InvalidDataException($"No existe la app: {reference}");
    }
}