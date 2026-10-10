using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace deneAI;

public partial class Advanced : Form
{
    private static bool Plus
    {
        get;
        set;

    }
    public static HttpClient httpClient = new HttpClient();
    List<string> history = new List<string>();
    Dictionary<string, string> comandosSistema = new Dictionary<string, string>()
    {
        { "NOTES", @"c:\deneos\systemapps\deneNotes\deneNotes.exe" },
        { "EXPLORER", @"c:\deneos\systemapps\deneFiles\deneFiles.exe" },
        { "BROWSER", @"C:\deneos\systemapps\deneNavi\deneNavi.exe" },
        { "CLEAR_CHAT", "CLEAR_CHAT" }
    };

    private readonly string? authCode;

    public Advanced(string? authCode = null)
    {
        InitializeComponent();


        rtbChat.ReadOnly = true;

        this.authCode = authCode;

        if (!string.IsNullOrWhiteSpace(authCode))
        {
            Shown += async (_, _) =>
            {
                bool success =
                    await ExchangeAuthCodeAsync(authCode);

                if (success)
                {
                    MessageBox.Show(
                        $"¡Sesión iniciada como {CurrentUser?.name}!",
                        "deneAI",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            };

            label1.Text += CurrentUser?.name;
        }
    }

    public class AiResponse
    {
        public string? response
        {
            get; set;
        }
        public string? plan
        {
            get; set;
        }
        public bool plus
        {
            get; set;
        }
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

    private async void button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(SessionToken))
        {
            MessageBox.Show(
                "Debes iniciar sesión para usar deneAI.",
                "deneAI",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            Process.Start("start https://repoficialx.xyz/auth/microsoft/deneai/login.php");

            return;
        }

        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                SessionToken
            );

        button1.Enabled = false;
        var prompt = txtPrompt.Text;
        txtPrompt.Clear();
        history.Add("User: " + prompt);

        var data = new
        {
            message = prompt,
            history = history
        };

        rtbChat.AppendText("You: " + prompt + "\nAI: ");
        string json = JsonSerializer.Serialize(data);

        using var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json"
        );

        try
        {
            using var response = await httpClient.PostAsync(
                "https://repoficialx.xyz/ai.php",
                content
            );

            // Verificar si la respuesta fue exitosa
            if (!response.IsSuccessStatusCode)
            {
                rtbChat.Text = $"Error del servidor: {response.StatusCode} - {response.ReasonPhrase}";
                return;
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();

            // Verificar si la respuesta está vacía
            if (string.IsNullOrWhiteSpace(jsonResponse))
            {
                rtbChat.Text = "Error: El servidor devolvió una respuesta vacía.";
                return;
            }

            var result = JsonSerializer.Deserialize<AiResponse>(jsonResponse);

            if (result?.response == null)
            {
                rtbChat.Text = "Error: La respuesta no contiene los datos esperados.";
                return;
            }

            var _response = result.response;
            Plus = result.plus;

            if (Plus)
            {
                label2.Show();
            }
            else
            {
                label2.Hide();
            }

            if (_response.StartsWith("COMMAND:"))
            {
                string cmdName = _response.Substring(8).Trim();
                if (comandosSistema.TryGetValue(cmdName, out string ruta))
                {
                    if (ruta == "CLEAR_CHAT")
                        btnClear.PerformClick();
                    else
                        System.Diagnostics.Process.Start(ruta);

                    //rtbChat.AppendText($"Ejecutando comando: {cmdName}\n\n");
                }
                else
                {
                    //rtbChat.AppendText($"Comando no reconocido: {cmdName}\n\n");
                }
            }
            else
            {
                AppendMarkdown(rtbChat, _response + "\n\n");
            }

            txtPrompt.Focus();

            button1.Enabled = true;
            history.Add("AI: " + _response);
        }
        catch (HttpRequestException ex)
        {
            rtbChat.Text = $"Error de conexión: {ex.Message}";
        }
        catch (JsonException ex)
        {
            rtbChat.Text = $"Error al procesar la respuesta: {ex.Message}";
        }
        catch (Exception ex)
        {
            rtbChat.Text = $"Error inesperado: {ex.Message}";
        }


    }

    private void Advanced_FormClosing(object sender, FormClosingEventArgs e)
    {
        Application.Exit();
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        rtbChat.Clear();

        history.Clear();

        rtbChat.AppendText("Chat cleared.\n\n");

        txtPrompt.Focus();
    }

    private async Task<bool> ExchangeAuthCodeAsync(string code)
    {
        try
        {
            var data = new
            {
                code = code
            };

            string json = JsonSerializer.Serialize(data);

            using var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            using var response = await httpClient.PostAsync(
                "https://repoficialx.xyz/auth/deneai/exchange.php",
                content
            );

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(
                    $"No se pudo iniciar sesión.\n\n" +
                    $"Servidor: {response.StatusCode}",
                    "deneAI",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }

            string jsonResponse =
                await response.Content.ReadAsStringAsync();

            var result =
                JsonSerializer.Deserialize<AuthResponse>(jsonResponse);

            if (
                result == null ||
                !result.success ||
                string.IsNullOrWhiteSpace(result.token)
            )
            {
                MessageBox.Show(
                    "El servidor no devolvió una sesión válida.",
                    "deneAI",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }

            // Guardamos el token en memoria por ahora.
            // En el siguiente paso lo almacenaremos de forma
            // segura para conservar la sesión entre reinicios.
            SessionToken = result.token;

            CurrentUser = result.user;

            return true;
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show(
                $"No se pudo conectar con deneAI.\n\n{ex.Message}",
                "deneAI",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            return false;
        }
        catch (JsonException ex)
        {
            MessageBox.Show(
                $"Respuesta inválida del servidor.\n\n{ex.Message}",
                "deneAI",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            return false;
        }
    }
    private string? SessionToken;

    private UserInfo? CurrentUser;

    public class AuthResponse
    {
        public bool success
        {
            get; set;
        }
        public string? token
        {
            get; set;
        }
        public UserInfo? user
        {
            get; set;
        }
    }

    public class UserInfo
    {
        public int id
        {
            get; set;
        }
        public string? email
        {
            get; set;
        }
        public string? name
        {
            get; set;
        }
        public string? picture
        {
            get; set;
        }
    }

    private void label2_Click(object sender, EventArgs e)
    {

    }
}
