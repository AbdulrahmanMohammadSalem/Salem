using Salem.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Salem.Controls {
    /// <summary>
    /// Represents a composite control that combines a text-editable drop-down list with a customizable appearance,
    /// providing functionality similar to a <see cref="ComboBox"/> with additional styling and event support.
    /// </summary>
    /// <remarks><see cref="SalDropDownEdit"/> exposes events and properties for customizing the look and behavior of the
    /// drop-down button, border, and list items. It supports data binding, auto-complete features, and right-to-left
    /// layout. The control is designed for use in Windows Forms applications where a styled, editable drop-down
    /// selection is required. Most <see cref="ComboBox"/>-related operations and properties are accessible through this control.
    /// Thread safety is not guaranteed; access the control only from the UI thread.</remarks>
    [DefaultEvent("SelectedIndexChanged")]
    public class SalDropDownEdit : SalDropDownBase {
        #region Intance Fields
        private bool _dropDownHovering = false, _showSeparatorOnMouseOver = false;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether a separator line is drawn alongside the button.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(false)]
        public bool ShowSeparatorOnMouseOver {
            get => _showSeparatorOnMouseOver;
            set {
                _innerButton.Width = (_showSeparatorOnMouseOver = value) ? 27 : 26;

                _innerButton.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the border displayed around the control.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Color), "188, 188, 188")]
        public Color BorderColor {
            get => _borderColor;
            set {
                _borderColor = value;

                Invalidate();
                _innerButton.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the background color of the drop-down button when the mouse pointer is over it.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Color), "229, 229, 229")]
        public Color DropDownMouseOverBackColor { get => _innerButton.FlatAppearance.MouseOverBackColor; set => _innerButton.FlatAppearance.MouseOverBackColor = value; }

        /// <summary>
        /// Gets or sets the background color of the drop-down button when the mouse button is pressed.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Color), "210, 210, 210")]
        public Color DropDownMouseDownBackColor { get => _innerButton.FlatAppearance.MouseDownBackColor; set => _innerButton.FlatAppearance.MouseDownBackColor = value; }

        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Color), "Window")]
        public override Color BackColor {
            get => base.BackColor;
            set {
                base.BackColor = value;

                if (_innerComboBox != null)
                    _innerButton.BackColor = _innerComboBox.BackColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color used to draw the drop-down arrow on the button.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Color), "60, 60, 60")]
        public Color DropDownArrowColor { get => _innerButton.ForeColor; set => _innerButton.ForeColor = value; }

        /// <summary>
        /// Gets or sets a value indicating whether text and child elements are displayed from right to left, such as
        /// for languages like Arabic or Hebrew.
        /// </summary>
        /// <remarks>Setting this property affects the layout and alignment of the control and its child
        /// elements to support right-to-left languages. Changing the value may also update the position of associated
        /// UI elements to match the reading direction.</remarks>
        [Localizable(true)]
        public override RightToLeft RightToLeft {
            get => base.RightToLeft;
            set {
                _innerComboBox.RightToLeft = base.RightToLeft = value;

                _innerButton.Dock = value == RightToLeft.Yes ? DockStyle.Left : DockStyle.Right;
                _innerButton.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the automatic completion behavior of the text box as the user types.
        /// </summary>
        /// <remarks>Use this property to control how suggestions are provided to the user based on their
        /// input. The available modes determine whether suggestions are shown in a drop-down list, appended to the
        /// existing text, or both. The default value is <see cref="AutoCompleteMode.None"/>.</remarks>
        [DefaultValue(AutoCompleteMode.None)]
        public AutoCompleteMode AutoCompleteMode { get => _innerComboBox.AutoCompleteMode; set => _innerComboBox.AutoCompleteMode = value; }

        /// <summary>
        /// Gets or sets the source from which the control retrieves auto-complete suggestions for the text entered by
        /// the user.
        /// </summary>
        /// <remarks>Use this property to specify whether the control should suggest and/or append
        /// possible matches based on a predefined list, file system entries, or other sources. The behavior of
        /// auto-complete is also influenced by the <see cref="AutoCompleteMode"/> and <see cref="AutoCompleteCustomSource"/> properties.</remarks>
        [DefaultValue(AutoCompleteSource.None)]
        public AutoCompleteSource AutoCompleteSource { get => _innerComboBox.AutoCompleteSource; set => _innerComboBox.AutoCompleteSource = value; }

        /// <summary>
        /// Gets or sets the custom string collection used to provide auto-complete suggestions for the text box.
        /// </summary>
        /// <remarks>Use this property to specify a custom list of strings that will be suggested to the
        /// user as they type, when the auto-complete feature is enabled. The collection can be modified at runtime to
        /// update the available suggestions. This property is typically used in conjunction with the <see cref="AutoCompleteMode"/>
        /// and <see cref="AutoCompleteSource"/> properties.</remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public AutoCompleteStringCollection AutoCompleteCustomSource { get => _innerComboBox.AutoCompleteCustomSource; set => _innerComboBox.AutoCompleteCustomSource = value; }

        /// <summary>
        /// Gets or sets the maximum number of characters the user can enter into the text box component of the combo
        /// box.
        /// </summary>
        /// <remarks>A value of 0 indicates that there is no limit to the number of characters that can be
        /// entered. Setting this property to a value less than 0 will throw an exception.</remarks>
        [DefaultValue(0)]
        [Localizable(true)]
        public int MaxLength { get => _innerComboBox.MaxLength; set => _innerComboBox.MaxLength = value; }
        
        /// <summary>
        /// Gets or sets the number of characters selected in the text of the combo box.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength { get => _innerComboBox.SelectionLength; set => _innerComboBox.SelectionLength = value; }

        /// <summary>
        /// Gets or sets the starting position of the text selection within the editable portion of the combo box.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart { get => _innerComboBox.SelectionStart; set => _innerComboBox.SelectionStart = value; }

        /// <summary>
        /// This property is irrelevant for this control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Padding Padding { get => Padding.Empty; set { } }

        /// <summary>
        /// Gets a value indicating whether the <see cref="SalDropDownEdit"/> has focus.
        /// </summary>
        /// <returns><see langword="true"/> if this control has focus; otherwise, <see langword="false"/>.</returns>
        public override bool Focused => _innerComboBox.Focused;
        #endregion

        #region Identity
        /// <summary>
        /// Initializes a new instance of the <see cref="SalDropDownEdit"/> class.
        /// </summary>
        public SalDropDownEdit() : base() {
            ForeColor = SystemColors.WindowText;

            _innerComboBox.Text = string.Empty;
            _innerComboBox.TabIndex = 0;

            _innerButton.Dock = DockStyle.Right;
            _innerButton.Width = 26;
            _innerButton.Font = new Font("Segoe Fluent Icons", 7.5F);
            _innerButton.ForeColor = Color.FromArgb(60, 60, 60);
            _innerButton.RightToLeft = RightToLeft = RightToLeft.No;
            _innerButton.TabIndex = 1;
            _innerButton.FlatAppearance.BorderSize = 0;
            _innerButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 229, 229);
            _innerButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 210, 210);

            _innerComboBox.FontChanged += InnerComboBox_FontChanged;
            _innerComboBox.DropDown += InnerComboBox_DropDown;
            _innerComboBox.SelectedIndexChanged += InnerComboBox_SelectedIndexChanged;
            _innerComboBox.SelectionChangeCommitted += InnerComboBox_SelectionChangeCommitted;
            _innerComboBox.TextChanged += InnerComboBox_TextChanged;
            _innerComboBox.TextUpdate += InnerComboBox_TextUpdate;
            _innerComboBox.DropDownClosed += InnerComboBox_DropDownClosed;
            _innerComboBox.DrawItem += InnerComboBox_DrawItem;
            _innerComboBox.MeasureItem += InnerComboBox_MeasureItem;

            _innerButton.Paint += InnerButton_Paint;
            _innerButton.MouseEnter += InnerButton_MouseEnter;
            _innerButton.MouseLeave += InnerButton_MouseLeave;
            _innerButton.Click += InnerButton_Click;
            
            SuspendLayout();
            Controls.Add(_innerButton);
            Controls.Add(_innerComboBox);
            ResumeLayout();

            AdjustDimensions();
        }
        #endregion

        #region Implementation
        private void InnerComboBox_TextChanged(object sender, EventArgs e) => OnTextChanged(e);

        private void InnerButton_MouseLeave(object sender, EventArgs e) {
            _dropDownHovering = false;

            if (ShowSeparatorOnMouseOver)
                _innerButton.Invalidate();
        }

        private void InnerButton_MouseEnter(object sender, EventArgs e) {
            _dropDownHovering = true;

            if (ShowSeparatorOnMouseOver)
                _innerButton.Invalidate();
        }
        #endregion

        #region Protected Override
        protected override void InnerComboBox_FontChanged(object sender, EventArgs e) => AdjustDimensions();

        protected override void InnerComboBox_DropDownClosed(object sender, EventArgs e) => OnDropDownClosed(e);

        protected override void InnerComboBox_TextUpdate(object sender, EventArgs e) => OnTextUpdate(e);

        protected override void InnerComboBox_SelectionChangeCommitted(object sender, EventArgs e) => OnSelectionChangeCommitted(e);

        protected override void InnerComboBox_SelectedIndexChanged(object sender, EventArgs e) => OnSelectedIndexChanged(e);

        protected override void InnerComboBox_DropDown(object sender, EventArgs e) => OnDropDown(e);

        protected override void InnerButton_Click(object sender, EventArgs e) {
            _innerComboBox.Select();
            _innerComboBox.DroppedDown = true;
        }

        protected override void InnerButton_Paint(object sender, PaintEventArgs e) {
            using (Pen _pen = new Pen(_borderColor, 1F)) {
                if (RightToLeft == RightToLeft.Yes) {
                    e.Graphics.DrawLines(_pen, new Point[4] { new Point(_innerButton.Width - 1, 0), new Point(0, 0), new Point(0, _innerButton.Height - 1), new Point(_innerButton.Width - 1, _innerButton.Height - 1) });

                    if (_dropDownHovering && ShowSeparatorOnMouseOver)
                        e.Graphics.DrawLine(_pen, _innerButton.Width - 1, 0, _innerButton.Width - 1, _innerButton.Height - 1);
                } else {
                    e.Graphics.DrawLines(_pen, new Point[4] { new Point(0, 0), new Point(_innerButton.Width - 1, 0), new Point(_innerButton.Width - 1, _innerButton.Height - 1), new Point(0, _innerButton.Height - 1) });

                    if (_dropDownHovering && ShowSeparatorOnMouseOver)
                        e.Graphics.DrawLine(_pen, 0, 0, 0, _innerButton.Height - 1);
                }
            }

            using (var _brush = new SolidBrush(DropDownArrowColor))
            using (var _stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }) {
                if (RightToLeft == RightToLeft.Yes)
                    e.Graphics.DrawString(DrawingHelpers.GetArrowChar(BasicDirections.Down, ArrowStyles.Chevron).ToString(), _innerButton.Font, _brush, new Rectangle(0, 1, 26, _innerButton.ClientRectangle.Height - 1), _stringFormat);
                else
                    e.Graphics.DrawString(DrawingHelpers.GetArrowChar(BasicDirections.Down, ArrowStyles.Chevron).ToString(), _innerButton.Font, _brush, new Rectangle(0, 1, _innerButton.ClientRectangle.Width - 1, _innerButton.ClientRectangle.Height - 1), _stringFormat);
            }
        }

        protected override void AdjustDimensions() {
            IDisposable _oldRegion = _innerComboBox.Region;
            _innerButton.Width = ShowSeparatorOnMouseOver ? 27 : 26; //It broke in design-time, so I had to enforce it here...
        
            if (RightToLeft == RightToLeft.Yes)
                _innerComboBox.Region = new Region(new Rectangle(30, 3, _innerComboBox.Width - _innerButton.Width - 5, _innerComboBox.PreferredHeight - 6));
            else
                _innerComboBox.Region = new Region(new Rectangle(3, 3, _innerComboBox.Width - _innerButton.Width - 5, _innerComboBox.PreferredHeight - 6));
        
            Height = _innerComboBox.PreferredHeight;
            _oldRegion?.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e) {
            DrawingHelpers.DrawSimpleBorder(e.Graphics, new Rectangle(0, 0, Width - 1, Height - 1), _borderColor, 1F);
            base.OnPaint(e);
        }

        protected override void OnRightToLeftChanged(EventArgs e) {
            _innerButton.Dock = RightToLeft == RightToLeft.Yes ? DockStyle.Left : DockStyle.Right;
            base.OnRightToLeftChanged(e);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _innerButton.Paint -= InnerButton_Paint;
                _innerButton.MouseEnter -= InnerButton_MouseEnter;
                _innerButton.MouseLeave -= InnerButton_MouseLeave;
                _innerButton.Click -= InnerButton_Click;

                _innerComboBox.FontChanged -= InnerComboBox_FontChanged;
                _innerComboBox.DropDown -= InnerComboBox_DropDown;
                _innerComboBox.SelectedIndexChanged -= InnerComboBox_SelectedIndexChanged;
                _innerComboBox.SelectionChangeCommitted -= InnerComboBox_SelectionChangeCommitted;
                _innerComboBox.TextChanged -= InnerComboBox_TextChanged;
                _innerComboBox.TextUpdate -= InnerComboBox_TextUpdate;
                _innerComboBox.DropDownClosed -= InnerComboBox_DropDownClosed;
                _innerComboBox.DrawItem -= InnerComboBox_DrawItem;
                _innerComboBox.MeasureItem -= InnerComboBox_MeasureItem;
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Selects a range of text within the editable portion of the combo box.
        /// </summary>
        /// <remarks>If the combo box is not editable, this method has no effect. The selection will be
        /// adjusted if the specified range exceeds the available text length.</remarks>
        /// <param name="start">The zero-based index of the first character in the selection.</param>
        /// <param name="length">The number of characters to select.</param>
        public void Select(int start, int length) => _innerComboBox.Select(start, length);

        /// <summary>
        /// Selects all the text in the editable portion of the combo box, if any.
        /// </summary>
        /// <remarks>If the combo box is not editable or contains no text, this method has no
        /// effect.</remarks>
        public void SelectAll() => _innerComboBox.SelectAll();

        /// <summary>
        /// Sets input focus to the control.
        /// </summary>
        public new void Focus() => _innerComboBox.Focus();
        #endregion
    }
}
