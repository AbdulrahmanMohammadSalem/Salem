using Salem.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Salem.Controls {
    [ToolboxItem(true)]
    public class SalContextMenuStrip : ContextMenuStrip{
        public SalContextMenuStrip() {
            Renderer = new OfficeFlatToolStripRenderer(0);
            DropShadowEnabled = false;
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            base.OnPaintBackground(e);

            DrawingHelpers.DrawSimpleBorder(e.Graphics, DrawingHelpers.TranslateOrigin(Bounds), PredefinedColors.OfficeFlat_BorderColor, 2);
        }
    }
}
