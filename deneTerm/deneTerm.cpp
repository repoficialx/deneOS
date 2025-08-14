#include "pch.h"
#include <windows.h>
#include <iostream>

using namespace System;
using namespace System::Diagnostics;
using namespace System::IO;

ref class Program
{
public:
    // Variables est�ticas

    static String^ GetAlias(String^ path)
    {
        if (path->StartsWith(userPath))
        {
            String^ sub = path->Substring(userPath->Length)->TrimStart('\\', '/');
            return "~" + (sub != "" ? "\\" + sub : "");
        }
        if (path->StartsWith(softwarePath))
        {
            String^ sub = path->Substring(softwarePath->Length)->TrimStart('\\', '/');
            return "~S" + (sub != "" ? "\\" + sub : "");
        }
        return "?";
    }

    static initonly String^ userPath = "C:\\DNUSR";
    static initonly String^ softwarePath = "C:\\SOFTWARE";
    static String^ currentPath = "C:\\DNUSR";

    // M�todo Main como en C#
    static void Main(array<String^>^ args)
    {
        // Configuraci�n de la consola
        Console::Title = "deneTerm - Terminal Segura";
        Console::ForegroundColor = ConsoleColor::Green;
        Console::OutputEncoding = System::Text::Encoding::UTF8;
        Console::WriteLine("deneTerm - Terminal segura de deneOS");
        Console::WriteLine("Escribe 'help' para ver los comandos disponibles.\n");

        while (true) {
            Console::Write("{0}\\> ", { GetAlias(currentPath) });
            String^ input = Console::ReadLine()->Trim();
            if (String::IsNullOrEmpty(input)) continue;
            array<String^>^ parts = input->Split(' ', 2);
            String^ cmd = parts[0]->ToLower();
            String^ arg = parts->Length > 1 ? parts[1] : "";
            array<String^>^ parts1 = input->Trim()->Split(gcnew array<wchar_t>{' '}, 2);
            String^ command = parts1[0]->ToLower();
            String^ argument = parts1->Length > 1 ? parts1[1] : "";

            if (cmd == "help") {
                Console::WriteLine("Comandos disponibles:");
                Console::WriteLine("- help: Muestra esta ayuda");
                Console::WriteLine("- ls: Lista archivos del directorio actual");
                Console::WriteLine("- cd <carpeta>: Cambia de carpeta dentro del directorio permitido");
                Console::WriteLine("- chamadi <user/software>: Cambia entre ~\\ (usuario) y ~S\\ (software)");
                Console::WriteLine("- repair: Ejecuta herramientas de reparación");
                Console::WriteLine("- exit: Cierra la terminal");
                Console::WriteLine("- mkdir <nombre>: Crea una carpeta en el directorio actual");
                Console::WriteLine("- rmdir <nombre>: Elimina una carpeta en el directorio actual");
                Console::WriteLine("- mkfile <nombre>: Crea un archivo vacío en el directorio actual");
                Console::WriteLine("- rmfile <nombre>: Elimina un archivo en el directorio actual");
                Console::WriteLine("- openfile <nombre>: Abre un archivo en el directorio actual");
                Console::WriteLine("- openapp <nombre>: Abre una aplicación instalada en $PATH$ o en SOFTWARE");
                Console::WriteLine("- pwd: Muestra la ruta actual con alias");
                Console::WriteLine("- start <file>: Abre un archivo o ejecutable en el directorio actual");
				Console::WriteLine("- version: Muestra la versión de deneTerm");
                Console::WriteLine("- a:openapp <nombre>: Abre una aplicación instalada en $PATH$ o en SOFTWARE como administrador");
                Console::WriteLine("- l:openapp <nombre>: Abre una aplicación instalada en $PATH$ o en SOFTWARE mostrando salida");
                Console::WriteLine("- l:a:openapp <nombre>: Abre una aplicación instalada en $PATH$ o en SOFTWARE como administrador y mostrando salida");
            }
            else if (cmd == "version") {
                Console::WriteLine("deneTerm v1.1 - Terminal segura de deneOS");
                Console::WriteLine("Desarrollado por repoficialx");
			}
            else if (cmd == "ls") {
                ListDirectory();
            }
            else if (cmd == "cd") {
                ChangeDirectory(arg);
            }
            else if (cmd == "chamadi") {
                ChangeMainDirectory(arg);
            }
            else if (cmd == "repair") {
                StartRepair();
            }
            else if (cmd == "exit") {
                Console::WriteLine("Saliendo de deneTerm...");
                Environment::Exit(0);
            }
            else if (cmd == "mkdir") {
                CreateDirectory(arg);
            }
            else if (cmd == "rmdir") {
                RemoveDirectory(argument);
            }
            else if (cmd == "mkfile") {
                CreateFile(argument);
            }
            else if (cmd == "rmfile") {
                DeleteFile(argument);
            }
            else if (cmd == "openfile") {
                OpenFile(argument);
            }
            else if (cmd == "openapp") {
                OpenApp(argument, false, false);
            }
            else if (cmd == "l:openapp") {
                OpenApp(argument, false, false);
            }
            else if (cmd == "a:openapp") {
                OpenApp(argument, true, false);
            }
            else if (cmd == "l:a:openapp") {
                OpenApp(argument, true, true);
            }
            else if (cmd == "pwd") {
                Console::WriteLine(GetAlias(currentPath));
            }
            else if (cmd == "start") {
                if (System::IO::File::Exists(System::IO::Path::Combine(currentPath, argument)) || System::IO::Directory::Exists(System::IO::Path::Combine(currentPath, argument)))
                {
                    try
                    {
                        Process^ process = gcnew Process();

                        process->StartInfo->FileName = Path::Combine(currentPath, arg);
                        process->StartInfo->UseShellExecute = true;

                        process->Start();
                    }
                    catch (Exception^ ex)
                    {
                        Console::WriteLine("Error: {0}", ex->Message);
                    }
                }
                else
                {
                    Console::WriteLine("❌ Archivo o directorio no encontrado.");
                }
            }
            else {
                Console::WriteLine("❓ Comando no reconocido. Escribe 'help' para ver los comandos.");
            }
        }
    }

    static void ListDirectory()
    {
        try
        {
            array<String^>^ files = Directory::GetFiles(currentPath);
            array<String^>^ dirs = Directory::GetDirectories(currentPath);
            Console::WriteLine("📄 Archivos:");
            for each(String ^ file in files)
            {
                Console::WriteLine("  [F] " + Path::GetFileName(file));
            }
            Console::WriteLine("\n📁 Directorios:");
            for each(String ^ dir in dirs)
            {
                Console::WriteLine("  [D] " + Path::GetFileName(dir));
            }
        }
        catch (Exception^ ex)
        {
            Console::WriteLine("❌ Error al listar directorio: " + ex->Message);
        }
    }
    static void ChangeDirectory(String^ dir)
    {
        if (String::IsNullOrEmpty(dir))
        {
            Console::WriteLine("❌ Ruta vacía o no permitida.");
            return;
        }
        String^ newPath;

        if (dir == "..") {
            String^ parentPath = "";

            System::IO::DirectoryInfo^ dirInfo = System::IO::Directory::GetParent(currentPath);
            if (dirInfo != nullptr)
            {
                parentPath = dirInfo->FullName;
            }

            // No permitir salir de la main-directory
            bool violatesUserPath = currentPath->StartsWith(userPath) && !parentPath->StartsWith(userPath);
            bool violatesSoftwarePath = currentPath->StartsWith(softwarePath) && !parentPath->StartsWith(softwarePath);

            if (violatesUserPath || violatesSoftwarePath)
            {
                System::Console::WriteLine("❌ No puedes salir de la main-directory actual.");
                return;
            }


            System::String^ newPath = parentPath;
        }
        else {
            newPath = Path::Combine(currentPath, dir);
        }
        if (Directory::Exists(newPath) && IsAllowedPath(newPath))
        {
            currentPath = newPath;
            Console::WriteLine("📂 Cambiado a: " + GetAlias(currentPath));
        }
        else
        {
            Console::WriteLine("❌ Directorio ({0}) no válido o fuera de límites", dir);
        }
    }
    static void ChangeMainDirectory(String^ target) {
        if (target->ToLower() == "user") {

            currentPath = userPath;
            Console::WriteLine("✅ Cambiado a ~\\ (Usuario)");
        }
        else if (target->ToLower() == "software") {
            currentPath = softwarePath;
            Console::WriteLine("✅ Cambiado a ~S\\ (Software)");
        }
        else {
            Console::WriteLine("❌ Opción no válida. Usa 'chamadi user' o 'chamadi software'.");
        }
    }
    public: static void StartRepair()
    {
        StartRepairCore(false); // valor por defecto
    }
    public: static void StartRepair(bool showInfo)
{
    StartRepairCore(showInfo);
}
    static void StartRepairCore(Boolean^ showInfo)
    {
        Console::WriteLine("🔧 Iniciando reparación con SFC y DISM...");
        try
        {
            // Ejecutar SFC
            Process^ sfcProcess = gcnew Process();
            sfcProcess->StartInfo->FileName = "sfc";
            sfcProcess->StartInfo->Arguments = "/scannow";
            sfcProcess->StartInfo->UseShellExecute = false;
            sfcProcess->StartInfo->RedirectStandardOutput = true;
            sfcProcess->StartInfo->RedirectStandardError = true;
            sfcProcess->Start();
            String^ output = sfcProcess->StandardOutput->ReadToEnd();
            String^ error = sfcProcess->StandardError->ReadToEnd();
            if (showInfo)
            {
                Console::WriteLine("🔧 Reparando archivos del sistema...");
                Console::WriteLine("SFC Output: " + output);
			}
            //
            if (!String::IsNullOrEmpty(error))
                Console::WriteLine("SFC Error: " + error);
            // Ejecutar DISM
            Process^ dismProcess = gcnew Process();
            dismProcess->StartInfo->FileName = "dism";
            dismProcess->StartInfo->Arguments = "/Online /Cleanup-Image /RestoreHealth";
            dismProcess->StartInfo->UseShellExecute = false;
            dismProcess->StartInfo->RedirectStandardOutput = true;
            dismProcess->StartInfo->RedirectStandardError = true;
            dismProcess->Start();
            String^ dismOutput = dismProcess->StandardOutput->ReadToEnd();
            String^ dismError = dismProcess->StandardError->ReadToEnd();
			Console::WriteLine("🔧 Reparaciones iniciadas. Puede tardar varios minutos.");
            dismProcess->WaitForExit();
            sfcProcess->WaitForExit();
            if (showInfo)
            {
                Console::WriteLine("DISM Output: " + dismOutput);
                Console::WriteLine("🔧 Reparación de imagen del sistema completada.");
                
			}
            if (!String::IsNullOrEmpty(dismError))
                Console::WriteLine("DISM Error: " + dismError);
        }
        catch (Exception^ ex)
        {
            Console::WriteLine("❌ Error al ejecutar reparación: " + ex->Message);
		}
        Console::WriteLine("✅ Reparación completada.");
	}
    static void CreateDirectory(String^ name)
    {
		String^ path = Path::Combine(currentPath, name);
        if (!IsAllowedPath(path))
        {
            Console::WriteLine("❌ Acceso denegado");
            return;
		}

        try {
			Directory::CreateDirectory(path);
			Console::WriteLine("✅ Carpeta '{0}' creada en {1}", name, GetAlias(currentPath));
        }
        catch (Exception^ ex) {
            Console::WriteLine("❌ Error al crear carpeta: " + ex->Message);
		}
	}
    static void RemoveDirectory(String^ name) {
		String^ path = Path::Combine(currentPath, name);
        if (!IsAllowedPath(path))
        {
            Console::WriteLine("❌ Acceso denegado");
            return;
        }

        try {
            if (Directory::Exists(path)) {
                Directory::Delete(path, true);
                Console::WriteLine("🗑️ Carpeta '{0}' eliminada en {1}", name, GetAlias(currentPath));
            }
            else {
                Console::WriteLine("❌ Carpeta '{0}' no encontrada en {1}", name, GetAlias(currentPath));
            }
        }
        catch (Exception^ ex) {
            Console::WriteLine("❌ Error al eliminar carpeta: " + ex->Message);
        }
    }
    static void CreateFile(String^ name)
    {
        String^ path = Path::Combine(currentPath, name);
        if (!IsAllowedPath(path))
        {
            Console::WriteLine("❌ Acceso denegado");
            return;
        }
        try {
            File::Create(path)->Close();
            Console::WriteLine("✅ Archivo '{0}' creado en {1}", name, GetAlias(currentPath));
        }
        catch (Exception^ ex) {
            Console::WriteLine("❌ Error al crear archivo: " + ex->Message);
        }
	}
    static void DeleteFile(String^ name)
    {
        String^ path = Path::Combine(currentPath, name);
        if (!IsAllowedPath(path))
        {
            Console::WriteLine("❌ Acceso denegado");
            return;
        }
        try {
            if (File::Exists(path)) {
                File::Delete(path);
                Console::WriteLine("🗑️ Archivo '{0}' eliminado en {1}", name, GetAlias(currentPath));
            }
            else {
                Console::WriteLine("❌ Archivo '{0}' no encontrado en {1}", name, GetAlias(currentPath));
            }
        }
        catch (Exception^ ex) {
            Console::WriteLine("❌ Error al eliminar archivo: " + ex->Message);
        }
	}
    static void OpenFile(String^ name)
    {
        String^ path = Path::Combine(currentPath, name);
        if (!IsAllowedPath(path))
        {
            Console::WriteLine("❌ Acceso denegado");
            return;
        }
        try {
            if (File::Exists(path)) {
                Process^ process = gcnew Process();
                process->StartInfo->FileName = path;
                process->StartInfo->UseShellExecute = true;
                process->Start();
                Console::WriteLine("📂 Abriendo archivo: {0}", GetAlias(path));
            }
            else {
                Console::WriteLine("❌ Archivo '{0}' no encontrado en {1}", name, GetAlias(currentPath));
            }
        }
        catch (Exception^ ex) {
            Console::WriteLine("❌ Error al abrir archivo: " + ex->Message);
        }
    }
    static void OpenApp(String^ name, bool runAsAdmin, bool showOutput)
    {
        
        String^ path = Path::Combine(currentPath, name);
        if (!IsAllowedPath(path))
        {
            Console::WriteLine("❌ Acceso denegado");
            return;
        }
        try
        {
            Process^ process = gcnew Process();
            process->StartInfo->FileName = path;
            process->StartInfo->UseShellExecute = true;
            if (runAsAdmin)
            {
                process->StartInfo->Verb = "runas"; // Ejecutar como administrador
            }
            if (showOutput)
            {
                process->StartInfo->RedirectStandardOutput = true;
                process->StartInfo->RedirectStandardError = true;
                process->StartInfo->CreateNoWindow = false; // Mostrar ventana de la aplicaci��n
            }
            else
            {
                process->StartInfo->CreateNoWindow = true; // No mostrar ventana de la aplicaci��n
            }
            process->Start();
        }
        catch (Exception^ ex) {
        Retry:
            try {
				String^ softwareAppPath = Path::Combine(softwarePath, name);
                Console::WriteLine("🔍 Buscando aplicación en SOFTWARE...");
				array<String^>^ programs = Directory::GetDirectories(softwarePath);
                for each(String ^ program in programs) {
                    program = program->ToLower();
                    if (program->Contains(name)) {
						array<String^>^ files = Directory::GetFiles(program);
                        for each (String ^ file in files) {
                            // Si termina en .exe y el nombre coincide...
                            try {
                                if (file->EndsWith(".exe") && (program->StartsWith(file) || file->StartsWith(program))) {
									Process^ process = gcnew Process();
                                    process->StartInfo->FileName = file;
									process->StartInfo->UseShellExecute = true;
                                    if (runAsAdmin)
										process->StartInfo->Verb = "runas"; // Ejecutar como administrador
									if (showOutput)
										process->StartInfo->RedirectStandardOutput = true;
									else
										process->StartInfo->CreateNoWindow = true; // No mostrar ventana de la aplicaci��n
									process->StartInfo->CreateNoWindow = !showOutput; // No mostrar ventana de la aplicaci��n

                                    process->Start();
                                    Console::WriteLine("🚀 Aplicación '{0}' abierta desde SOFTWARE. (ID#2)", name);
                                    return;
								}
                            }
                            catch (Exception^ ex) {
                                // Si se llama a un nombre de la lista de probables:
                                try {
                                    array<String^>^ probableApps
										= { "launcher.exe", "main.exe", "start.exe", "app.exe", "program.exe" };
                                    for each (String ^ probable in probableApps) {
										String^ probablePath = Path::Combine(program, probable);
                                        if (File::Exists(probablePath)) {
                                            Process^ process = gcnew Process();
                                            process->StartInfo->FileName = probablePath;
                                            process->StartInfo->UseShellExecute = true;
                                            if (runAsAdmin)
                                                process->StartInfo->Verb = "runas"; // Ejecutar como administrador
                                            if (showOutput)
                                                process->StartInfo->RedirectStandardOutput = true;
                                            else
                                                process->StartInfo->CreateNoWindow = true; // No mostrar ventana de la aplicaci��n
                                            process->Start();
                                            Console::WriteLine("🚀 Aplicación '{0}' abierta desde SOFTWARE usando {1}. (ID#3)", name, probable);
                                            return;
										}
									}
                                }
                                catch (Exception^ ex) {
									// Si no es probable ni coincice con nada:
                                    try {
                                        // Abrir el primer .exe encontrado en el directorio
                                        array<String^>^ exeFiles = System::IO::Directory::GetFiles(program, "*.exe");
                                        String^ firstExe = exeFiles->Length > 0 ? exeFiles[0] : nullptr;
                                        if (firstExe != nullptr) {
                                            ProcessStartInfo^ startInfo = gcnew ProcessStartInfo();
                                            startInfo->FileName = firstExe;
                                            startInfo->UseShellExecute = true;
                                            if (runAsAdmin)
                                                startInfo->Verb = "runas"; // Ejecutar como administrador
                                            if (showOutput)
                                                startInfo->RedirectStandardOutput = true;
                                            else
                                                startInfo->CreateNoWindow = true; // No mostrar ventana de la aplicaci��n
                                            Process^ process = gcnew Process();
                                            process->StartInfo = startInfo;
                                            process->Start();
                                            Console::WriteLine("🚀 Aplicación '{0}' abierta desde SOFTWARE usando el primer ejecutable encontrado. (ID#4)", name);
                                        }
                                        else {
                                            Console::WriteLine("❌ No se encontró un ejecutable para '{0}' en SOFTWARE.", name);
                                        }
                                    }
                                    catch (Exception^ ex) {
                                        Console::WriteLine("❌ No se pudo abrir la aplicación '{0}' desde SOFTWARE. Desea abortar la operación?", name);
                                        String^ respuesta = Console::ReadLine()->Trim()->ToLower();
                                        System::Collections::Generic::List<String^>^ opcionessí = gcnew System::Collections::Generic::List<String^>();
                                        opcionessí->Add("sí");
                                        opcionessí->Add("si");
                                        opcionessí->Add("yes");
                                        opcionessí->Add("y");
                                        opcionessí->Add("s");
                                        System::Collections::Generic::List<String^>^ opcionesno = gcnew System::Collections::Generic::List<String^>();
										opcionesno->Add("no");
										opcionesno->Add("n");

                                        if (opcionessí->Contains(respuesta))
                                        {
                                            System::Console::WriteLine("❌ Operación abortada.");
                                            return;
                                        }
                                        else if (opcionesno->Contains(respuesta)) {
                                            Console::WriteLine("No se abortó la operación.");
											goto Retry; // Reintentar buscar la aplicaci��n
                                        }
                                        else {
                                            Console::WriteLine("❓ Respuesta no reconocida. Abortando operación.");
                                            return;
										}
                                    }
								}
							}
						}
					}
                }
            }
            catch (Exception^ ex) { Console::WriteLine("❌ No se pudo abrir la app."); }
        }
        
    }
    static bool IsAllowedPath(String^ path)
    {
        return path->StartsWith(userPath) || path->StartsWith(softwarePath);
    }
};
// Punto de entrada real del programa
int main(array<System::String^>^ args)
{
    Program::Main(args); // Llama al Main como si fuera en C#
    return 0;
}


