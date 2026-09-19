using Salem.Drawing;
using Salem.PInvoke;
using System;
using System.Windows.Forms;

namespace Salem.Controls {
    public class SalForm : Form {
        private ColorModes _titleBarColorMode = ColorModes.Light;

        public ColorModes TitleBarColorMode {
            get => _titleBarColorMode;
            set {
                _titleBarColorMode = value;

                if (IsHandleCreated)
                    _ApplyTitleBarTheme();
            }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            _ApplyTitleBarTheme();
        }

        private void _ApplyTitleBarTheme() {
            int useDark = _titleBarColorMode == ColorModes.Dark ? 1 : 0;
            Dwmapi.DwmSetWindowAttribute(Handle, 20, ref useDark, sizeof(int));
        }
    }
}
