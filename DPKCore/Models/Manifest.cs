namespace DPKCore.Models
{
    public class Manifest
    {
        public string Name { get; set; } = "";
        public string Version { get; set; } = "";
        public string Author { get; set; } = "";

        public string[] Permissions { get; set; } = [];

        public string? EntryPoint { get; set; }

        public string? Type { get; set; }

        public string? TargetRuntime { get; set; } = "deneOS-1.0";
    }
}