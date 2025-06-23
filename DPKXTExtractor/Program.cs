Console.WriteLine("DPKXT - dene Package Xtractor");
Console.WriteLine("Assigning variables...");
byte[] dpkxt_deps_json = DPKXTExtractor.Properties.Resources.dpkxtdepsjson;
byte[] dpkxt_dll = DPKXTExtractor.Properties.Resources.dpkxtdll;
byte[] dpkxt_exe = DPKXTExtractor.Properties.Resources.dpkxtexe;
byte[] dpkxt_runtimeconfig_json = DPKXTExtractor.Properties.Resources.dpkxtruntimeconfigjson;
byte[] Microsoft_Windows_SDK_NET_dll = DPKXTExtractor.Properties.Resources.MicrosoftWindowsSDKNETdll;
byte[] WinRT_Runtime_dll = DPKXTExtractor.Properties.Resources.WinRTRuntimedll;
Console.WriteLine("Creating temp directory...");
string tempPath = Path.Combine(Path.GetTempPath(), "DPKXTExtractor");
if (Directory.Exists(tempPath))
{
    Directory.Delete(tempPath, true);
}
Directory.CreateDirectory(tempPath);
Console.WriteLine("Extracting resources...");
File.WriteAllBytes(Path.Combine(tempPath, "dpkxtdeps.json"), dpkxt_deps_json);
File.WriteAllBytes(Path.Combine(tempPath, "dpkxt.dll"), dpkxt_dll);
File.WriteAllBytes(Path.Combine(tempPath, "dpkxt.exe"), dpkxt_exe);
File.WriteAllBytes(Path.Combine(tempPath, "dpkxt.runtimeconfig.json"), dpkxt_runtimeconfig_json);
File.WriteAllBytes(Path.Combine(tempPath, "Microsoft.Windows.SDK.NET.dll"), Microsoft_Windows_SDK_NET_dll);
File.WriteAllBytes(Path.Combine(tempPath, "WinRT.Runtime.dll"), WinRT_Runtime_dll);
Console.WriteLine("Copying into deneOS SYS folder");
string sysPath = @"C:\DENEOS\dpkxt";
if (Directory.Exists(sysPath))
{
    Directory.Delete(sysPath, true);
}
Directory.CreateDirectory(sysPath);
File.Copy(Path.Combine(tempPath, "dpkxtdeps.json"), Path.Combine(sysPath, "dpkxt.deps.json"));
File.Copy(Path.Combine(tempPath, "dpkxt.dll"), Path.Combine(sysPath, "dpkxt.dll"));
File.Copy(Path.Combine(tempPath, "dpkxt.exe"), Path.Combine(sysPath, "dpkxt.exe"));
File.Copy(Path.Combine(tempPath, "dpkxt.runtimeconfig.json"), Path.Combine(sysPath, "dpkxt.runtimeconfig.json"));
File.Copy(Path.Combine(tempPath, "Microsoft.Windows.SDK.NET.dll"), Path.Combine(sysPath, "Microsoft.Windows.SDK.NET.dll"));
File.Copy(Path.Combine(tempPath, "WinRT.Runtime.dll"), Path.Combine(sysPath, "WinRT.Runtime.dll"));
Console.WriteLine("Resources copied successfully.");
Console.WriteLine("Cleaning up temporary files...");
Directory.Delete(tempPath, true);
Console.WriteLine("Temporary files cleaned up.");
Console.WriteLine("DPKXTExtractor completed successfully.");
Console.Write("Press any key to exit...");
Console.ReadKey();
Environment.Exit(0);