using System;
using System.Runtime.InteropServices;

class Program {
    [StructLayout(LayoutKind.Sequential)]
    struct APPBARDATA {
        public uint cbSize;
        public IntPtr hWnd;
        public uint uCallbackMessage;
        public uint uEdge;
        public System.Drawing.Rectangle rc;
        public int lParam;
    }

    const uint ABM_GETSTATE = 0x00000004;
    const uint ABM_SETSTATE = 0x0000000A;
    const int ABS_AUTOHIDE = 0x01;

    [DllImport("shell32.dll")]
    static extern uint SHAppBarMessage(uint dwMessage, ref APPBARDATA pData);

    static void Main() {
        var data = new APPBARDATA();
        data.cbSize = (uint)Marshal.SizeOf(data);
        uint state = SHAppBarMessage(ABM_GETSTATE, ref data);
        data.lParam = (state & ABS_AUTOHIDE) != 0 ? 0 : ABS_AUTOHIDE;
        SHAppBarMessage(ABM_SETSTATE, ref data);
    }
}