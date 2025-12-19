//#if WINDOWS
using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma

if (args != null)
{
    if (args.Length > 0)
    {
        bool dualArg = args.Length > 1;
        switch (args[0], dualArg)
        {
            case ("deneStore", true):
                if (args[1] == "swc=true")
                {
                    // HKEY_CURRENT_USER\Software\deneOS\deneStore\skipWelcomeScreen(DWORD)
                    //Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\deneOS\\deneStore", "skipWelcomeScreen", 1);
                    // Registrar wpifile
                    using (RegistryKey skipWelcomeScreen = Registry.CurrentUser.CreateSubKey("Software\\deneOS\\deneStore"))
                    {
                        skipWelcomeScreen.SetValue("skipWelcomeScreen", 1, RegistryValueKind.DWord);
                    }
                } else if (args[1] == "swc=false")
                {
                    //Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\deneOS\\deneStore", "skipWelcomeScreen", 0);
                    using (RegistryKey skipWelcomeScreen = Registry.CurrentUser.CreateSubKey("Software\\deneOS\\deneStore"))
                    {
                        skipWelcomeScreen.SetValue("skipWelcomeScreen", 0, RegistryValueKind.DWord);
                    }
                } else if (args[1] == "installWpiSupport")
                {
                    bool isAdmin = IsRunningAsAdmin();
                    _ = isAdmin ? InstallWPISupport() : (Elevate() ? InstallWPISupport() : 0);
                }
                    break;
        }
    }
}
int InstallWPISupport()
{
    /*string Header = "Windows Registry Editor Version 5.00";
    string newline = "\r\n";
    string wpiFile = @"[HKEY_CLASSES_ROOT\wpifile]
@=""Application""
""EditFlags""=hex:38,07,00,00
""FriendlyTypeName""=hex(2):40,00,25,00,53,00,79,00,73,00,74,00,65,00,6d,00,52,\
  00,6f,00,6f,00,74,00,25,00,5c,00,53,00,79,00,73,00,74,00,65,00,6d,00,33,00,\
  32,00,5c,00,73,00,68,00,65,00,6c,00,6c,00,33,00,32,00,2e,00,64,00,6c,00,6c,\
  00,2c,00,2d,00,31,00,30,00,31,00,35,00,36,00,00,00

[HKEY_CLASSES_ROOT\wpifile\DefaultIcon]
@=""%1""

[HKEY_CLASSES_ROOT\wpifile\shell]

[HKEY_CLASSES_ROOT\wpifile\shell\open]
""EditFlags""=hex:00,00,00,00

[HKEY_CLASSES_ROOT\wpifile\shell\open\command]
@=""\""%1\"" %*""
""IsolatedCommand""=""\""%1\"" %*""

[HKEY_CLASSES_ROOT\wpifile\shell\runas]
""HasLUAShield""=""""

[HKEY_CLASSES_ROOT\wpifile\shell\runas\command]
@=""\""%1\"" %*""
""IsolatedCommand""=""\""%1\"" %*""

[HKEY_CLASSES_ROOT\wpifile\shell\runasuser]
@=""@shell32.dll,-50944""
""Extended""=""""
""SuppressionPolicyEx""=""{F211AA05-D4DF-4370-A2A0-9F19C09756A7}""

[HKEY_CLASSES_ROOT\wpifile\shell\runasuser\command]
""DelegateExecute""=""{ea72d00e-4960-42fa-ba92-7792a7944c1d}""

[HKEY_CLASSES_ROOT\wpifile\shellex]

[HKEY_CLASSES_ROOT\wpifile\shellex\ContextMenuHandlers]
@=""Compatibility""

[HKEY_CLASSES_ROOT\wpifile\shellex\ContextMenuHandlers\Compatibility]
@=""{1d27f844-3a1f-4410-85ac-14651078412d}""

[HKEY_CLASSES_ROOT\wpifile\shellex\ContextMenuHandlers\PintoStartScreen]
@=""{470C0EBD-5D73-4d58-9CED-E91E22E23282}""

[HKEY_CLASSES_ROOT\wpifile\shellex\DropHandler]
@=""{86C86720-42A0-1069-A2E8-08002B30309D}""

[HKEY_CLASSES_ROOT\wpifile\shellex\PropertySheetHandlers]

[HKEY_CLASSES_ROOT\wpifile\shellex\PropertySheetHandlers\ShimLayer Property Page]
@=""{513D916F-2A8E-4F51-AEAB-0CBC76FB1AF8}""";
    string _wpi = @"[HKEY_CLASSES_ROOT\.wpi]
@=""wpifile""
""Content Type""=""application/x-msdownload""

[HKEY_CLASSES_ROOT\.wpi\PersistentHandler]
@=""{098f2470-bae0-11cd-b579-08002b30bfeb}""";

    string wpiFile_full = Header + newline + wpiFile;
    string _wpi_full = Header + newline + _wpi;*/

    try
    {
        // Registrar wpifile
        using (RegistryKey wpifile = Registry.ClassesRoot.CreateSubKey("wpifile"))
        {
            wpifile.SetValue("", "Application");
            wpifile.SetValue("EditFlags", new byte[] { 0x38, 0x07, 0x00, 0x00 }, RegistryValueKind.Binary);
            wpifile.SetValue("FriendlyTypeName", new byte[] {
                    0x40,0x00,0x25,0x00,0x53,0x00,0x79,0x00,0x73,0x00,0x74,0x00,0x65,0x00,0x6d,0x00,
                    0x52,0x00,0x6f,0x00,0x6f,0x00,0x74,0x00,0x25,0x00,0x5c,0x00,0x53,0x00,0x79,0x00,
                    0x73,0x00,0x74,0x00,0x65,0x00,0x6d,0x00,0x33,0x00,0x32,0x00,0x5c,0x00,0x73,0x00,
                    0x68,0x00,0x65,0x00,0x6c,0x00,0x6c,0x00,0x33,0x00,0x32,0x00,0x2e,0x00,0x64,0x00,
                    0x6c,0x00,0x6c,0x00,0x2c,0x00,0x2d,0x00,0x31,0x00,0x30,0x00,0x31,0x00,0x35,0x00,
                    0x36,0x00,0x00,0x00
                }, RegistryValueKind.Binary);
        }

        // Subclaves de wpifile
        using (RegistryKey defaultIcon = Registry.ClassesRoot.CreateSubKey(@"wpifile\DefaultIcon"))
        {
            defaultIcon.SetValue("", "%1");
        }

        using (RegistryKey shellOpen = Registry.ClassesRoot.CreateSubKey(@"wpifile\shell\open"))
        {
            shellOpen.SetValue("EditFlags", new byte[] { 0x00, 0x00, 0x00, 0x00 }, RegistryValueKind.Binary);
        }

        using (RegistryKey shellOpenCmd = Registry.ClassesRoot.CreateSubKey(@"wpifile\shell\open\command"))
        {
            shellOpenCmd.SetValue("", "\"%1\" %*");
            shellOpenCmd.SetValue("IsolatedCommand", "\"%1\" %*");
        }

        using (RegistryKey shellRunas = Registry.ClassesRoot.CreateSubKey(@"wpifile\shell\runas"))
        {
            shellRunas.SetValue("HasLUAShield", "");
        }

        using (RegistryKey shellRunasCmd = Registry.ClassesRoot.CreateSubKey(@"wpifile\shell\runas\command"))
        {

            shellRunasCmd.SetValue("", "\"%1\" %*");
            shellRunasCmd.SetValue("IsolatedCommand", "\"%1\" %*");
        }

        using (RegistryKey shellRunasUser = Registry.ClassesRoot.CreateSubKey(@"wpifile\shell\runasuser"))
        {
            shellRunasUser.SetValue("", "@shell32.dll,-50944");
            shellRunasUser.SetValue("Extended", "");
            shellRunasUser.SetValue("SuppressionPolicyEx", "{F211AA05-D4DF-4370-A2A0-9F19C09756A7}");
        }

        using (RegistryKey shellRunasUserCmd = Registry.ClassesRoot.CreateSubKey(@"wpifile\shell\runasuser\command"))
        {
            shellRunasUserCmd.SetValue("DelegateExecute", "{ea72d00e-4960-42fa-ba92-7792a7944c1d}");
        }

        // Shellex
        using (RegistryKey shellexCtx = Registry.ClassesRoot.CreateSubKey(@"wpifile\shellex\ContextMenuHandlers"))
        {
            shellexCtx.SetValue("", "Compatibility");
        }

        using (RegistryKey shellexCompat = Registry.ClassesRoot.CreateSubKey(@"wpifile\shellex\ContextMenuHandlers\Compatibility"))
        {
            shellexCompat.SetValue("", "{1d27f844-3a1f-4410-85ac-14651078412d}");
        }

        using (RegistryKey shellexPinto = Registry.ClassesRoot.CreateSubKey(@"wpifile\shellex\ContextMenuHandlers\PintoStartScreen"))
        {
            shellexPinto.SetValue("", "{470C0EBD-5D73-4d58-9CED-E91E22E23282}");
        }

        using (RegistryKey shellexDrop = Registry.ClassesRoot.CreateSubKey(@"wpifile\shellex\DropHandler"))
        {
            shellexDrop.SetValue("", "{86C86720-42A0-1069-A2E8-08002B30309D}");
        }

        using (RegistryKey shellexProp = Registry.ClassesRoot.CreateSubKey(@"wpifile\shellex\PropertySheetHandlers\ShimLayer Property Page"))
        {
            shellexProp.SetValue("", "{513D916F-2A8E-4F51-AEAB-0CBC76FB1AF8}");
        }

        // Registrar extensión .wpi
        using (RegistryKey wpiExt = Registry.ClassesRoot.CreateSubKey(".wpi"))
        {
            wpiExt.SetValue("", "wpifile");
            wpiExt.SetValue("Content Type", "application/x-msdownload");
        }

        using (RegistryKey wpiPersistent = Registry.ClassesRoot.CreateSubKey(@".wpi\PersistentHandler"))
        {
            wpiPersistent.SetValue("", "{098f2470-bae0-11cd-b579-08002b30bfeb}");
        }

        Console.WriteLine("Registro completado correctamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error registrando: " + ex.Message);
        return -1;
    }
    return 0;
}

bool IsRunningAsAdmin()
{
    using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
    {
        WindowsPrincipal principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
}

bool Elevate()
{
    try
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = Process.GetCurrentProcess().MainModule.FileName,
            UseShellExecute = true,
            Verb = "runas" // Esto activa el UAC
        };

        Process.Start(psi);
        return true;
    }
    catch
    {
        Console.WriteLine("El usuario ha cancelado la elevación. Nada que hacer 🙃");
        return false;
    }
}
//#endif