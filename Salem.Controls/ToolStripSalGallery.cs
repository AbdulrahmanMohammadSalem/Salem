using Salem.Drawing;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Salem.Controls {
    [ToolboxItem(false)]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripSalGallery : ToolStripControlHost {
        private readonly GenericControlCollection<SalButton> _items;
        private bool _useRoundedCorners = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GenericControlCollection<SalButton> Items => _items;

        public bool UseRoundedCorners {
            get => _useRoundedCorners;
            set {
                _useRoundedCorners = value;
                Invalidate();
            }
        }

        public override Padding Padding { get => Control.Padding; set => Control.Padding = value; }

        public ToolStripSalGallery() : base(new BufferedFlowLayoutPanel() { AutoScroll = true })  {
            _items = new GenericControlCollection<SalButton>(Control);
            AutoSize = false;
            Size = new Size(220, 100);
            BackColor = Color.White;
            
        }

        protected override void OnPaint(PaintEventArgs e) {
            if (_useRoundedCorners) {
                DrawingHelpers.DrawCustomShape(Control, e.Graphics, new ShapeInfo() {
                    BorderColor = PredefinedColors.OfficeFlat_SoftBorderColor,
                    BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid,
                    BorderSize = 1,
                    BottomLeftRadius = 12,
                    BottomRightRadius = 12,
                    TopLeftRadius = 12,
                    TopRightRadius = 12
                });
            } else
                DrawingHelpers.DrawSimpleBorder(e.Graphics, new Rectangle(0, 0, Control.ClientRectangle.Width - 1, Control.ClientRectangle.Height - 1), PredefinedColors.OfficeFlat_SoftBorderColor, 1);
        }

        protected override void OnSubscribeControlEvents(Control control) {
            base.OnSubscribeControlEvents(control);
            BufferedFlowLayoutPanel _innerPanel = (BufferedFlowLayoutPanel) control;

            _innerPanel.Scroll += ToolStripSalGallery_Scroll;
            _innerPanel.MouseWheel += ToolStripSalGallery_MouseWheel;
        }

        private void ToolStripSalGallery_MouseWheel(object sender, MouseEventArgs e) {
            Invalidate();
        }

        protected override void OnUnsubscribeControlEvents(Control control) {
            base.OnUnsubscribeControlEvents(control);
            BufferedFlowLayoutPanel _innerPanel = (BufferedFlowLayoutPanel) control;

            _innerPanel.Scroll -= ToolStripSalGallery_Scroll;
            _innerPanel.MouseWheel -= ToolStripSalGallery_MouseWheel;
        }

        private void ToolStripSalGallery_Scroll(object sender, ScrollEventArgs e) {
            Invalidate();
        }

        public class BufferedFlowLayoutPanel : FlowLayoutPanel {
            public BufferedFlowLayoutPanel() {
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
                UpdateStyles();
            }

            protected override CreateParams CreateParams {
                get {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                    return cp;
                }
            }
        }
    }
}
