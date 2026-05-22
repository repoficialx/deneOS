using System.IO;

namespace DPKCore.Models
{
    public class DPKPackage
    {
        public string PackagePath { get; set; } = "";

        public string ExtractedPath { get; set; } = "";

        public Manifest Manifest { get; set; } = new Manifest();

        public string InstallPath { get; set; } = "";

        public string Name => Manifest.Name;

        public bool IsValid()
        {
            return
                File.Exists(PackagePath) &&
                Directory.Exists(ExtractedPath) &&
                Manifest != null &&
                !string.IsNullOrWhiteSpace(Manifest.Name);
        }
    }
}