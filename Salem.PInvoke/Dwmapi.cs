using System;
using System.Runtime.InteropServices;

namespace Salem.PInvoke {
    public static class Dwmapi {
        public const string AssemblyName = "dwmapi.dll";

        [DllImport(AssemblyName)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);



    }
}
