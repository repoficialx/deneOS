//#include "pch.h"
//#include <windows.h>
//#include <iostream>

using namespace System;
using namespace System::Diagnostics;
using namespace System::IO;
using namespace System::Net;

ref class Program
{
public:
    static void Main(array<String^>^ args)
    {
		Console::WriteLine("DPKXT - deneOS Package Extractor Installer");
		Console::WriteLine("Setting up variables...");
		String^ webHosting = "https://repoficialx.xyz/dpkxt/";
		String^ dpkxt_deps_json = webHosting + "dpkxt.deps.json";
		String^ dpkxt_dll = webHosting + "dpkxt.dll";
		String^ dpkxt_exe = webHosting + "dpkxt.exe";
		String^ dpkxt_runtimeconfig_json = webHosting + "dpkxt.runtimeconfig.json";
		String^ Microsoft_Windows_SDK_NET_dll = webHosting + "Microsoft.Windows.SDK.NET.dll";
		String^ WinRT_Runtime_dll = webHosting + "WinRT.Runtime.dll";
		Console::WriteLine("Creating temp directory...");
		String^ tempPath = Path::Combine(Path::GetTempPath(), "DPKXTExtractor");
		if (Directory::Exists(tempPath))
		{
			Directory::Delete(tempPath, true);
		}
		Directory::CreateDirectory(tempPath);
		Console::WriteLine("Extracting resources...");
		WebClient^ client = gcnew System::Net::WebClient;
		client->DownloadFile(dpkxt_deps_json, Path::Combine(tempPath, "dpkxtdeps.json"));
		client->DownloadFile(dpkxt_dll, Path::Combine(tempPath, "dpkxt.dll"));
		client->DownloadFile(dpkxt_exe, Path::Combine(tempPath, "dpkxt.exe"));
		client->DownloadFile(dpkxt_runtimeconfig_json, Path::Combine(tempPath, "dpkxt.runtimeconfig.json"));
		client->DownloadFile(Microsoft_Windows_SDK_NET_dll, Path::Combine(tempPath, "Microsoft.Windows.SDK.NET.dll"));
		client->DownloadFile(WinRT_Runtime_dll, Path::Combine(tempPath, "WinRT.Runtime.dll"));
		Console::WriteLine("Copying into deneOS SYS folder");
		String^ sysPath = "C:\\DENEOS\\dpkxt";
		if (Directory::Exists(sysPath))
		{
			Directory::Delete(sysPath, true);
		}
		Directory::CreateDirectory(sysPath);
		array<String^>^ files = Directory::GetFiles(tempPath);
		for each (String ^ file in files)
		{
			String^ fileName = Path::GetFileName(file);
			File::Copy(file, Path::Combine(sysPath, fileName));
		}
		Console::WriteLine("Adding to PATH environment variable...");
		String^ pathEnv = Environment::GetEnvironmentVariable("PATH", EnvironmentVariableTarget::Machine);
		if (!pathEnv->Contains(sysPath))
		{
			pathEnv += ";" + sysPath;
			Environment::SetEnvironmentVariable("PATH", pathEnv, EnvironmentVariableTarget::Machine);
		}
		Console::WriteLine("DPKXT has been installed successfully!");
		Console::WriteLine("Cleaning up...");
		Directory::Delete(tempPath, true);
		Console::WriteLine("Installation complete! You can now use DPKXT from deneOS terminal by typing 'dpkxt'.");
		Console::WriteLine("Press any key to exit...");
		Console::ReadKey();
		Environment::Exit(0);
    }
};