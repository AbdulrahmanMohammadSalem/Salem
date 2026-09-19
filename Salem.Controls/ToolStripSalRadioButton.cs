using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Salem.Controls {
    [ToolboxItem(false)]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripSalRadioButton : ToolStripButton {
        private Color _accentColor = Color.FromArgb(16, 110, 190);
        private byte _paddingSpaces = 6;

        public byte TextPadding {
            get => _paddingSpaces;
            set {
                _paddingSpaces = value;
                Text = Text;
            }
        }

        public override string Text {
            get => base.Text;
            set {
                string _trimmed = value.TrimStart();
                base.Text = _trimmed.PadLeft(_paddingSpaces + _trimmed.Length);
            }
        }

        public Color AccentColor {
            get => _accentColor;
            set {
                _accentColor = value;
                Invalidate();
            }
        }

        public new bool Checked {
            get => base.Checked;
            set {
                if (value == base.Checked)
                    return;

                base.Checked = value;

                if (value) {
                    var _otherCheckedBtn = Owner.Items.OfType<ToolStripSalRadioButton>().FirstOrDefault(btn => btn != this && btn.Checked);

                    if (_otherCheckedBtn != null)
                        _otherCheckedBtn.Checked = false;
                }
            }
        }

        public ToolStripSalRadioButton() {
            CheckOnClick = true;
            Margin = new Padding(2, 0, 2, 0);
            TextAlign = ContentAlignment.MiddleLeft;
            DisplayStyle = ToolStripItemDisplayStyle.Text;
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            using (var _font = new Font("Segoe Fluent Icons", Font.Size))
            using (var _brush = new SolidBrush(_accentColor))
            using (var _stringFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }) {
                if (Checked)
                    e.Graphics.DrawString("\ueccb", _font, _brush, new Rectangle(4, 2, Padding.Left, Height - 2), _stringFormat);
                else
                    e.Graphics.DrawString("\uecca", _font, _brush, new Rectangle(4, 2, Padding.Left, Height - 2), _stringFormat);
            }
        }

        protected override void OnClick(EventArgs e) {
            if (Checked)
                return; //Do nothing if already checked.

            var _otherCheckedBtn = Owner.Items.OfType<ToolStripSalRadioButton>().FirstOrDefault(btn => btn != this && btn.Checked);

            if (_otherCheckedBtn != null)
                _otherCheckedBtn.Checked = false;

            base.OnClick(e);
        }
    }
}
