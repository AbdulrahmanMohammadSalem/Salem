using Salem.Drawing;
using Salem.Utils;
using Salem.Utils.Image_Resources;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Salem.Controls {
    /// <summary>
    /// Represents a custom dropdown list control that combines a button and a combo box, providing enhanced appearance
    /// customization and selection functionality.
    /// </summary>
    [DefaultEvent("SelectedIndexChanged")]
    public class SalDropDownList : SalDropDownBase {
        #region Instance Fields
        private Color _dropDownArrowColor = Color.FromArgb(60, 60, 60);
        private int _borderSize = 1, _imageSizeCache;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the color of the border displayed around the control.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Color), "188, 188, 188")]
        public Color BorderColor {
            get => _borderColor;
            set {
                _borderColor = value;
                _innerButton.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the background color of the combo box when the mouse pointer is over it.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Color), "229, 229, 229")]
        public Color MouseOverBackColor { get => _innerButton.FlatAppearance.MouseOverBackColor; set => _innerButton.FlatAppearance.MouseOverBackColor = value; }

        /// <summary>
        /// Gets or sets the background color of the combo box when the mouse button is pressed.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Color), "210, 210, 210")]
        public Color MouseDownBackColor { get => _innerButton.FlatAppearance.MouseDownBackColor; set => _innerButton.FlatAppearance.MouseDownBackColor = value; }

        /// <summary>
        /// Gets or sets the border thickness of the combo box.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(1)]
        public int BorderSize {
            get => _borderSize;
            set {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(BorderSize), "The value must be 0 or higher.");

                _borderSize = value;
                _imageSizeCache = _innerButton.Height - 2 * value;
                _innerButton.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color used to draw the drop-down arrow on the button.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Color), "60, 60, 60")]
        public Color DropDownArrowColor { 
            get => _dropDownArrowColor; 
            set {
                _dropDownArrowColor = value;
                _innerButton.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Color), "Window")]
        public override Color BackColor {
            get => base.BackColor;
            set{ 
                base.BackColor = value;

                if (_innerComboBox != null)
                    _innerComboBox.BackColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the padding of the combo box control.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Padding), "0, 0, 0, 0")]
        public new Padding Padding { get => _innerButton.Padding; set => _innerButton.Padding = value; }

        /// <summary>
        /// Gets or sets the alignment of the text on the combo box control.
        /// </summary>
        /// <returns>One of the <see cref="ContentAlignment"/> values. The default is <see cref="ContentAlignment.MiddleLeft"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">The value assigned is not one of the <see cref="ContentAlignment"/> values.</exception>
        [Localizable(true)]
        [DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment TextAlign { get => _innerButton.TextAlign; set => _innerButton.TextAlign = value; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="SalDropDownList"/> has focus.
        /// </summary>
        /// <returns><see langword="true"/> if this control has focus; otherwise, <see langword="false"/>.</returns>
        public override bool Focused => _innerButton.Focused;
        #endregion

        #region Identity
        public SalDropDownList() : base() {
            _innerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _innerComboBox.TabStop = false;
            _innerComboBox.Text = string.Empty;

            _innerButton.Dock = DockStyle.Fill;
            _innerButton.TextAlign = ContentAlignment.MiddleLeft;
            _innerButton.FlatAppearance.BorderSize = 0;
            _innerButton.FlatAppearance.BorderColor = Color.FromArgb(188, 188, 188);
            _innerButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 229, 229);
            _innerButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 210, 210);

            _innerComboBox.FontChanged += InnerComboBox_FontChanged;
            _innerComboBox.SelectedIndexChanged += InnerComboBox_SelectedIndexChanged;
            _innerComboBox.DropDown += InnerComboBox_DropDown;
            _innerComboBox.DropDownClosed += InnerComboBox_DropDownClosed;
            _innerComboBox.SelectedIndexChanged += InnerComboBox_SelectedIndexChanged;
            _innerComboBox.SelectionChangeCommitted += InnerComboBox_SelectionChangeCommitted;
            _innerComboBox.TextUpdate += InnerComboBox_TextUpdate;
            _innerComboBox.DrawItem += InnerComboBox_DrawItem;
            _innerComboBox.MeasureItem += InnerComboBox_MeasureItem;

            _innerButton.Paint += InnerButton_Paint;
            _innerButton.Click += InnerButton_Click;

            SuspendLayout();
            Controls.Add(_innerButton);
            Controls.Add(_innerComboBox);
            ResumeLayout();

            AdjustDimensions();
        }
        #endregion

        #region Protected Override
        protected override void InnerComboBox_TextUpdate(object sender, EventArgs e) => OnTextUpdate(e);

        protected override void InnerComboBox_SelectionChangeCommitted(object sender, EventArgs e) => OnSelectionChangeCommitted(e);

        protected override void InnerComboBox_DropDownClosed(object sender, EventArgs e) => OnDropDownClosed(e);

        protected override void InnerComboBox_DropDown(object sender, EventArgs e) => OnDropDown(e);

        protected override void InnerComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            _innerButton.Text = _innerComboBox.Text;
            OnSelectedIndexChanged(e);
        }

        protected override void InnerComboBox_FontChanged(object sender, EventArgs e) => AdjustDimensions();

        protected override void InnerButton_Click(object sender, EventArgs e) {
            _innerComboBox.Select();
            _innerComboBox.DroppedDown = true;
            OnClick(e);
        }

        protected override void InnerButton_Paint(object sender, PaintEventArgs e) {
            Rectangle _arrowRect, _imageRect;

            if (RightToLeft == RightToLeft.Yes) {
                _arrowRect = new Rectangle(_innerButton.ClientRectangle.X - 1, _innerButton.ClientRectangle.Y + 2, 28, _innerButton.ClientRectangle.Height - 2);
                _imageRect = new Rectangle(_innerButton.Width - _borderSize - _imageSizeCache, _borderSize, _imageSizeCache, _imageSizeCache);
            }
            else {
                _arrowRect = new Rectangle(_innerButton.ClientRectangle.Right - 28, _innerButton.ClientRectangle.Y + 2, 28, _innerButton.ClientRectangle.Height - 2);
                _imageRect = new Rectangle(_borderSize, _borderSize, _imageSizeCache, _imageSizeCache);
            }

            if (_borderSize > 0)
                DrawingHelpers.DrawSimpleBorder(e.Graphics, new Rectangle(_innerButton.ClientRectangle.X, _innerButton.ClientRectangle.Y, _innerButton.ClientRectangle.Width - 1, _innerButton.ClientRectangle.Height - 1), _borderColor, _borderSize);
            
            if (_innerComboBox.SelectedIndex > -1 && _purpose == SalDropDownPurpose.Countries && UsePurposeAwareItemImages)
                e.Graphics.DrawImage(CountryFlags_Square64.Retrieve((Countries) _innerComboBox.SelectedIndex), _imageRect);

            using (var _brush = new SolidBrush(_dropDownArrowColor))    
            using (var _font = new Font("Segoe Fluent Icons", 7.5F))
            using (var _stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                e.Graphics.DrawString(DrawingHelpers.GetArrowChar(BasicDirections.Down, ArrowStyles.Chevron).ToString(), _font, _brush, _arrowRect, _stringFormat);
        }

        protected override void AdjustDimensions() {
            Height = _innerComboBox.PreferredHeight;
            _imageSizeCache = _innerButton.Height - 2 * _borderSize;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _innerButton.Paint -= InnerButton_Paint;
                _innerButton.Click -= InnerButton_Click;

                _innerComboBox.FontChanged -= InnerComboBox_FontChanged;
                _innerComboBox.SelectedIndexChanged -= InnerComboBox_SelectedIndexChanged;
                _innerComboBox.DropDown -= InnerComboBox_DropDown;
                _innerComboBox.DropDownClosed -= InnerComboBox_DropDownClosed;
                _innerComboBox.SelectedIndexChanged -= InnerComboBox_SelectedIndexChanged;
                _innerComboBox.SelectionChangeCommitted -= InnerComboBox_SelectionChangeCommitted;
                _innerComboBox.TextUpdate -= InnerComboBox_TextUpdate;
                _innerComboBox.DrawItem -= InnerComboBox_DrawItem;
                _innerComboBox.MeasureItem -= InnerComboBox_MeasureItem;
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets input focus to the control.
        /// </summary>
        public new void Focus() => _innerButton.Focus();
        #endregion
    }
}
