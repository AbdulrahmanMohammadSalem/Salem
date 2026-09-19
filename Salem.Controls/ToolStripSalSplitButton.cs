using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;

namespace Salem.Controls {
    [ToolboxItem(false)]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripSalSplitButton : ToolStripControlHost {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SalSplitButton InnerSplitButton => Control as SalSplitButton;

        public ToolStripSalSplitButton() : base(new SalSplitButton()) {
            AutoSize = false;
            Size = new Size(90, 40);
        }
    }
}
