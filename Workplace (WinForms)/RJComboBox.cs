using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.ListView;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace Workplace__WinForms_ {
    [DefaultEvent("SelectedIndexChanged")]
    public class RJComboBox : UserControl {
        //Fields:
        private Color _backColor = Color.WhiteSmoke;
        private Color _iconColor = Color.MediumSlateBlue;
        private Color _listBackColor = Color.FromArgb(230, 228, 245);
        private Color _listTextColor = Color.DimGray;
        private Color _borderColor = Color.MediumSlateBlue;
        private int _borderSize = 1;

        //Items:
        private ComboBox _innerComboBox;
        private Label _lblText;
        private Button _btnIcon;

        //Properties:
        public new Color BackColor { get => _backColor; set => _lblText.BackColor = _btnIcon.BackColor = _backColor = value; }
        public Color IconColor { 
            get => _iconColor;
            set { 
                _iconColor = value;
                _btnIcon.Invalidate();
            }
        }
        public Color ListBackColor { get => _listBackColor; set => _innerComboBox.BackColor = _listBackColor = value; }
        public Color ListTextColor { get => _listTextColor; set => _innerComboBox.ForeColor = _listTextColor = value; }
        public Color BorderColor { get => _borderColor; set => base.BackColor = _borderColor = value; }
        public int BorderSize { 
            get => _borderSize;
            set {
                Padding = new Padding(_borderSize = value);
                _AdjustInnerComboBoxDimensions();
            }
        }
        public override Color ForeColor { get => base.ForeColor; set => _lblText.ForeColor = base.ForeColor = value; }
        public override Font Font { get => base.Font; set => _lblText.Font = _innerComboBox.Font = base.Font = value; }
        public override string Text { get => _lblText.Text; set => _lblText.Text = value; }
        public ComboBoxStyle DropDownStyle { 
            get => _innerComboBox.DropDownStyle;
            set {
                if (value != ComboBoxStyle.Simple)
                    _innerComboBox.DropDownStyle = value;
            }
        }

        //Data:
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        public ComboBox.ObjectCollection Items => _innerComboBox.Items;

        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [AttributeProvider(typeof(IListSource))]
        public object DataSource { get => _innerComboBox.DataSource; set => _innerComboBox.DataSource = value; }

        public AutoCompleteMode AutoCompleteMode { get => _innerComboBox.AutoCompleteMode; set => _innerComboBox.AutoCompleteMode = value; }
        public AutoCompleteSource AutoCompleteSource { get => _innerComboBox.AutoCompleteSource; set => _innerComboBox.AutoCompleteSource = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public AutoCompleteStringCollection AutoCompleteCustomSource { get => _innerComboBox.AutoCompleteCustomSource; set => _innerComboBox.AutoCompleteCustomSource = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex { get => _innerComboBox.SelectedIndex; set => _innerComboBox.SelectedIndex = value; }

        [Browsable(false)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem { get => _innerComboBox.SelectedItem; set => _innerComboBox.SelectedItem = value; }

        //Events:
        public event EventHandler SelectedIndexChanged;
        
        //Constructor:
        public RJComboBox() {
            //Inner ComboBox:
            _innerComboBox = new ComboBox { BackColor = _listBackColor, Font = new Font(Font.Name, 10F), ForeColor = _listTextColor };
            _innerComboBox.SelectedIndexChanged += _innerComboBox_SelectedIndexChanged;
            _innerComboBox.TextChanged += _innerComboBox_TextChanged;

            //Inner Button:
            _btnIcon = new Button { Dock = DockStyle.Right, FlatStyle = FlatStyle.Flat, BackColor = _backColor, Size = new Size(31, 31) };
            _btnIcon.FlatAppearance.BorderSize = 0;
            _btnIcon.Click += _btnIcon_Click;
            _btnIcon.Paint += _btnIcon_Paint;

            //Inner TextBox:
            _lblText = new Label() { Dock = DockStyle.Fill, AutoSize = false, BackColor = _backColor, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(8, 0, 0, 0), Font = new Font(Font.Name, 10F) };
            _lblText.Click += _surface_Click;

            SuspendLayout();

            Controls.Add(_lblText);
            Controls.Add(_btnIcon);
            Controls.Add(_innerComboBox);
            MinimumSize = new Size(31, 31);
            Size = new Size(200, 31);
            ForeColor = Color.DimGray;
            Padding = new Padding(_borderSize);
            base.BackColor = _borderColor;

            ResumeLayout();
            _AdjustInnerComboBoxDimensions();
        }

        //Private Methods:
        private void _AdjustInnerComboBoxDimensions() {
            _innerComboBox.DropDownWidth = _innerComboBox.Width = Width - Padding.Horizontal;
            _innerComboBox.Location = new Point(Padding.Left, Height - Padding.Bottom - _innerComboBox.Height);
        }

        //Event Methods:
        private void _surface_Click(object sender, EventArgs e) {
            _innerComboBox.Select();

            if (_innerComboBox.DropDownStyle == ComboBoxStyle.DropDownList)
                _innerComboBox.DroppedDown = true;
        }

        private void _btnIcon_Paint(object sender, PaintEventArgs e) {
            int _iconWidth = 14, _iconHeight = 6;
            var _iconRect = new Rectangle((_btnIcon.Width - _iconWidth) / 2, (_btnIcon.Height - _iconHeight) / 2, _iconWidth, _iconHeight);

            using (var _path = new GraphicsPath())
            using (var _pen = new Pen(_iconColor, 2)) {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                _path.AddLine(_iconRect.X, _iconRect.Y, _iconRect.X + _iconWidth / 2, _iconRect.Bottom);
                _path.AddLine(_iconRect.X + _iconWidth / 2, _iconRect.Bottom, _iconRect.Right, _iconRect.Y);

                e.Graphics.DrawPath(_pen, _path);
            }
        }

        private void _btnIcon_Click(object sender, EventArgs e) {
            _innerComboBox.Select();
            _innerComboBox.DroppedDown = true;
        }

        private void _innerComboBox_TextChanged(object sender, EventArgs e) {
            _lblText.Text = _innerComboBox.Text;
        }

        private void _innerComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            _lblText.Text = _innerComboBox.Text;
            SelectedIndexChanged?.Invoke(sender, EventArgs.Empty);
        }

        //Method Overridings:
        protected override void OnResize(EventArgs e) {
            _AdjustInnerComboBoxDimensions();
            base.OnResize(e);
        }
        
        protected override void Dispose(bool disposing) {
            if (disposing) {
                _innerComboBox.SelectedIndexChanged -= _innerComboBox_SelectedIndexChanged;
                _innerComboBox.TextChanged -= _innerComboBox_TextChanged;
                _innerComboBox.Dispose();
                
                _btnIcon.Click -= _btnIcon_Click;
                _btnIcon.Paint -= _btnIcon_Paint;
                _btnIcon.Dispose();

                _lblText.Click -= _surface_Click;
                _lblText.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
