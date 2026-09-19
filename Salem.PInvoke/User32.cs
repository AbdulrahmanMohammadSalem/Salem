using System;
using System.Runtime.InteropServices;

namespace Salem.PInvoke {
    /// <summary>
    /// Provides P/Invoke signatures for functions in the Windows User32 library (user32.dll).
    /// </summary>
    public static class User32 {
        /// <summary>
        /// Specifies the name of the Windows User32 dynamic-link library (DLL).
        /// </summary>
        /// <remarks>This constant is typically used when invoking native methods from user32.dll via
        /// platform invocation (P/Invoke).</remarks>
        public const string ASSEMBLY_NAME = "user32.dll";

        /// <summary>
        /// Removes the caret from the screen. Hiding a caret does not destroy its current shape or invalidate the insertion point.<br/>
        /// Click <a href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-hidecaret">here</a> for the full documentation.
        /// </summary>
        /// <param name="hWnd">A handle to the window that owns the caret. If this parameter is NULL, HideCaret searches the current task for the window that owns the caret.</param>
        /// <returns>true if the function succeeded, otherwise, false.<br/>
        /// To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// HideCaret hides the caret only if the specified window owns the caret. If the specified window does not own the caret, HideCaret does nothing and returns FALSE.<br/>
        /// Hiding is cumulative. If your application calls HideCaret five times in a row, it must also call ShowCaret five times before the caret is displayed.<br/>
        /// For an example, see <a href="https://learn.microsoft.com/en-us/windows/desktop/menurc/using-carets">Hiding a Caret</a>.
        /// </remarks>
        [DllImport(ASSEMBLY_NAME)]
        public static extern bool HideCaret(IntPtr hWnd);

        [DllImport(ASSEMBLY_NAME, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
    }
}
