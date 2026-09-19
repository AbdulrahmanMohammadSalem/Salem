using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Salem.Drawing;

namespace Salem.Controls {
    public class OfficeFlatToolStripRenderer : ToolStripRenderer {
        [DefaultValue((short) 0)]
        public short ButtonPressNudgeAmount { get; set; } = 0;

        public OfficeFlatToolStripRenderer(short buttonPressNudgeAmount = 0) => ButtonPressNudgeAmount = buttonPressNudgeAmount;

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e) {
            var _btn = e.Item as ToolStripButton;

            if (_btn.Pressed) {
                DrawingHelpers.FillRect(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_PressBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_BorderColor, 2);
            } else if (_btn.Selected) {
                DrawingHelpers.FillRect(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_HoverBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_BorderColor, 2);
            } else
                base.OnRenderButtonBackground(e);
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e) {
            var _btn = e.Item as ToolStripDropDownButton;

            if (_btn.Pressed) {
                DrawingHelpers.FillRect(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_PressBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_BorderColor, 2);
            } else if (_btn.Selected) {
                DrawingHelpers.FillRect(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_HoverBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_BorderColor, 2);
            } else
                base.OnRenderDropDownButtonBackground(e);

            if (_btn.DisplayStyle == ToolStripItemDisplayStyle.None)
                _DrawDropDownArrow(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds));
            else
                _DrawDropDownArrow(e.Graphics, new Rectangle(0, _btn.Height - 26, _btn.Width, 26));
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e) {
            var _btn = e.Item as ToolStripSplitButton;
            bool _alreadyPainted = false;

            if (_btn.ButtonPressed) {
                DrawingHelpers.FillRect(e.Graphics, _btn.ButtonBounds, PredefinedColors.OfficeFlat_PressBackColor);
                DrawingHelpers.FillRect(e.Graphics, _btn.DropDownButtonBounds, PredefinedColors.OfficeFlat_HoverBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_BorderColor, 2);

                DrawingHelpers.FillRect(e.Graphics, _btn.SplitterBounds, PredefinedColors.OfficeFlat_BorderColor);
                e.Graphics.DrawLine(new Pen(PredefinedColors.OfficeFlat_BorderColor), _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Top, _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Bottom);

                _alreadyPainted = true;
            } else if (_btn.DropDownButtonPressed) {
                DrawingHelpers.FillRect(e.Graphics, _btn.ButtonBounds, PredefinedColors.OfficeFlat_HoverBackColor);
                DrawingHelpers.FillRect(e.Graphics, _btn.DropDownButtonBounds, PredefinedColors.OfficeFlat_PressBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_BorderColor, 2);

                DrawingHelpers.FillRect(e.Graphics, _btn.SplitterBounds, PredefinedColors.OfficeFlat_BorderColor);
                e.Graphics.DrawLine(new Pen(PredefinedColors.OfficeFlat_BorderColor), _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Top, _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Bottom);

                _alreadyPainted = true;
            }

            if (_btn.Selected) {
                if (!_alreadyPainted) {
                    DrawingHelpers.FillRect(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_HoverBackColor);
                    DrawingHelpers.DrawSimpleBorder(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), PredefinedColors.OfficeFlat_BorderColor, 2);
                    DrawingHelpers.FillRect(e.Graphics, _btn.SplitterBounds, PredefinedColors.OfficeFlat_BorderColor);
                    e.Graphics.DrawLine(new Pen(PredefinedColors.OfficeFlat_BorderColor), _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Top, _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Bottom);
                }
            } else
                base.OnRenderSplitButtonBackground(e);
            
            Rectangle _dropDownBounds = _btn.DropDownButtonBounds;
            _dropDownBounds.Width--;
            _DrawDropDownArrow(e.Graphics, _dropDownBounds);
        }

        private void _DrawDropDownArrow(Graphics graphics, Rectangle _dropDownBounds) {
            using (var _brush = new SolidBrush(PredefinedColors.OfficeFlat_ArrowColor))
            using (var _font = new Font("Segoe MDL2 Assets", 5))
            using (var _stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                graphics.DrawString(DrawingHelpers.GetArrowChar(BasicDirections.Down, ArrowStyles.ChevronThick).ToString(), _font, _brush, _dropDownBounds, _stringFormat);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) => DrawingHelpers.FillRect(e.Graphics, e.Item.Bounds, e.Item.ForeColor);

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
            if (e.Item.OwnerItem != null)
                e.Item.ForeColor = e.Item.OwnerItem.ForeColor;

            if (e.Item.Selected) {
                DrawingHelpers.FillRect(e.Graphics, e.Item.Bounds, PredefinedColors.OfficeFlat_HoverBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, DrawingHelpers.TranslateOrigin(e.Item.Bounds), PredefinedColors.OfficeFlat_BorderColor, 2);
            } else
                base.OnRenderMenuItemBackground(e);
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e) {
            if (e.Item.Pressed || (e.Item is ToolStripSplitButton _btn && _btn.ButtonPressed))
                base.OnRenderItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, e.Item, new Rectangle(e.ImageRectangle.X, e.ImageRectangle.Y + ButtonPressNudgeAmount, e.ImageRectangle.Width, e.ImageRectangle.Height)));
            else
                base.OnRenderItemImage(e);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
            if (e.Item.Pressed || (e.Item is ToolStripSplitButton _btn && _btn.ButtonPressed))
                e.TextRectangle = new Rectangle(e.TextRectangle.X, e.TextRectangle.Y + ButtonPressNudgeAmount, e.TextRectangle.Width, e.TextRectangle.Height);
            
            base.OnRenderItemText(e);
        }
    }
}
