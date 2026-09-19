using Salem.Drawing;
using System;
using System.Windows.Forms;

namespace Salem.Controls {
    public class SalButton : Button {
        private enum States : byte { Normal, Hover, Pressed }

        private States _currentState = States.Normal;

        public short NudgeAmountOnPress { get; set; } = 1;

        public SalButton() {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.MouseOverBackColor = PredefinedColors.OfficeFlat_HoverBackColor;
            FlatAppearance.MouseDownBackColor = PredefinedColors.OfficeFlat_PressBackColor;
            FlatAppearance.BorderSize = 0;
        }

        protected override void OnPaint(PaintEventArgs pevent) {
            base.OnPaint(pevent);
        
            if (_currentState == States.Normal)
                DrawingHelpers.DrawSimpleBorder(pevent.Graphics, new System.Drawing.Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1), PredefinedColors.OfficeFlat_SoftBorderColor, 1);
            else
                DrawingHelpers.DrawSimpleBorder(pevent.Graphics, DrawingHelpers.TranslateOrigin(Bounds), PredefinedColors.OfficeFlat_BorderColor, 2);
        }

        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);

            _currentState = States.Hover;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);

            _currentState = States.Normal;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent) {
            base.OnMouseDown(mevent);
            Padding = new Padding(Padding.Left, Padding.Top + NudgeAmountOnPress, Padding.Right, Padding.Bottom);
        }

        protected override void OnMouseUp(MouseEventArgs mevent) {
            base.OnMouseUp(mevent);
            Padding = new Padding(Padding.Left, Padding.Top - NudgeAmountOnPress, Padding.Right, Padding.Bottom);
        }
    }
}
