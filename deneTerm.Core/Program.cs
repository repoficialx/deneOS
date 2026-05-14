using System.Diagnostics;
using denePathParser;

string GetAlias(string path)
{
    return denePathParser.Parse(path);
}

Console.WriteLine("Hello, World!");

const string userPath = @"C:\DNUSR";
const string softwarePath = @"C:\SOFTWARE";
string currentPath = @"C:\DNUSR";
string argCmd = string.Empty;

if (args.Length>1)
{
    argCmd = string.Join(" ", args, 0, args.Length);
}

void W(string message)
    {
        Console.WriteLine(message);
    }

Console.Title = $"deneTerm - Safe Terminal {(argCmd==null?"":$" - {argCmd}")}";
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("deneTerm - Safe terminal made for deneOS");
Console.WriteLine("Type 'help' for a list of commands.");

while (true)
{
    string cmd;
    string argument;

    if (argCmd != string.Empty)
    {
        Console.WriteLine($"Executing command from arguments: {argCmd}");

        var parts = argCmd.Split(' ', 2);
        cmd = parts[0].ToLower();
        argument = parts.Length > 1 ? parts[1] : string.Empty;

        argCmd = string.Empty;
    } else
    {
        Console.Write("{0}\\> ", GetAlias(currentPath));
        string input = Console.ReadLine().Trim();
        if (string.IsNullOrEmpty(input))
            continue;

        var parts = input.Split(' ', 2);
        cmd = parts[0].ToLower();
        argument = parts.Length > 1 ? parts[1] : string.Empty;
    }
    if (cmd == "help")
    {
        W("Available commands:");
        string separator = new string('-', 30);
        W(separator);
        W("help: Show this help message");
        W("ls: List directory contents");
        W("cd <path>: Change directory");
        W("chamadi <user|software>: Switch to user or software directory");
        W("repair: Executes repair commands (sfc and DISM)");   
        W("exit: Exit the terminal");
        W("mkdir <path>: Create a directory");
        W("rmdir <path>: Remove a directory");
        W("mkfile <path>: Create an empty file");
        W("rmfile <path>: Remove a file");
        W("openfile <path>: Open a file with the default application");
        W("pwd: Show current directory");
        W("start <path>: Start an application or open a file");
        W("version: Show terminal version");
        W("openapp <app>: Open an application (e.g., deneNotes, GrayJay, Edge)");
        W("a:openapp <app>: Open an application as administrator");
        W("l:openapp <app>: Open an application with output");
        W("l:a:openapp <app>: Open an application as administrator with output");
    }
    else if (cmd == "version")
    {
        W("deneTerm v0.9.1 - Safe Terminal for deneOS");
        W("Developed by repoficialx");
    }
    else if (cmd == "ls")
    {
        ListDirectory();
    }
    else if (cmd == "cd")
    {
        ChangeDirectory(argument);
    }
    else if (cmd == "chamadi")
    {
        ChangeMainDirectory(argument);
    }
    else if (cmd == "repair")
    {
        StartRepair();
    }
    else if (cmd == "exit")
    {
        W("Exiting deneTerm...");
        Environment.Exit(0);
    }
    else if (cmd == "mkdir")
    {
        CreateDirectory(argument);
    }
    else if (cmd == "rmdir")
    {
        RemoveDirectory(argument);
    }
    else if (cmd == "mkfile")
    {
        CreateFile(argument);
    }
    else if (cmd == "rmfile")
    {
        DeleteFile(argument);
    }
    else if (cmd == "openfile")
    {
        OpenFile(argument);
    }
    else if (cmd == "openapp")
    {
        OpenApp(argument, false, false);
    }
    else if (cmd == "l:openapp")
    {
        OpenApp(argument, false, true);
    }
    else if (cmd == "a:openapp")
    {
        OpenApp(argument, true, false);
    }
    else if (cmd == "l:a:openapp")
    {
        OpenApp(argument, true, true);
    }
    else if (cmd == "pwd")
    {
        W(GetAlias(currentPath));
    }
    else if (cmd == "start")
    {
        if (System.IO.File.Exists(System.IO.Path.Combine(currentPath, argument)) || System.IO.Directory.Exists(System.IO.Path.Combine(currentPath, argument)))
        {
            try
            {
                Process process = new();

                process.StartInfo.FileName = Path.Combine(currentPath, argument);
                process.StartInfo.UseShellExecute = true;

                process.Start();
            } catch (Exception ex)
            {
                W("Error: {0}", ex.Message);
            }
        } else
        {
            W("File or directory not found: {0}", argument);
        }
    } else
    {
        W("Unknown command: {0}. Type 'help' for a list of available commands.", cmd);
    }
}
return 0;

void ListDirectory()
{
    try
    {
        var files = Directory.GetFiles(currentPath);
        var dirs = Directory.GetDirectories(currentPath);
        W("Files: ");
        foreach (var file in files)
        {
            W("  [F] {0}", Path.GetFileName(file));
        }
        W("Directories: ");
        foreach (var dir in dirs)        {
            W("  [D] {0}", Path.GetFileName(dir));
        }
    }
    catch (Exception ex)
    {
        W("Error listing directory: {0}", ex.Message);
    }
}

void ChangeDirectory(string dir)
{
    if (string.IsNullOrEmpty(dir))
    {
        W("Usage: cd <path>");
        return;
    }
    string newPath;

    if (dir == "..")
    {
        string parentPath = string.Empty;

        DirectoryInfo dirInfo = Directory.GetParent(currentPath);
        if (dirInfo != null)
            parentPath = dirInfo.FullName;

        bool violatesUserPath = currentPath.StartsWith(userPath) && !parentPath.StartsWith(userPath);
        bool violatesSoftwarePath = currentPath.StartsWith(softwarePath) && !parentPath.StartsWith(softwarePath);

        if (violatesUserPath || violatesSoftwarePath)
        {
            W("⛔ Access denied: Cannot navigate above the main directory.");
            return;
        }

        string newPath = parentPath;
    } else
    {
        newPath = Path.Combine(currentPath, dir);
    } if (Directory.Exists(newPath) && IsAllowedPath(newPath))
    {
        currentPath = newPath;
        W("Changed directory to: {0}", GetAlias(currentPath));
    } else
    {
        W("Directory not found or access denied: {0}", dir);
    }
}

void ChangeMainDirectory(string target)
{
    if (target.ToLower() == "user")
    {
        currentPath = userPath;
        W("Switched to user directory: {0}", GetAlias(currentPath));
    } else if (target.ToLower() == "software")
    {
        currentPath = softwarePath;
        W("Switched to software directory: {0}", GetAlias(currentPath));
    } else
    {
        W("Usage: chamadi <user|software>");
    }
}

void StartRepair()
{
    StartRepairCore(false);
}

void StartRepair(bool showInfo)
{
    StartRepairCore(showInfo);
}

void StartRepairCore(bool showInfo)
{
    W("Starting repair process with SFC and DISM...");

    try
    {
        // SFC
        Process sfcProcess = new();
        sfcProcess.StartInfo.FileName = "sfc";
        sfcProcess.StartInfo.Arguments = "/scannow";
        sfcProcess.StartInfo.UseShellExecute = false;
        sfcProcess.StartInfo.RedirectStandardOutput = true;
        sfcProcess.StartInfo.RedirectStandardError = true;
        sfcProcess.Start();
        string sfcOutput = sfcProcess.StandardOutput.ReadToEnd();
        string sfcError = sfcProcess.StandardError.ReadToEnd();

        if (showInfo)
        {
            W("Repairing system files...");
            W("SFC Output: {0}", sfcOutput);
            if (!string.IsNullOrEmpty(sfcError))
            {
                W("SFC Error: {0}", sfcError);
            }
        }

        // DISM
        Process dismProcess = new();
        dismProcess.StartInfo.FileName = "dism";
        dismProcess.StartInfo.Arguments = "/Online /Cleanup-Image /RestoreHealth";
        dismProcess.StartInfo.UseShellExecute = false;
        dismProcess.StartInfo.RedirectStandardOutput = true;
        dismProcess.StartInfo.RedirectStandardError = true;
        dismProcess.Start();
        string dismOutput = dismProcess.StandardOutput.ReadToEnd();
        string dismError = dismProcess.StandardError.ReadToEnd();
        W("Repair process started. This may take several minutes...");
        dismProcess.WaitForExit();
        sfcProcess.WaitForExit();
        if (showInfo)
        {
            W("DISM Output: {0}", dismOutput);
            if (!string.IsNullOrEmpty(dismError))
            {
                W("DISM Error: {0}", dismError);
            }
            W("System repair completed.");
        }
    } catch (Exception ex)
    {
        W("Error during repair process: {0}", ex.Message);
    }
    W("Repair process finished.");
}

void CreateDirectory(string name)
{
    string path = Path.Combine(currentPath, name);
    if (!IsAllowedPath(path))
    {
        W("⛔ Access denied: Cannot create directory here.");
        return;
    }

    try
    {
        Directory.CreateDirectory(path);
        W("Directory '{0}' created in '{1}'", name, GetAlias(path));
    }
    catch (Exception ex)
    {
        W("Error creating directory: {0}", ex.Message);
    }
}

void RemoveDirectory(string name)
{
    string path = Path.Combine(currentPath, name);

    if (!IsAllowedPath(path))
    {
        W("⛔ Access denied: Cannot remove directory here.");
        return;
    }

    try
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
            W("Directory '{0}' removed from '{1}'", name, GetAlias(path));
        } else
        {
            W("Directory '{0}' not found in '{1}'", name, GetAlias(currentPath));
        }
    } catch (Exception ex)
    {
        W("Error removing directory: {0}", ex.Message);
    }
}

void CreateFile(string name) {
    string path = Path.Combine(currentPath, name);
    if (!IsAllowedPath(path))
    {
        W("⛔ Access denied: Cannot create file here.");
        return;
    }

    try
    {
        File.Create(path).Close();
        W("File '{0}' created in '{1}'", name, GetAlias(path));
    }
    catch (Exception ex)
    {
        W("Error creating file: {0}", ex.Message);
    }
}

void DeleteFile(string name)
{
    string path = Path.Combine(currentPath, name);
    if (!IsAllowedPath(path))
    {
        W("⛔ Access denied: Cannot delete file here.");
        return;
    }
    try
    {
        if (File.Exists(path)) {
            File.Delete(path);
            W("File '{0}' removed from '{1}'", name, GetAlias(currentPath));
        } else
        {
            W("File '{0}' not found in '{1}'", name, GetAlias(currentPath));
        }
    } catch (Exception ex)
    {
        W("Error deleting file: {0}", ex.Message);
    }
}

void OpenFile(string name)
{
    string path = Path.Combine(currentPath, name);
    if (!IsAllowedPath(path))
    {
        W("⛔ Access denied: Cannot open file here.");
        return;
    } try
    {
        if (File.Exists(path))
        {
            Process process = new();
            process.StartInfo.FileName = path;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            W("File '{0}' opened from '{1}'", name, GetAlias(currentPath));
        } else
        {
            W("File '{0}' not found in '{1}'", name, GetAlias(currentPath));
        }
    } catch (Exception ex)
    {
        W("Error opening file: {0}", ex.Message);
    }
}

void StartProcess(string filePath, bool runAsAdmin, bool showOutput)
{
    Process process = new();
    process.StartInfo.FileName = filePath;
    process.StartInfo.UseShellExecute = !showOutput;
    process.StartInfo.RedirectStandardOutput = showOutput;
    process.StartInfo.RedirectStandardError = showOutput;
    process.StartInfo.CreateNoWindow = !showOutput;
    process.StartInfo.Verb = runAsAdmin ? "runas" : string.Empty;
    process.Start();
}

bool IsAllowedPath (string path)
{
    return path.StartsWith(userPath) || path.StartsWith(softwarePath);
}