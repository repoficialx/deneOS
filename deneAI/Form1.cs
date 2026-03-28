using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace deneAI
{
    public partial class Form1 : Form
    {
        HttpClient client = new HttpClient();
        List<string> history = new List<string>();
        Dictionary<string, string> comandosSistema = new Dictionary<string, string>()
        {
            { "NOTES", @"c:\deneos\systemapps\deneNotes\deneNotes.exe" },
            { "EXPLORER", @"c:\deneos\systemapps\deneFiles\deneFiles.exe" },
            { "BROWSER", @"C:\deneos\systemapps\deneNavi\deneNavi.exe" },
            { "CLEAR_CHAT", "CLEAR_CHAT" }
        };
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Clear();
            ComboBox.ObjectCollection items = new(comboBox1)
            {
                new LLModel { billions = 7, codename = "llama2:7b", gigabytes = 4, name = "Llama 2 (7B)" },
                new LLModel { billions = 7, codename = "llama3:8b", gigabytes = 5, name = "Llama 3 (8B)" },
                new LLModel { billions = 7, codename = "llama3.1:8b", gigabytes = 5, name = "Llama 3.1 (8B)" },
                new LLModel { billions = 7, codename = "llama3.2:1b", gigabytes = 1, name = "Llama 3.2 (1B)" },
                new LLModel { billions = 7, codename = "llama3.2:3b", gigabytes = 2, name = "Llama 3.2 (3B)" },
                new LLModel { billions = 7, codename = "qwen3.5:0.8b", gigabytes = 1, name = "Qwen 3.5 (0.8B)" },
                new LLModel { billions = 7, codename = "qwen3.5:2b", gigabytes = 3, name = "Qwen 3.5 (2B)" },
                new LLModel { billions = 7, codename = "lfm2.5-thinking:1.2b", gigabytes = 0.73f, name = "LFM 2.5 Thinking (1.2B)" }
            };
            comboBox1.DataSource = items;
            comboBox1.DisplayMember = "name";
            comboBox1.SelectedItem = items[3];
            comboBox1.Text = comboBox1.SelectedItem is LLModel llm ? llm.name : "Unknown Model";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is LLModel llm)
            {
                if (llm.gigabytes > 4)
                {
                    var result =
                        MessageBox.Show(
                            $"Selected model ({llm.name}) requires {Math.Ceiling(llm.gigabytes)} GB of free space. Make sure to have enough storage.",
                            "deneAI", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    switch (result)
                    {
                        case System.Windows.Forms.DialogResult.OK:
                            break;
                        case System.Windows.Forms.DialogResult.Cancel:
                            comboBox1.SelectedIndex = 3;
                            return;
                    }
                }

                if (llm.gigabytes >= 2)
                {
                    MessageBox.Show(
                        $"Selected model ({llm.name}) is quite large ({Math.Ceiling(llm.gigabytes)} GB). Performance may be slow on low-end systems.",
                        "deneAI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (llm.gigabytes < 2)
                {
                    MessageBox.Show(
                        $"Selected model ({llm.name}) is lightweight ({Math.Ceiling(llm.gigabytes)} GB) and should run well on most systems.",
                        "deneAI", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                // get free space on C: drive

            CHECK:
                DriveInfo drive = new DriveInfo("C:\\");

                long freeSpaceGB = drive.AvailableFreeSpace / (1024 * 1024 * 1024);
                long totalSpaceGB = drive.TotalSize / (1024 * 1024 * 1024);

                int GBRequeridos = (int)Math.Ceiling(llm.gigabytes) + 3;
                int GBRecomendados = (int)Math.Ceiling(llm.gigabytes) + 5;

                if (freeSpaceGB < GBRequeridos)
                {
                    var result = MessageBox.Show(
                        $"You have only {freeSpaceGB} GB of free space, but the selected model ({llm.name}) requires at least {GBRequeridos} GB. Please free up some space before using this model.",
                        "deneAI", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                    switch (result)
                    {
                        case DialogResult.Cancel:
                            comboBox1.SelectedIndex = 3;
                            return;
                        case DialogResult.Retry:
                            goto CHECK;
                    }
                }
                else if (freeSpaceGB < GBRecomendados)
                {
                    var result = MessageBox.Show(
                        $"Caution: You have {freeSpaceGB} GB of free space, which is below the recommended {GBRecomendados} GB for the selected model ({llm.name}). Performance may be affected.",
                        "deneAI", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);

                    switch (result)
                    {
                        case DialogResult.Abort:
                            comboBox1.SelectedIndex = 3;
                            return;
                        case DialogResult.Retry:
                            goto CHECK;
                        case DialogResult.Ignore:
                            break;
                    }
                }

                // Comprobar instalación
                string path = @"%userprofile%\.ollama\models\manifests\registry.ollama.ai\library\";
                string parentModelName = llm.codename.Split(':')[0];
                string billionParams = llm.codename.Split(':')[1];
                bool parentModelExists = Directory.Exists(Path.Combine(path, parentModelName));
                bool exactModelExists =
                    parentModelExists && File.Exists(Path.Combine(path, parentModelName, billionParams));

                if (!exactModelExists)
                {
                    var result = MessageBox.Show(
                        $"The selected model ({llm.name}) is not installed in Ollama. Do you want to install it now? (This will download the model and may take some time)",
                        "deneAI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            MessageBox.Show($"Downloading and installing {llm.name} via Ollama. This may take a while depending on your internet speed.", "deneAI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            System.Diagnostics.Process.Start("ollama", $"pull {llm.codename}").WaitForExit();
                            MessageBox.Show($"{llm.name} has been installed. You can now use it in deneAI.", "deneAI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case DialogResult.No:
                            comboBox1.SelectedIndex = 3;
                            return;
                    }
                }
            }
        }

        struct LLModel
        {
            public override string ToString()
            {
                return $"{name} - {gigabytes} ({codename})";
            }

            public string name { get; set; }
            public string codename { get; set; }
            public int billions { get; set; }
            public float gigabytes { get; set; }
        }
        async Task StreamAI(string prompt)
        {
            var request = new
            {
                model = "llama3.2:1b",
                prompt = prompt,
                stream = true
            };

            var json = JsonSerializer.Serialize(request);

            var response = await client.PostAsync(
                "http://localhost:11434/api/generate",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            history.Add("AI: " + response);

            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                using var doc = JsonDocument.Parse(line);

                if (doc.RootElement.TryGetProperty("response", out var token))
                {
                    rtbChat.AppendText(token.GetString());
                }
            }

            rtbChat.AppendText("\n\n");
        }
        /*
        private async void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;

            var prompt = txtPrompt.Text;

            history.Add("User: " + prompt);

            var fullPrompt = string.Join("\n", history) + "\nAI:";

            rtbChat.AppendText("You: " + prompt + "\nAI: ");

            txtPrompt.Clear();

            await StreamAI(fullPrompt);

            btnSend.Enabled = true;

            txtPrompt.Focus();
        }*/
        public async Task<string> AskOllama(string prompt)
        {
            var selectedModel = comboBox1.SelectedItem is LLModel model ? model : (LLModel?)null;

            if (selectedModel == null)
            {
                MessageBox.Show("Please select a model from the dropdown.");
                return "No model selected";
            }
            var request = new
            {
                //model = "llama3.2:1b",
                model = selectedModel?.codename,
                prompt,
                stream = false // <-- no streaming
            };

            var json = JsonSerializer.Serialize(request);
            client.Timeout = new(Int32.MaxValue, 23, 59, 59);
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
        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is LLModel llm)
            {
                btnSend.Enabled = false;
                string path = @"%userprofile%\.ollama\models\manifests\registry.ollama.ai\library\";
                string parentModelName = llm.codename.Split(':')[0];
                string billionParams = llm.codename.Split(':')[1];
                bool parentModelExists = Directory.Exists(Path.Combine(path, parentModelName));
                bool exactModelExists =
                    parentModelExists && File.Exists(Path.Combine(path, parentModelName, billionParams));

                if (!exactModelExists)
                {
                    var result = MessageBox.Show(
                        $"The selected model ({llm.name}) is not installed in Ollama. Do you want to install it now? (This will download the model and may take some time)",
                        "deneAI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            MessageBox.Show(
                                $"Downloading and installing {llm.name} via Ollama. This may take a while depending on your internet speed.",
                                "deneAI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            System.Diagnostics.Process.Start("ollama", $"pull {llm.codename}").WaitForExit();
                            MessageBox.Show($"{llm.name} has been installed. You can now use it in deneAI.", "deneAI",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case DialogResult.No:
                            comboBox1.SelectedIndex = 3;
                            return;
                    }
                }

                var prompt = txtPrompt.Text;
                txtPrompt.Clear();
                rtbChat.AppendText("You: " + prompt + "\nAI: ");

                await ProcesarInput(prompt);

                btnSend.Enabled = true;
                txtPrompt.Focus();
            }
        }
        private async Task ProcesarInput(string input)
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
                        btnClear.PerformClick();
                    else
                        System.Diagnostics.Process.Start(ruta);

                    rtbChat.AppendText($"✅ Ejecutando comando: {cmdName}\n\n");
                }
                else
                {
                    rtbChat.AppendText($"⚠ Comando no reconocido: {cmdName}\n\n");
                }
            }
            else
            {
                AppendMarkdown(rtbChat, response + "\n\n");
            }
        }
        private bool IsOllamaInstalled()
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

        public static void AppendMarkdown(RichTextBox rtb, string text)
        {
            int index = 0;

            while (index < text.Length)
            {
                int startBold = text.IndexOf("**", index);
                if (startBold == -1)
                {
                    rtb.AppendText(text.Substring(index));
                    break;
                }

                // texto normal antes de los **
                rtb.AppendText(text.Substring(index, startBold - index));

                int endBold = text.IndexOf("**", startBold + 2);
                if (endBold == -1) endBold = text.Length;

                // texto en negrita
                int boldStart = rtb.TextLength;
                rtb.AppendText(text.Substring(startBold + 2, endBold - startBold - 2));
                rtb.Select(boldStart, endBold - startBold - 2);
                rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);
                rtb.SelectionLength = 0;

                index = endBold + 2;
            }
        }
        async Task<bool> OllamaRunning()
        {
            try
            {
                var res = await client.GetAsync("http://localhost:11434/api/tags");
                return res.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            if (!await OllamaRunning())
            {
                MessageBox.Show("Ollama no está iniciado.");
            }
        }


        private void txtPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                btnSend.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbChat.Clear();

            history.Clear();

            rtbChat.AppendText("Chat cleared.\n\n");

            txtPrompt.Focus();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
