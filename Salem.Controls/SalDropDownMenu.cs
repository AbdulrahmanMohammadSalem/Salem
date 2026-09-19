using Salem.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Salem.Controls {
    [ToolboxItem(true)]
    public class SalDropDownMenu : ToolStripDropDown {
        public SalDropDownMenu() {
            Renderer = new OfficeFlatToolStripRenderer(0);
            DropShadowEnabled = false;
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            base.OnPaintBackground(e);

            DrawingHelpers.DrawSimpleBorder(e.Graphics, DrawingHelpers.TranslateOrigin(Bounds), PredefinedColors.OfficeFlat_BorderColor, 2);
        }
    }
}
