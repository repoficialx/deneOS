using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Windows.Devices.Geolocation;
using Windows.Media.Devices;

public static class AppBarManager
{
    #region Win32
    private const int WM_APP = 
        8000;

    private const int ABM_NEW   =    0x00000000;
    private const int ABM_REMOVE  =  0x00000001;
    private const int ABM_QUERYPOS = 0x00000002;
    private const int ABM_SETPOS  =  0x00000003;

    private const int ABN_POSCHANGED=0x00000001;

    public enum AppBarEdge
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3,
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct APPBARDATA
    {
        public int cbSize;
        public nint hWnd;
        public int uCallbackMessage;
        public AppBarEdge uEdge;
        public RECT rc;
        public nint lParam;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int left, top, right, bottom;
    }

    [DllImport("shell32.dll")]
    private static extern nint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

    #endregion
    // ----------------------------------------------

    public static void Register(Form form, AppBarEdge edge, int thickness)
    {
        var abd = new APPBARDATA
        {
            cbSize = Marshal.SizeOf<APPBARDATA>(),
            hWnd = form.Handle
        };
        SHAppBarMessage(ABM_NEW, ref abd);

        SetPosition(form, edge, thickness);
    }
    public static void Unregister(Form form)
    {
        var abd = new APPBARDATA
        {
            cbSize = Marshal.SizeOf<APPBARDATA>(),
            hWnd = form.Handle
        };

        SHAppBarMessage(ABM_REMOVE, ref abd);
    }
    public static void SetPosition(Form form, AppBarEdge edge, int thickness)
    {
        var screen = Screen.FromHandle(form.Handle);
        var bounds = screen.Bounds;

        var abd = new APPBARDATA
        {
            cbSize = Marshal.SizeOf<APPBARDATA>(),
            hWnd = form.Handle,
            uEdge = edge,
            rc = new RECT
            {
                left = bounds.Left,
                top = bounds.Top,
                right = bounds.Right,
                bottom = bounds.Bottom
            }
        };

        SHAppBarMessage(ABM_QUERYPOS, ref abd);

        switch (edge)
        {
            case AppBarEdge.Top:
                abd.rc.bottom = abd.rc.top + thickness;
                break;

            case AppBarEdge.Bottom:
                abd.rc.bottom = abd.rc.bottom - thickness;
                break;

            case AppBarEdge.Left:
                abd.rc.bottom = abd.rc.left + thickness;
                break;

            case AppBarEdge.Right:
                abd.rc.bottom = abd.rc.right - thickness;
                break;
        }

        SHAppBarMessage(ABM_SETPOS, ref abd);

        form.Bounds = new System.Drawing.Rectangle(
            abd.rc.left,
            abd.rc.top,
            abd.rc.right - abd.rc.left,
            abd.rc.bottom - abd.rc.top
        );
    }
}