using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Management;
using System.Globalization;

bool installed = false;

if (IsOllamaInstalled())
{
    Console.WriteLine("Ollama already installed.");
    installed = true;
    await Task.Delay(2000);
    goto MainChat;
}
else
{
    Console.WriteLine("Ollama isn't installed. Install now? (Y/N)");
    var input = Console.ReadLine();
    if (input?.Trim().ToUpper() == "Y")
    {
        // Check if enough disk space available
        System.IO.DriveInfo drive = new System.IO.DriveInfo(Path.GetPathRoot(Environment.CurrentDirectory));
        if (drive.AvailableFreeSpace < 2L * 1024 * 1024 * 1024) // 2 GB
        {
            Console.WriteLine("Not enough disk space to install Ollama.");
            return -1;
        }
        

        Console.WriteLine("Downloading Ollama...");
        var progress = new Progress<int>(p =>
        {
            Console.WriteLine($"Downloading... {p}%");
        });

        try
        {
            await DownloadAndInstallOllamaAsync(progress);
            Console.WriteLine("Ollama installed successfully.");
            installed = true;
            await Task.Delay(2000);
            goto MainChat;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during installation: {ex.Message}");
        }
    }
}

MainChat:
HttpClient client = new HttpClient();
List<string> history = new List<string>();
Dictionary<string, string> comandosSistema = new Dictionary<string, string>()
        {
            { "NOTES", @"c:\deneos\systemapps\deneNotes\deneNotes.exe" },
            { "EXPLORER", @"c:\deneos\systemapps\deneFiles\deneFiles.exe" },
            { "BROWSER", @"C:\deneos\systemapps\deneNavi\deneNavi.exe" },
            { "CLEAR_CHAT", "CLEAR_CHAT" }
        };
Console.WriteLine("Wait a moment, checking which LLM model is the best for your system.");
Process.Start("ollama", $"pull qwen2:0.5b").WaitForExit();
await Task.Delay(1000); // ensure model is registered in Ollama
string recommendedModel = await AskForRecommendation();
Console.WriteLine($"Recommended model: {recommendedModel}");

Console.WriteLine("Chat initialized. You can start chatting now.\n\n");
while (true)
{
    Console.Write("You: ");
    string input = Console.ReadLine() ?? "";
    history.Add($"User: {input}");
    await ProcesarInput(input);
}



async Task<string> AskForRecommendation()
{
    RAM RAM_GB = 0;
    var searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
    foreach (var obj in searcher.Get()) {
        var ramBytes = Convert.ToDouble(obj["TotalPhysicalMemory"]);
        if (Debugger.IsAttached)
        {
            Console.WriteLine("Detected RAM: " + ramBytes + " bytes");
        }
        RAM_GB = Math.Round(ramBytes / (1024 * 1024 * 1024));
    }
    
    var WINVER = Environment.OSVersion.Version.Major >= 10 ? "10/11" : "older than 10";
    var availableModels = new List<string> { "qwen2:0.5b", "llama2:7b", "llama3:8b", "llama3.1:8b", "llama3.2:1b", "llama3.2:3b", "qwen3.5:0.8b", "qwen3.5:2b", "lfm2.5-thinking:1.2b" };
    var FREESPACE_GB = Math.Round(new DriveInfo(Path.GetPathRoot(Environment.CurrentDirectory)).AvailableFreeSpace / (1024.0 * 1024 * 1024));

    Console.WriteLine("RAM: " + RAM_GB.GB.ToString(CultureInfo.InvariantCulture) + "GB - Free space: " + FREESPACE_GB + "GB - Windows " + WINVER);
    string mode = Debugger.IsAttached ? "" : "Just answer with the model codename, no explanations. If you are between more than one, choose the most balanced one. Not being too small for normal tasks but being capable of running. With codename I mean the name model in the exact same way it is going to be given to you in the following list.";
    var request = new
    {
        model = "qwen2:0.5b",
        prompt = $"Which is the best LLM model for a system with {RAM_GB.GB.ToString(CultureInfo.InvariantCulture)}GB RAM, {FREESPACE_GB}GB of free disk space and Windows {WINVER} from this list? " + mode + $"\n\n List of models: {string.Join(", ", availableModels)}",
        stream = false
    };

    var json = JsonSerializer.Serialize(request);
    var response = await client.PostAsync(
        "http://localhost:11434/api/generate",
        new StringContent(json, Encoding.UTF8, "application/json")
    );

    var responseText = await response.Content.ReadAsStringAsync();

    using var doc = JsonDocument.Parse(responseText);
    if (doc.RootElement.TryGetProperty("response", out var token))
    {
        return token.GetString() ?? "";
    }

    return "";
}

async Task ProcesarInput(string input)
        {
            string prompt = $@"
Eres un asistente de deneOS. Solo reconoce comandos o responde normalmente.
Lista de comandos (input exacto o similar):
- abre el bloc de notas -> COMMAND:NOTES
- abre explorador -> COMMAND:EXPLORER
- reinicia el chat -> COMMAND:CLEAR_CHAT
- abre el navegador -> COMMAND:BROWSER

Usuario: {input}";

            string response = await AskOllama(prompt);

            if (response.StartsWith("COMMAND:"))
            {
                string cmdName = response.Substring(8).Trim();
                if (comandosSistema.TryGetValue(cmdName, out string ruta))
                {
                    if (ruta == "CLEAR_CHAT")
                    {
                        Console.Clear();
                        history.Clear();
                        Console.WriteLine("Chat and history cleared.\n\n");
                    }
                    else
                        System.Diagnostics.Process.Start(ruta);

                    Console.WriteLine($"Executing command: {cmdName}\n\n");
                }
                else
                {
                    Console.WriteLine($"⚠ Command not recognized: {cmdName}\n\n");
                }
            }
            else
            {
                Console.WriteLine(response + "\n\n");
            }
        }

bool IsOllamaInstalled()
        {
            try
            {
                using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
                var response = client.GetAsync("http://localhost:11434/api/tags").Result;
                return response.IsSuccessStatusCode;
            }
            catch { }

            string defaultPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Programs", "Ollama", "ollama.exe"
            );
            return File.Exists(defaultPath);
        }

async Task DownloadAndInstallOllamaAsync(IProgress<int> progress)
        {
            string installerUrl = "https://ollama.com/download/OllamaSetup.exe";
            string tempPath = Path.Combine(Path.GetTempPath(), "OllamaSetup.exe");

            using (var client = new HttpClient())
            using (var response = await client.GetAsync(installerUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                var totalBytes = response.Content.Headers.ContentLength ?? -1;
                using var stream = await response.Content.ReadAsStreamAsync();
        
                using (var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var buffer = new byte[8192];
                    long downloaded = 0;
                    int read;

                    while ((read = await stream.ReadAsync(buffer)) > 0)
                    {
                        await fileStream.WriteAsync(buffer.AsMemory(0, read));
                        downloaded += read;
                        if (totalBytes > 0)
                            progress?.Report((int)(downloaded * 100 / totalBytes));
                    }

                    await fileStream.FlushAsync();
                } // liberado
            }

            // archivo liberado
            var psi = new ProcessStartInfo
            {
                FileName = tempPath,
                Arguments = "/SILENT",
                UseShellExecute = true
            };

            using var proc = Process.Start(psi);
            await proc!.WaitForExitAsync();

            File.Delete(tempPath);
        }

async Task<string> AskOllama(string prompt)
        {
            var selectedModel = recommendedModel; // or let user choose (not yet)

            if (selectedModel == null)
            {
                Console.WriteLine("Unexpected error occurred.");
                return "No model selected. Please restart the application.";
            }
            var request = new
            {
                model = selectedModel,
                prompt,
                stream = false // <-- no streaming
            };

            var json = JsonSerializer.Serialize(request);
            //client.Timeout = System.Threading.Timeout.InfiniteTimeSpan;
            var response = await client.PostAsync(
                "http://localhost:11434/api/generate",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            var responseText = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseText);
            if (doc.RootElement.TryGetProperty("response", out var token))
            {
                return token.GetString() ?? "";
            }

            return "";
        }

return 0;
struct RAM
{
    public double GB { get; set; }
    // Implicit operator double -> String and to assign double value directly to RAM struct
    public static implicit operator RAM(double gb) => new RAM { GB = gb };
    public static implicit operator double(RAM ram) => ram.GB;
}
