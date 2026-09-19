using Salem.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Salem.Controls {
    public class SalSplitButton : Control {
        private Button _buttonPortion, _dropDownPortion;
        private bool _hovering = false, _preventClosing = false;
        private short _dropDownArrowPadding = 0;
        private float _dropDownArrowSize = 6F;
        private BasicDirections _dropDownArrowDirection = BasicDirections.Right;
        private ArrowStyles _dropDownArrowStyle = ArrowStyles.FilledTriangle;
        private ToolStripDropDown _dropDown;

        [DefaultValue(null)]
        public ToolStripDropDown DropDown {
            get => _dropDown;
            set {
                if (_dropDown != value) {
                    if (_dropDown != null) {
                        _dropDown.Closed -= _dropDown_Closed;
                        _dropDown.Closing -= _dropDown_Closing;
                    }
                    
                    _dropDown = value;

                    _dropDown.Closed += _dropDown_Closed;
                    _dropDown.Closing += _dropDown_Closing;
                }
            }
        }

        [DefaultValue((short) 0)]
        public short DropDownArrowPadding {
            get => _dropDownArrowPadding;
            set {
                _dropDownArrowPadding = value;
                _dropDownPortion.Invalidate();
            }
        }

        [DefaultValue(6F)]
        public float DropDownArrowSize {
            get => _dropDownArrowSize;
            set {
                _dropDownArrowSize = value;
                _dropDownPortion.Invalidate();
            }
        }

        [DefaultValue(BasicDirections.Right)]
        public BasicDirections DropDownArrowDirection {
            get => _dropDownArrowDirection;
            set {
                _dropDownArrowDirection = value;
                _dropDownPortion.Invalidate();
            }
        }

        [DefaultValue(ArrowStyles.FilledTriangle)]
        public ArrowStyles DropDownArrowStyle {
            get => _dropDownArrowStyle;
            set {
                _dropDownArrowStyle = value;
                _dropDownPortion.Invalidate();
            }
        }

        [DefaultValue(BasicDirections.Right)]
        public BasicDirections DropDownAlignment { get => (BasicDirections) _dropDownPortion.Dock; set => _dropDownPortion.Dock = (DockStyle) value; }

        [DefaultValue(30)]
        public int DropDownLength { 
            get => _dropDownPortion.Dock == DockStyle.Top || _dropDownPortion.Dock == DockStyle.Bottom ? _dropDownPortion.Height : _dropDownPortion.Width;
            set {
                if (_dropDownPortion.Dock == DockStyle.Top || _dropDownPortion.Dock == DockStyle.Bottom)
                    _dropDownPortion.Height = value;
                else
                    _dropDownPortion.Width = value;
            }
        }

        [DefaultValue((short) 0)]
        public short ButtonPressNudgeAmount { get; set; } = 0;

        public override string Text { get => _buttonPortion.Text; set => _buttonPortion.Text = value; }

        [DefaultValue("")]
        public string DropDownText { get => _dropDownPortion.Text; set => _dropDownPortion.Text = value; }

        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment TextAlign { get => _buttonPortion.TextAlign; set => _buttonPortion.TextAlign = value; }

        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment DropDownTextAlign { get => _dropDownPortion.TextAlign; set => _dropDownPortion.TextAlign = value; }

        [DefaultValue(null)]
        public Image Image { get => _buttonPortion.Image; set => _buttonPortion.Image = value; }

        [DefaultValue(null)]
        public Image DropDownImage { get => _dropDownPortion.Image; set => _dropDownPortion.Image = value; }

        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment ImageAlign { get => _buttonPortion.ImageAlign; set => _buttonPortion.ImageAlign = value; }

        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment DropDownImageAlign { get => _dropDownPortion.ImageAlign; set => _dropDownPortion.ImageAlign = value; }

        /// <summary>
        /// This property is meaningless for this control
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor { get => Parent == null ? Color.Black : Parent.BackColor; set { } }

        public Color ButtonBackColor { get => _buttonPortion.BackColor; set => _buttonPortion.BackColor = value; }

        public Color DropDownBackColor { get => _dropDownPortion.BackColor; set => _dropDownPortion.BackColor = value; }

        [DefaultValue(TextImageRelation.Overlay)]
        public TextImageRelation TextImageRelation { get => _buttonPortion.TextImageRelation; set => _buttonPortion.TextImageRelation = value; }

        
        [DefaultValue(TextImageRelation.Overlay)]
        public TextImageRelation DropDownTextImageRelation { get => _dropDownPortion.TextImageRelation; set => _dropDownPortion.TextImageRelation = value; }

        /// <summary>
        /// This property is meaningless for this control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Padding Padding { get => Padding.Empty; set { } }

        public Padding ButtonPadding { get => _buttonPortion.Padding; set => _buttonPortion.Padding = value; }

        public Padding DropDownPadding { get => _dropDownPortion.Padding; set => _dropDownPortion.Padding = value; }

        public SalSplitButton() => _InitializeComponent();

        private void _InitializeComponent() {
            TabStop = false;

            _buttonPortion = new Button() { Dock = DockStyle.Fill };
            _dropDownPortion = new Button() { Dock = DockStyle.Right, Width = 30 };
            
            _buttonPortion.Margin = _dropDownPortion.Margin = Padding.Empty;
            _buttonPortion.FlatStyle = _dropDownPortion.FlatStyle = FlatStyle.Flat;
            _buttonPortion.FlatAppearance.BorderSize = _dropDownPortion.FlatAppearance.BorderSize = 0;
            _buttonPortion.FlatAppearance.MouseOverBackColor = _dropDownPortion.FlatAppearance.MouseOverBackColor = PredefinedColors.OfficeFlat_HoverBackColor;
            _buttonPortion.FlatAppearance.MouseDownBackColor = _dropDownPortion.FlatAppearance.MouseDownBackColor = PredefinedColors.OfficeFlat_PressBackColor;
            
            _buttonPortion.Paint += _buttonPortion_Paint;
            _buttonPortion.MouseEnter += _buttonPortion_MouseEnter;
            _buttonPortion.MouseLeave += _buttonPortion_MouseLeave;
            _buttonPortion.MouseDown += _buttonPortion_MouseDown;
            _buttonPortion.MouseUp += _buttonPortion_MouseUp;
            _buttonPortion.Click += _buttonPortion_Click;
            
            _dropDownPortion.Paint += _dropDownPortion_Paint;
            _dropDownPortion.MouseEnter += _dropDownPortion_MouseEnter;
            _dropDownPortion.MouseLeave += _dropDownPortion_MouseLeave;
            _dropDownPortion.MouseUp += _dropDownPortion_MouseUp;

            _dropDownPortion.Click += _dropDownPortion_Click; ;
            _dropDownPortion.MouseDown += _dropDownPortion_MouseDown; ;
            
            SuspendLayout();
            Controls.AddRange(new Button[2] { _buttonPortion, _dropDownPortion });
            ResumeLayout();
        }

        private void _buttonPortion_Click(object sender, EventArgs e) => OnClick(e);

        private void _buttonPortion_MouseDown(object sender, MouseEventArgs e) =>
            _buttonPortion.Padding = new Padding(_buttonPortion.Padding.Left, _buttonPortion.Padding.Top + ButtonPressNudgeAmount, _buttonPortion.Padding.Right, _buttonPortion.Padding.Bottom - ButtonPressNudgeAmount);

        private void _dropDownPortion_MouseUp(object sender, MouseEventArgs e) =>
            _dropDownPortion.Padding = new Padding(_dropDownPortion.Padding.Left, _dropDownPortion.Padding.Top - ButtonPressNudgeAmount, _dropDownPortion.Padding.Right, _dropDownPortion.Padding.Bottom + ButtonPressNudgeAmount);

        private void _buttonPortion_MouseUp(object sender, MouseEventArgs e) =>
            _buttonPortion.Padding = new Padding(_buttonPortion.Padding.Left, _buttonPortion.Padding.Top - ButtonPressNudgeAmount, _buttonPortion.Padding.Right, _buttonPortion.Padding.Bottom);

        private void _dropDownPortion_MouseDown(object sender, MouseEventArgs e) {
            if (_dropDown != null) {
                if (_dropDown.Visible)
                    _preventClosing = true;
                else
                    _dropDown.Show(_dropDownPortion, _GetCorrectDropDownPosition());
            }

            _dropDownPortion.Padding = new Padding(_dropDownPortion.Padding.Left, _dropDownPortion.Padding.Top + ButtonPressNudgeAmount, _dropDownPortion.Padding.Right, _dropDownPortion.Padding.Bottom);
        }

        private void _dropDownPortion_Click(object sender, EventArgs e) {
            if (_dropDown != null) {
                if (!_dropDown.Visible)
                    _dropDown.Show(_dropDownPortion, _GetCorrectDropDownPosition());

                _dropDownPortion.BackColor = _dropDownPortion.FlatAppearance.MouseOverBackColor = PredefinedColors.OfficeFlat_PressBackColor;
            }
        }

        private void _dropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e) {
            _dropDownPortion.FlatAppearance.MouseOverBackColor = PredefinedColors.OfficeFlat_HoverBackColor;

            if (ClientRectangle.Contains(PointToClient(Cursor.Position)))
                _dropDownPortion.BackColor = PredefinedColors.OfficeFlat_HoverBackColor;
            else {
                _hovering = false;
                _buttonPortion.BackColor = _dropDownPortion.BackColor = BackColor;
            }
        }

        private void _dropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e) {
            if (_preventClosing) {
                if (e.CloseReason == ToolStripDropDownCloseReason.Keyboard || e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                    _preventClosing = false;
                else
                    e.Cancel = true;
            }
        }

        private Point _GetCorrectDropDownPosition() {
            switch (_dropDownArrowDirection) {
                case BasicDirections.Up: return new Point(0, 0);
                case BasicDirections.Right: return new Point(_dropDownPortion.Width - 2, 0);
                case BasicDirections.Down: return new Point(0, _dropDownPortion.Height - 2);
                case BasicDirections.Left: return new Point(0, 0);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void _dropDownPortion_MouseLeave(object sender, EventArgs e) {
            if (_dropDown != null && _dropDown.Visible)
                _dropDownPortion.BackColor = _dropDownPortion.FlatAppearance.MouseOverBackColor = PredefinedColors.OfficeFlat_PressBackColor;
            else if (ClientRectangle.Contains(PointToClient(Cursor.Position)))
                _dropDownPortion.BackColor = PredefinedColors.OfficeFlat_HoverBackColor;
            else {
                _hovering = false;
                _dropDownPortion.BackColor = BackColor;
            }

            if (_dropDown == null || !_dropDown.Visible)
                _buttonPortion.BackColor = BackColor;
            
            if (_dropDown != null)
                _preventClosing = false;
        }

        private void _dropDownPortion_MouseEnter(object sender, EventArgs e) {
            _dropDownPortion.BackColor = BackColor;
            _buttonPortion.BackColor = PredefinedColors.OfficeFlat_HoverBackColor;

            _invalidateIfNecessary();

            if (_dropDown != null)
                _preventClosing = true;
        }

        private void _buttonPortion_MouseLeave(object sender, EventArgs e) {
            if (ClientRectangle.Contains(PointToClient(Cursor.Position)) || (_dropDown != null && _dropDown.Visible))
                _buttonPortion.BackColor = PredefinedColors.OfficeFlat_HoverBackColor;
            else {
                _hovering = false;
                _buttonPortion.BackColor = BackColor;
            }

            if (_dropDown == null || !_dropDown.Visible)
                _dropDownPortion.BackColor = BackColor;
        }

        private void _buttonPortion_MouseEnter(object sender, EventArgs e) {
            _buttonPortion.BackColor = BackColor;
            _dropDownPortion.BackColor = (_dropDown != null && _dropDown.Visible) ? PredefinedColors.OfficeFlat_PressBackColor : PredefinedColors.OfficeFlat_HoverBackColor;

            _invalidateIfNecessary();
        }

        private void _invalidateIfNecessary() {
            if (!_hovering) {
                _hovering = true;

                _buttonPortion.Invalidate();
                _dropDownPortion.Invalidate();
            }
        }

        private void _dropDownPortion_Paint(object sender, PaintEventArgs e) {
            if (_hovering)
                DrawingHelpers.DrawSimpleBorder(e.Graphics, _dropDownPortion.ClientRectangle, PredefinedColors.OfficeFlat_BorderColor, 2);

            char _arrowChar = DrawingHelpers.GetArrowChar(_dropDownArrowDirection, _dropDownArrowStyle);

            using (var _brush = new SolidBrush(PredefinedColors.OfficeFlat_ArrowColor))
            using (var _arrowCharFont = new Font("Segoe MDL2 Assets", _dropDownArrowSize))
            using (var _stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                e.Graphics.DrawString(_arrowChar.ToString(), _arrowCharFont, _brush, _CalculateArrowRect(e.Graphics, _arrowChar, _arrowCharFont), _stringFormat);
        }

        private RectangleF _CalculateArrowRect(Graphics g, char arrowChar, Font arrowCharFont) {
            if (_dropDownPortion.Text == string.Empty && _dropDownPortion.Image == null)
                return new RectangleF(0, 1, _dropDownPortion.ClientSize.Width, _dropDownPortion.ClientSize.Height);
            
            float _arrowSize = g.MeasureString(arrowChar.ToString(), arrowCharFont).Height;

            switch (_dropDownPortion.Dock) {
                case DockStyle.Top: return new RectangleF(0, _dropDownArrowPadding + 1, _dropDownPortion.ClientSize.Width, _arrowSize);
                case DockStyle.Right: return new RectangleF(_dropDownPortion.ClientSize.Width - DropDownArrowPadding - _arrowSize - 1, 1, _arrowSize, _dropDownPortion.ClientSize.Height);
                case DockStyle.Bottom: return new RectangleF(0, _dropDownPortion.ClientSize.Height - _dropDownArrowPadding - _arrowSize, _dropDownPortion.ClientSize.Width, _arrowSize);
                case DockStyle.Left: return new RectangleF(DropDownArrowPadding, 1, _arrowSize, _dropDownPortion.ClientSize.Height);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void _buttonPortion_Paint(object sender, PaintEventArgs e) {
            if (_hovering) {
                switch (_dropDownPortion.Dock) {
                    case DockStyle.Top:
                        e.Graphics.DrawLines(new Pen(PredefinedColors.OfficeFlat_BorderColor, 2), new Point[4] { new Point(1, 0), new Point(1, _buttonPortion.ClientSize.Height - 1), new Point(_buttonPortion.ClientSize.Width - 1, _buttonPortion.ClientSize.Height - 1), new Point(_buttonPortion.ClientSize.Width - 1, 0) });
                        break;
                    case DockStyle.Bottom:
                        e.Graphics.DrawLines(new Pen(PredefinedColors.OfficeFlat_BorderColor, 2), new Point[4] { new Point(1, _buttonPortion.ClientSize.Height), new Point(1, 1), new Point(_buttonPortion.ClientSize.Width - 1, 1), new Point(_buttonPortion.ClientSize.Width - 1, _buttonPortion.ClientSize.Height) });
                        break;
                    case DockStyle.Left:
                        e.Graphics.DrawLines(new Pen(PredefinedColors.OfficeFlat_BorderColor, 2), new Point[4] { new Point(0, 1), new Point(_buttonPortion.ClientSize.Width - 1, 1), new Point(_buttonPortion.ClientSize.Width - 1, _buttonPortion.ClientSize.Height - 1), new Point(0, _buttonPortion.ClientSize.Height - 1) });
                        break;
                    case DockStyle.Right:
                        e.Graphics.DrawLines(new Pen(PredefinedColors.OfficeFlat_BorderColor, 2), new Point[4] { new Point(_buttonPortion.ClientSize.Width, 1), new Point(1, 1), new Point(1, _buttonPortion.ClientSize.Height - 1), new Point(_buttonPortion.ClientSize.Width, _buttonPortion.ClientSize.Height - 1) });
                        break;
                }
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _buttonPortion.Paint -= _buttonPortion_Paint;
                _buttonPortion.MouseEnter -= _buttonPortion_MouseEnter;
                _buttonPortion.MouseLeave -= _buttonPortion_MouseLeave;
                _buttonPortion.Click -= _buttonPortion_Click;
                _buttonPortion.MouseDown -= _buttonPortion_MouseDown;
                _buttonPortion.MouseUp -= _buttonPortion_MouseUp;

                _dropDownPortion.Paint -= _dropDownPortion_Paint;
                _dropDownPortion.MouseEnter -= _dropDownPortion_MouseEnter;
                _dropDownPortion.MouseLeave -= _dropDownPortion_MouseLeave;
                _dropDownPortion.Click -= _dropDownPortion_Click;
                _dropDownPortion.MouseDown -= _dropDownPortion_MouseDown;
                _dropDownPortion.MouseUp -= _dropDownPortion_MouseUp;

                _buttonPortion.Dispose();
                _dropDownPortion.Dispose();
                
                _buttonPortion = _dropDownPortion = null;

                if (_dropDown != null) {
                    _dropDown.Closed -= _dropDown_Closed;
                    _dropDown.Closing -= _dropDown_Closing;
                    _dropDown = null;
                    //We mustn't dispose of the _dropDown because we don't own it.
                }
            }

            base.Dispose(disposing);
        }
    }
}
