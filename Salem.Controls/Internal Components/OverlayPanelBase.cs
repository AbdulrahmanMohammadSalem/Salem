using System.Drawing;
using System.Windows.Forms;

namespace Salem.Controls.Internal_Utils {
    internal class OverlayPanelBase : Panel {
        private Color _color;

        internal OverlayPanelBase(Color color) => _color = color;

        protected override CreateParams CreateParams {
            get {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT
                return cp;
            }
        }
        
        protected override void OnPaintBackground(PaintEventArgs e) { }
        
        protected override void OnPaint(PaintEventArgs e) {
            using (SolidBrush _brush = new SolidBrush(_color))
                e.Graphics.FillRectangle(_brush, ClientRectangle);
        }
    }
}
