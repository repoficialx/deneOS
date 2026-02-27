using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Sys = System;

namespace dosu.System;

public static class Initializers
{
    public static void StartdO(string args = "")
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = @"C:\DENEOS\core\deneOS.exe",
            Arguments = args,
            UseShellExecute = true,
            WorkingDirectory = @"C:\DENEOS\core",
            Verb = "runas" // Ejecutar como administrador
        };
        try
        {
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al iniciar deneOS: {ex.Message}", 0x01, 0x01, "Error de Inicio");
        }
    }
    public static void StartCC()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = @"C:\DENEOS\systemApps\controlcenter.exe",
            UseShellExecute = true,
            WorkingDirectory = @"C:\DENEOS\systemApps",
            Verb = "runas" // Ejecutar como administrador
        };
        try
        {
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al iniciar el Centro de Control: {ex.Message}", 0x01, 0x01, "Error de Inicio");
        }
    }
    public static void UpdatedO(string url)
    {
        // Iniciar el Control Center con argumento /installUpdate: seguido de la URL de descarga
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = @"C:\DENEOS\systemApps\controlcenter.exe",
            Arguments = $"/installUpdate:{url}",
            UseShellExecute = true,
            WorkingDirectory = @"C:\DENEOS\systemApps",
            Verb = "runas" // Ejecutar como administrador
        };
        try
        {
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al iniciar el Control Center para la actualización: {ex.Message}", 0x01, 0x01, "Error de Inicio");
        }
    }
    public static void StartDF(bool root = false, string path = "USER")
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = @"C:\DENEOS\systemApps\deneFiles\deneFiles.exe",
            UseShellExecute = true,
            Arguments = root ? "/dangerZone:enableRoot" : "",
            WorkingDirectory = @"C:\DENEOS\systemApps",
            Verb = "runas" // Ejecutar como administrador
        };
        try
        {
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al iniciar deneFiles: {ex.Message}", 0x01, 0x01, "Error de Inicio");
        }
    }
    public static void StartDN()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = @"C:\DENEOS\systemApps\deneNotes\deneNotes.exe",
            UseShellExecute = true,
            WorkingDirectory = @"C:\DENEOS\systemApps",
            Verb = "runas" // Ejecutar como administrador
        };
        try
        {
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al iniciar deneNotes: {ex.Message}", 0x01, 0x01, "Error de Inicio");
        }
    }
    public static void KilldO()
    {
        try
        {
            Process[] processes = Process.GetProcessesByName("deneOS");
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al cerrar deneOS: {ex.Message}", 0x01, 0x01, "Error de Cierre");
        }
    }
    public static void KillAll()
    {
        string[] processNames = {
            "deneOS",
            "controlcenter",
            "deneFiles",
            "deneNotes"
        };
        foreach (string processName in processNames)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                Utils.SysMsg($"Error al cerrar {processName}: {ex.Message}", 0x01, 0x01, "Error de Cierre");
            }
        }
    }
}

public static class Power
{
    public static class BatteryStatus
    {
        [DllImport("powrprof.dll", SetLastError = true)]
        static extern uint PowerGetActiveScheme(IntPtr UserRootPowerKey, out IntPtr ActivePolicyGuid);

        [DllImport("powrprof.dll", SetLastError = true)]
        static extern uint PowerReadACValue(
            IntPtr RootPowerKey,
            ref Guid SchemeGuid,
            ref Guid SubGroupOfPowerSettingsGuid,
            ref Guid PowerSettingGuid,
            out uint Type,
            IntPtr Buffer,
            ref uint BufferSize);

        static Guid GUID_POWER_SAVING = new Guid("DE830923-A562-41AF-A086-E3A2C6BAD2DA"); // ahorro de batería
        static Guid GUID_ENERGY_SAVER = new Guid("E69653CA-CF7F-4F05-AA73-CB833FA90AD4"); // modo de energía

        public static bool IsBatterySaverOn()
        {
            IntPtr pActiveScheme;
            uint res = PowerGetActiveScheme(IntPtr.Zero, out pActiveScheme);
            if (res != 0) return false;

            Guid activeScheme = (Guid)Marshal.PtrToStructure(pActiveScheme, typeof(Guid));
            uint valType = 0;
            uint buffSize = 4;

            // Esto se usa para leer el estado del ahorro
            uint result = PowerReadACValue(IntPtr.Zero,
                ref activeScheme,
                ref GUID_POWER_SAVING,
                ref GUID_ENERGY_SAVER,
                out valType,
                IntPtr.Zero,
                ref buffSize);

            return result == 0 && valType == 0; // Si devuelve 0 y tipo 0, normalmente es ON
        }
    }
}

public static class BrightnessMGMT
{
    [DllImport("gdi32.dll")]
    private static extern bool SetDeviceGammaRamp(IntPtr hdc, ref RAMP lpRamp);
    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool GetMonitorBrightness(IntPtr hMonitor, out uint minimumBrightness, out uint currentBrightness, out uint maximumBrightness);

    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetDesktopWindow();

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]

    private struct RAMP
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] Red;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] Green;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] Blue;
    }
    public static bool isLaptop()
    {
        try
        {
            using var process = new Process();
            process.StartInfo.FileName = "powershell";
            process.StartInfo.Arguments = "-Command \"(Get-CimInstance -ClassName Win32_SystemEnclosure).ChassisTypes\"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // Chassis types para laptops: 8 (portable), 9 (laptop), 10 (notebook), 14 (sub notebook)
            var laptopTypes = new[] { "8", "9", "10", "14" };

            foreach (var type in laptopTypes)
                if (output.Contains(type))
                    return true;
        }
        catch { }

        return false;
    }
    public static void SetBrightness(int brightness)
    {
        if (isLaptop())
        {
            BrightnessLaptopMGMT.SetBrightness((byte)brightness);
            return;
        }
        IntPtr hdc = GetDC(IntPtr.Zero);
        try
        {
            RAMP ramp = new RAMP
            {
                Red = new byte[256],
                Green = new byte[256],
                Blue = new byte[256]
            };

            int adjustedBrightness = (brightness * 256) / 100;

            for (int i = 0; i < 256; i++)
            {
                ushort temp = (ushort)(i * adjustedBrightness);
                byte value = (byte)Sys.Math.Min((ushort)255, (ushort)temp);
                ramp.Red[i] = ramp.Green[i] = ramp.Blue[i] = value;
            }

            SetDeviceGammaRamp(hdc, ref ramp);
        }
        finally
        {
            ReleaseDC(IntPtr.Zero, hdc);
        }
    }
    public static int? GetBrightness()
    {
        if (isLaptop())
        {
            return BrightnessLaptopMGMT.GetBrightness();
        }
        IntPtr desktopWindow = GetDesktopWindow();
        IntPtr monitor = MonitorFromWindow(desktopWindow, 0);

        if (monitor == IntPtr.Zero)
        {
            Console.WriteLine("Failed to get monitor handle.");
            return -1;
        }

        if (GetMonitorBrightness(monitor, out uint min, out uint current, out uint max))
        {
            Console.WriteLine($"Brightness: {current} (Min: {min}, Max: {max})");
            return Convert.ToInt32(current);
        }
        else
        {
            Console.WriteLine("Failed to retrieve brightness.");
            return -2;
        }
    }
}
public static class BrightnessLaptopMGMT
{
    public static int? GetBrightness()
    {
        try
        {
            using Process process = new Process();
            process.StartInfo.FileName = "powershell";
            process.StartInfo.Arguments = "-Command \"(Get-WmiObject -Namespace root/wmi -Class WmiMonitorBrightness).CurrentBrightness\"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            //process.WaitForExit();

            if (int.TryParse(output.Trim(), out int brightness))
                return brightness;
        }
        catch { }

        return -1;
    }

    public static void SetBrightness(byte brightness)
    {
        try
        {
            using Process process = new Process();
            process.StartInfo.FileName = "powershell";
            process.StartInfo.Arguments = $"-Command \"(Get-WmiObject -Namespace root/wmi -Class WmiMonitorBrightnessMethods).WmiSetBrightness(1,{brightness})\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            process.Start();
            //process.WaitForExit();
        }
        catch (Exception ex)
        {
            throw new Exception("ERROR: " + ex.Message);
        }

    }

    public static async Task SetBrightnessAsync(byte brightness)
    {
        await Task.Run(() =>
        {
            using Process process = new Process();
            process.StartInfo.FileName = "powershell";
            process.StartInfo.Arguments =
                $"-WindowStyle Hidden -Command \"(Get-CimInstance -Namespace root/wmi -ClassName WmiMonitorBrightnessMethods).WmiSetBrightness(1,{brightness})\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        });
    }

}

public static class Network
{
    public static class WiFiStatus
    {
        public static async Task<int> GetWifiSignalStrengthAsync()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = "wlan show interfaces",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                string output;
                using (Process process = Process.Start(psi))
                {
                    // Leer la salida de forma asíncrona para evitar el interbloqueo
                    output = await process.StandardOutput.ReadToEndAsync();
                    await Task.Run(() => process.WaitForExit());
                }

                Console.Write(output);
                Match match = Regex.Match(output, @"Señal\s*:\s*(\d+)%");
                if (match.Success)
                {
                    int signalStrength = int.Parse(match.Groups[1].Value);
                    Console.WriteLine($"{MUI.T("wifistrength")} {signalStrength}%");
                    return signalStrength;
                }
                else
                {
                    Console.WriteLine(MUI.T("nowifisignal"));
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la señal de WiFi: {ex.Message}");
                return -1;
            }
        }
    }
}