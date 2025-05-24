using System;
using System.Runtime.InteropServices;

class BatteryHelper
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
