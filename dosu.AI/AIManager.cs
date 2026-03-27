using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace dosu.AI;

public static class AIManager
{
    private static HttpClient client = new HttpClient();
    public static List<string> history = new List<string>();

    private static string GetModelCodename(Models model)
    {
        return model switch
        {
            Models.Llama2_7B_4GB => "llama2:7b",
            Models.Llama2_Uncensored_7B_4GB => "llama2-uncensored:7b",
            Models.Llama2_13B_7GB => "llama2:13b",
            Models.Llama2_70B_39GB => "llama2:70b",
            Models.Llama2_Uncensored_70B_39GB => "llama2-uncensored:70b",
            Models.Llama3_8B_5GB => "llama3:8b",
            Models.Llama3_70B_40GB => "llama3:70b",
            Models.Llama3_1_8B_5GB => "llama3.1:8b",
            Models.Llama3_1_70B_43GB => "llama3.1:70b",
            Models.Llama3_1_405B_243GB => "llama3.1:405b",
            Models.Llama3_2_1B_1GB => "llama3.2:1b",
            Models.Llama3_2_3B_2GB => "llama3.2:3b",
            Models.Llama4_272B_67GB => "llama4:16x17b",
            Models.Llama4_2176B_245GB => "llama4:128x17b",
            Models.Qwen3_5_08B_1GB => "qwen3.5:0.8b",
            Models.Qwen3_5_2B_3GB => "qwen3.5:2b",
            Models.Qwen3_5_4B_3GB => "qwen3.5:4b",
            Models.Qwen3_5_9B_7GB => "qwen3.5:9b",
            Models.Qwen3_5_27B_17GB => "qwen3.5:27b",
            Models.Qwen3_5_35B_24GB => "qwen3.5:35b",
            Models.Qwen3_5_122B_81GB => "qwen3.5:122b",
            Models.LFM2_24B_14GB => "lfm2:24b",
            Models.Nemotron3Super_120B_87GB => "nemotron-3-super:120b",
            Models.Qwen3CoderNext_80B_85GB => "qwen3-coder-next:q8_0",
            Models.LFM2_5__1_2B_730MB => "lfm2.5-thinking:1.2b",
            _ => "llama3.2:1b"
        };
    }

    public enum Models
    {
        /// <summary>
        /// Llama 2 (7B - 4GB)
        /// </summary>
        Llama2_7B_4GB,
        /// <summary>
        /// Llama 2 (Uncensored) (7B - 4GB)
        /// </summary>
        Llama2_Uncensored_7B_4GB,
        /// <summary>
        /// Llama 2 (13B - 7GB)
        /// </summary>
        Llama2_13B_7GB,
        /// <summary>
        /// Llama 2 (70B - 39GB)
        /// </summary>
        Llama2_70B_39GB,
        /// <summary>
        /// Llama 2 (Uncensored) (70B - 39GB)
        /// </summary>
        Llama2_Uncensored_70B_39GB,
        /// <summary>
        /// Llama 3 (8B - 5GB)
        /// </summary>
        Llama3_8B_5GB,
        /// <summary>
        /// Llama 3 (70B - 40GB)
        /// </summary>
        Llama3_70B_40GB,
        /// <summary>
        /// Llama 3.1 (8B - 5GB)
        /// </summary>
        Llama3_1_8B_5GB,
        /// <summary>
        /// Llama 3.1 (70B - 43GB)
        /// </summary>
        Llama3_1_70B_43GB,
        /// <summary>
        /// Llama 3.1 (405B - 243GB)
        /// </summary>
        Llama3_1_405B_243GB,
        /// <summary>
        /// Llama 3.2 (1B - 1GB) [ Recommended ]
        /// </summary>
        Llama3_2_1B_1GB,
        /// <summary>
        /// Llama 3.2 (3B - 2GB)
        /// </summary>
        Llama3_2_3B_2GB,
        /// <summary>
        /// Llama 4 (272B - 67GB)
        /// </summary>
        Llama4_272B_67GB,
        /// <summary>
        /// Llama 4 (2.1T - 245GB)
        /// </summary>
        Llama4_2176B_245GB,
        /// <summary>
        /// Qwen 3.5 (0.8B - 1GB) [ Recommended ]
        /// </summary>
        Qwen3_5_08B_1GB,
        /// <summary>
        /// Qwen 3.5 (2B - 3GB)
        /// </summary>
        Qwen3_5_2B_3GB,
        /// <summary>
        /// Qwen 3.5 (4B - 3GB)
        /// </summary>
        Qwen3_5_4B_3GB,
        /// <summary>
        /// Qwen 3.5 (9B - 7GB)
        /// </summary>
        Qwen3_5_9B_7GB,
        /// <summary>
        /// Qwen 3.5 (27B - 17GB)
        /// </summary>
        Qwen3_5_27B_17GB,
        /// <summary>
        /// Qwen 3.5 (35B - 24GB)
        /// </summary>
        Qwen3_5_35B_24GB,
        /// <summary>
        /// Qwen 3.5 (122B - 81GB)
        /// </summary>
        Qwen3_5_122B_81GB,
        /// <summary>
        /// LFM 2 (24B - 14GB)
        /// </summary>
        LFM2_24B_14GB,
        /// <summary>
        /// Nemotron 3 Super (120B - 87GB)
        /// </summary>
        Nemotron3Super_120B_87GB,
        /// <summary>
        /// Qwen 3 Coder Next (80B - 85GB)
        /// </summary>
        Qwen3CoderNext_80B_85GB,
        /// <summary>
        /// LFM 2.5 Thinking (1.2B - 730MB) [ Recommended ]
        /// </summary>
        LFM2_5__1_2B_730MB,
    }
    
    public static async Task<string> Ask(string prompt)
    {
        history.Add("User: " + prompt);

        var request = new
        {
            model = "llama3.2:1b",
            prompt = string.Join("\n", history) + "\nAI:",
            stream = false
        };

        var json = JsonSerializer.Serialize(request);

        var response = await client.PostAsync(
            "http://localhost:11434/api/generate",
            new StringContent(json, Encoding.UTF8, "application/json")
        );

        var responseText = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(responseText);
        var answer = doc.RootElement.GetProperty("response").GetString();

        history.Add("AI: " + answer);
        return answer;
    }
}