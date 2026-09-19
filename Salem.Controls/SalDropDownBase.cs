using Salem.Drawing;
using Salem.Utils;
using Salem.Utils.Image_Resources;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Salem.Controls {
    /// <summary>
    /// This is the base, abstract class for both <see cref="SalDropDownList"/> and <see cref="SalDropDownEdit"/>.
    /// </summary>
    public abstract class SalDropDownBase : Control {
        #region SalDropDownPurpose
        /// <summary>
        /// Represents the possible purposes/use-cases for <see cref="SalDropDownBase"/> and its derived classes.
        /// </summary>
        public enum SalDropDownPurpose : byte {
            /// <summary>
            /// Indicates that the drop down is general-purpose.
            /// </summary>
            NotSet,
            /// <summary>
            /// Indicates that the drop down should display fonts installed on the client's system.
            /// </summary>
            Fonts, 
            /// <summary>
            /// Indicates that the drop down should display all countries.
            /// </summary>
            Countries,
            /// <summary>
            /// Indicates that the drop down should display a collection of paper sizes.
            /// </summary>
            PaperSizes,
            /// <summary>
            /// Indicates that the drop down should display all months in a year.
            /// </summary>
            Months,
            /// <summary>
            /// Indicates that the drop down should display all days in a week.
            /// </summary>
            DaysOfWeek
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the drop-down portion of a <see cref="ComboBox"/> is shown.
        /// </summary>
        public event EventHandler DropDown;

        /// <summary>
        /// Occurs when the <see cref="ComboBox.SelectedIndex"/> property has changed.
        /// </summary>
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// Occurs when the user changes the selected item and that change is displayed in the <see cref="ComboBox"/>.
        /// </summary>
        public event EventHandler SelectionChangeCommitted;

        /// <summary>
        /// Occurs when the control has formatted the text, but before the text is displayed.
        /// </summary>
        public event EventHandler TextUpdate;

        /// <summary>
        /// Occurs when the drop-down portion of the <see cref="ComboBox"/> is no longer visible.
        /// </summary>
        public event EventHandler DropDownClosed;
        #endregion

        #region Instance Fields
        protected readonly Button _innerButton = new Button { FlatStyle = FlatStyle.Flat };
        protected readonly ComboBox _innerComboBox = new ComboBox { Dock = DockStyle.Fill, DrawMode = DrawMode.OwnerDrawVariable, MaxDropDownItems = 12 };

        private readonly SolidBrush _foreColorBrush = new SolidBrush(SystemColors.WindowText);
        private readonly StringFormat _drawItemStringFormat = new StringFormat { LineAlignment = StringAlignment.Center };

        protected Color _borderColor = Color.FromArgb(188, 188, 188);
        protected SalDropDownPurpose _purpose = SalDropDownPurpose.NotSet;

        private int _dropDownItemsHeight = -1, _cachedDefaultDropDownItemsHeight;
        private StringAlignment _textAlignDropDownItems = StringAlignment.Near;

        private Func<DrawItemEventArgs, (Rectangle, Rectangle)> CalculateAppropriateItemRectangles = CalculateAppropriateItemRectangles_LTR;
        private Func<int> GetActiveItemsHeight;
        #endregion

        #region Event Raisers
        /// <summary>
        /// Raises the <see cref="DropDown"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnDropDown(EventArgs e) => DropDown?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="SelectedIndexChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="SelectionChangeCommitted"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnSelectionChangeCommitted(EventArgs e) => SelectionChangeCommitted?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="TextUpdate"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnTextUpdate(EventArgs e) => TextUpdate?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="DropDownClosed"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnDropDownClosed(EventArgs e) => DropDownClosed?.Invoke(this, e);
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the data source that populates the items in the combo box.
        /// </summary>
        /// <remarks>The data source can be any object that implements the <see cref="System.Collections.IList"/>, <see cref="IListSource"/>, or
        /// <see cref="IBindingList"/> interfaces, such as a <see cref="System.Data.DataTable"/>, <see cref="System.Data.DataView"/>, or an array. Setting this property enables data
        /// binding for the combo box. When the data source is set, the items collection is managed by the data source
        /// and cannot be modified directly. To display specific properties from complex objects, set the <see cref="DisplayMember"/>
        /// and <see cref="ValueMember"/> properties accordingly.</remarks>
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [AttributeProvider(typeof(IListSource))]
        public object DataSource { get => _innerComboBox.DataSource; set => _innerComboBox.DataSource = value; }

        /// <summary>
        /// Gets or sets the property to display for this <see cref="ListControl"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> specifying the name of an object property that is contained in
        /// the collection specified by the <see cref="ListControl.DataSource"/> property.
        /// The default is an empty string ("").
        /// </returns>
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string DisplayMember { get => _innerComboBox.DisplayMember; set => _innerComboBox.DisplayMember = value; }

        /// <summary>
        /// Gets or sets the path of the property to use as the actual value for the items in the <see cref="ListControl"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> representing a single property name of the <see cref="ListControl.DataSource"/>
        /// property value, or a hierarchy of period-delimited property names that resolves
        /// to a property name of the final data-bound object. The default is an empty string ("").
        /// </returns>
        /// <exception cref="ArgumentException">The specified property path cannot be resolved through the object specified by the <see cref="ListControl.DataSource"/> property.</exception>
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ValueMember { get => _innerComboBox.ValueMember; set => _innerComboBox.ValueMember = value; }
        
        /// <summary>
        /// Gets the collection of items contained in the ComboBox.
        /// </summary>
        /// <remarks>Use this property to add, remove, or access the items displayed in the ComboBox. The
        /// collection supports standard collection operations such as adding, removing, and enumerating items.
        /// Modifying the collection updates the items shown in the ComboBox control.</remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        public virtual ComboBox.ObjectCollection Items => _innerComboBox.Items;

        /// <summary>
        /// Gets or sets the zero-based index of the currently selected item in the combo box.
        /// </summary>
        /// <remarks>Set this property to -1 to clear the selection. If the value is less than -1 or
        /// greater than or equal to the number of items in the combo box, an exception may be thrown.</remarks>
        public int SelectedIndex { get => _innerComboBox.SelectedIndex; set => _innerComboBox.SelectedIndex = value; }

        /// <summary>
        /// Gets or sets the currently selected item in the control.
        /// </summary>
        /// <remarks>Setting this property to a value that does not exist in the item collection will
        /// clear the selection. The value can be null if no item is selected.</remarks>
        [Browsable(false)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem { get => _innerComboBox.SelectedItem; set => _innerComboBox.SelectedItem = value; }

        /// <summary>
        /// Gets or sets a value indicating whether the items in the combo box are sorted alphabetically.
        /// </summary>
        /// <remarks>When set to <see langword="true"/>, items added to the combo box are automatically
        /// sorted in ascending order. Setting this property to <see langword="false"/> preserves the order in which
        /// items are added.</remarks>
        [DefaultValue(false)]
        public bool Sorted { get => _innerComboBox.Sorted; set => _innerComboBox.Sorted = value; }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [DefaultValue("")]
        [Localizable(true)]
        [Bindable(true)]
        public override string Text { get => _innerComboBox.Text; set => _innerComboBox.Text = value; }

        /// <summary>
        /// Gets or sets the maximum number of items to display in the drop-down portion of the combo box.
        /// </summary>
        /// <remarks>If the number of items in the combo box exceeds this value, a vertical scroll bar is
        /// displayed in the drop-down list. The default value is 8.</remarks>
        [DefaultValue(12)]
        [Localizable(true)]
        public int MaxDropDownItems { get => _innerComboBox.MaxDropDownItems; set => _innerComboBox.MaxDropDownItems = value; }

        /// <summary>
        /// Gets or sets the width of the of the drop-down portion of a combo box.
        /// </summary>
        /// <returns>The width, in pixels, of the drop-down box.</returns>
        /// <exception cref="ArgumentException">The specified value is less than one.</exception>
        [Localizable(true)]
        public int DropDownWidth { get => _innerComboBox.DropDownWidth; set => _innerComboBox.DropDownWidth = value; }
        
        /// <summary>
        /// Gets or sets the height in pixels of the drop-down portion of the <see cref="SalDropDownBase"/> control and its derivatives.
        /// </summary>
        /// <returns>The height, in pixels, of the drop-down box.</returns>
        /// <exception cref="ArgumentException">The specified value is less than one.</exception>
        [Localizable(true)]
        [DefaultValue(106)]
        public int DropDownHeight { get => _innerComboBox.DropDownHeight; set => _innerComboBox.DropDownHeight = value; }

        /// <summary>
        /// Gets or sets a value indicating whether the combo box is displaying its drop-down portion.
        /// </summary>
        /// <returns><see langword="true"/> if the drop-down portion is displayed; otherwise, <see langword="false"/>. The default is <see langword="false"/>.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(false)]
        public bool DroppedDown { get => _innerComboBox.DroppedDown; set => _innerComboBox.DroppedDown = value; }
        
        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(typeof(Color), "WindowText")]
        public override Color ForeColor { 
            get => base.ForeColor;
            set {
                _foreColorBrush.Color = base.ForeColor = value;

                if (_innerComboBox != null)
                    _innerComboBox.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether formatting is applied to the <see cref="ListControl.DisplayMember"/> property of the <see cref="ListControl"/>.
        /// </summary>
        /// <returns><see langword="true"/> if formatting of the <see cref="ListControl.DisplayMember"/> property is enabled; otherwise, <see langword="false"/>. The default is <see langword="false"/>.</returns>
        public bool FormattingEnabled { get => _innerComboBox.FormattingEnabled; set => _innerComboBox.FormattingEnabled = value; }

        /// <summary>
        /// Gets or sets a value indicating the horizontal alignment of all drop down items.
        /// </summary>
        [DefaultValue(StringAlignment.Near)]
        public StringAlignment DropDownItemsTextAlign {
            get => _textAlignDropDownItems;
            set {
                if (_textAlignDropDownItems != value) {
                    _textAlignDropDownItems = value;
                    _drawItemStringFormat.Alignment = GetAppropriateStringFormatAlignment();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the vertical height of all drop down items. The default value of -1 will use the default height.
        /// </summary>
        [DefaultValue(-1)]
        [Localizable(true)]
        public int DropDownItemsHeight {
            get => _dropDownItemsHeight;
            set {
                if (value < -1)
                    throw new ArgumentOutOfRangeException(nameof(DropDownItemsHeight), "The value should be -1 or higher.");

                if (_dropDownItemsHeight != value) {
                    _dropDownItemsHeight = value;

                    if (value == -1)
                        GetActiveItemsHeight = () => _cachedDefaultDropDownItemsHeight;
                    else
                        GetActiveItemsHeight = () => _dropDownItemsHeight;

                    ForceMeasureItemEvent();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the intended purpose/usage of the combo box control.
        /// </summary>
        [DefaultValue(SalDropDownPurpose.NotSet)]
        public SalDropDownPurpose Purpose {
            get => _purpose;
            set {
                if (_purpose != value) {
                    _purpose = value;

                    if (!DesignMode)
                        ApplyPurposeSettings();
                }
            }
        }

        /// <summary>
        /// Determines whether to draw each item in different fonts -- useful when the value of <see cref="Purpose"/> is set to <see cref="SalDropDownPurpose.Fonts"/>.
        /// </summary>
        [DefaultValue(true)]
        public bool UsePurposeAwareItemFonts { get; set; } = true;

        /// <summary>
        /// Detemines whether to draw each item with different images -- useful when the value of <see cref="Purpose"/> is set to <see cref="SalDropDownPurpose.Countries"/>.
        /// </summary>
        [DefaultValue(true)]
        public bool UsePurposeAwareItemImages { get; set; } = true;

        /// <summary>
        /// Gets the internal <see cref="System.Windows.Forms.ComboBox"/> instance used by the drop-down base. You will most likely not need it, but it's available for you.
        /// </summary>
        [Browsable(false)]
        public ComboBox ComboBox => _innerComboBox;
        #endregion

        #region Identity
        protected SalDropDownBase() {
            UpdateCachedDefaultItemHeight();
            GetActiveItemsHeight = () => _cachedDefaultDropDownItemsHeight;

            TabStop = false;
            BackColor = SystemColors.Window;
        }
        #endregion

        #region Abstract Methods
        protected abstract void AdjustDimensions();

        protected abstract void InnerComboBox_TextUpdate(object sender, EventArgs e);

        protected abstract void InnerComboBox_SelectionChangeCommitted(object sender, EventArgs e);

        protected abstract void InnerComboBox_DropDownClosed(object sender, EventArgs e);

        protected abstract void InnerComboBox_DropDown(object sender, EventArgs e);

        protected abstract void InnerComboBox_SelectedIndexChanged(object sender, EventArgs e);

        protected abstract void InnerComboBox_FontChanged(object sender, EventArgs e);

        protected abstract void InnerButton_Click(object sender, EventArgs e);

        protected abstract void InnerButton_Paint(object sender, PaintEventArgs e);
        #endregion

        #region Protected Override
        protected override void OnResize(EventArgs e) {
            AdjustDimensions();
            base.OnResize(e);
        }

        protected override void OnCreateControl() {
            AdjustDimensions();

            if (!DesignMode)
                ApplyPurposeSettings();

            base.OnCreateControl();
        }

        protected override void OnRightToLeftChanged(EventArgs e) {
            _drawItemStringFormat.Alignment = GetAppropriateStringFormatAlignment();

            if (RightToLeft == RightToLeft.Yes)
                CalculateAppropriateItemRectangles = CalculateAppropriateItemRectangles_RTL;
            else
                CalculateAppropriateItemRectangles = CalculateAppropriateItemRectangles_LTR;

            base.OnRightToLeftChanged(e);
        }

        protected override void OnFontChanged(EventArgs e) {
            UpdateCachedDefaultItemHeight();

            base.OnFontChanged(e);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _innerButton.Dispose();
                _innerComboBox.Dispose();

                _foreColorBrush.Dispose();
                _drawItemStringFormat.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Implemenation
        private void ApplyPurposeSettings() {
            if (_purpose != SalDropDownPurpose.NotSet) {
                Items.Clear();
                Items.AddRange(GetAppropriateItemsCollection());
            }
        }

        private string[] GetAppropriateItemsCollection() {
            switch (_purpose) {
                case SalDropDownPurpose.Fonts: return DrawingHelpers.GetInstalledFontFamilyNames(false);
                case SalDropDownPurpose.Countries: return LocalizedStrings.GetCountryNames();
                case SalDropDownPurpose.PaperSizes: return LocalizedStrings.GetPaperSizeNames();
                case SalDropDownPurpose.Months: return LocalizedStrings.GetMonthNames();
                case SalDropDownPurpose.DaysOfWeek: return LocalizedStrings.GetDayOfWeekNames();
                default: return new string[] { };
            }
        }

        private StringAlignment GetAppropriateStringFormatAlignment() {
            switch (_textAlignDropDownItems) {
                case StringAlignment.Near: return RightToLeft == RightToLeft.Yes ? StringAlignment.Far : StringAlignment.Near;
                case StringAlignment.Center: return StringAlignment.Center;
                case StringAlignment.Far: return RightToLeft == RightToLeft.Yes ? StringAlignment.Near : StringAlignment.Far;
                default: throw new InvalidEnumArgumentException();
            }
        }

        private Brush GetAppropriateDrawItemBrush(DrawItemState state) => state.HasFlag(DrawItemState.Focus) || state.HasFlag(DrawItemState.Selected) ? Brushes.White : _foreColorBrush;

        protected void InnerComboBox_DrawItem(object sender, DrawItemEventArgs e) {
            if (e.Index < 0)
                return;

            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.DrawBackground();

            switch (_purpose) {
                case SalDropDownPurpose.Fonts:
                    PerformDrawItem_Fonts(e);
                    break;
                case SalDropDownPurpose.Countries:
                    PerformDrawItem_Countries(e);
                    break;
                default:
                    PerformDrawItem_Any(e);
                    break;
            }
        }

        protected void InnerComboBox_MeasureItem(object sender, MeasureItemEventArgs e) => e.ItemHeight = GetActiveItemsHeight();

        private void PerformDrawItem_Fonts(DrawItemEventArgs e) {
            if (UsePurposeAwareItemFonts) {
                using (var _font = new Font(Items[e.Index].ToString(), _innerComboBox.Font.Size))
                    e.Graphics.DrawString(Items[e.Index].ToString(), _font, GetAppropriateDrawItemBrush(e.State), e.Bounds, _drawItemStringFormat);

                return;
            }

            PerformDrawItem_Any(e);
        }

        private void PerformDrawItem_Countries(DrawItemEventArgs e) {
            if (UsePurposeAwareItemImages) {
                (Rectangle _imageRect, Rectangle _textRect) = CalculateAppropriateItemRectangles(e);
                
                e.Graphics.DrawImage(CountryFlags_Square64.Retrieve((Countries) e.Index), _imageRect);
                e.Graphics.DrawString(Items[e.Index].ToString(), Font, GetAppropriateDrawItemBrush(e.State), _textRect, _drawItemStringFormat);

                return;
            }
            
            PerformDrawItem_Any(e);
        }

        private static (Rectangle imgRect, Rectangle txtRect) CalculateAppropriateItemRectangles_RTL(DrawItemEventArgs e) => (
            new Rectangle(e.Bounds.X + e.Bounds.Width - e.Bounds.Height, e.Bounds.Y, e.Bounds.Height, e.Bounds.Height),
            new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height)
        );

        private static (Rectangle imgRect, Rectangle txtRect) CalculateAppropriateItemRectangles_LTR(DrawItemEventArgs e) => (
            new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Height, e.Bounds.Height),
            new Rectangle(e.Bounds.X + e.Bounds.Height, e.Bounds.Y, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height)
        );

        private void PerformDrawItem_Any(DrawItemEventArgs e) => e.Graphics.DrawString(Items[e.Index].ToString(), Font, GetAppropriateDrawItemBrush(e.State), e.Bounds, _drawItemStringFormat);

        private void UpdateCachedDefaultItemHeight() {
            _innerComboBox.DrawMode = DrawMode.Normal;
            _cachedDefaultDropDownItemsHeight = _innerComboBox.ItemHeight;
            _innerComboBox.DrawMode = DrawMode.OwnerDrawVariable;
        }

        private void ForceMeasureItemEvent() {
            _innerComboBox.DrawMode = DrawMode.Normal;
            _innerComboBox.DrawMode = DrawMode.OwnerDrawVariable;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Finds the first item in the list that starts with the specified string and returns its index.
        /// </summary>
        /// <remarks>The search starts at the beginning of the list and wraps around to the beginning if
        /// necessary. The method performs a case-insensitive comparison using the current culture.</remarks>
        /// <param name="s">The string to search for. The comparison is case-insensitive. Can be <see langword="null"/> or empty to search for an empty
        /// string.</param>
        /// <returns>The zero-based index of the first matching item; otherwise, -1 if no match is found.</returns>
        public int FindString(string s) => _innerComboBox.FindString(s);

        /// <summary>
        /// Finds the first item in the list that starts with the specified string, beginning the search at the given
        /// index.
        /// </summary>
        /// <param name="s">The string to search for. The search is case-insensitive and matches items that start with this string.</param>
        /// <param name="startIndex">The zero-based index at which to start the search. Must be greater than or equal to 0 and less than the
        /// number of items in the list.</param>
        /// <returns>The zero-based index of the first item found that starts with the specified string; otherwise, -1 if no
        /// matching item is found.</returns>
        public int FindString(string s, int startIndex) => _innerComboBox.FindString(s, startIndex);

        /// <summary>
        /// Finds the first item in the combo box that exactly matches the specified string.
        /// </summary>
        /// <param name="s">The string to search for. The comparison is case-insensitive and must match the entire item text.</param>
        /// <returns>The zero-based index of the first item that exactly matches the specified string; otherwise, -1 if no match
        /// is found.</returns>
        public int FindStringExact(string s) => _innerComboBox.FindStringExact(s);

        /// <summary>
        /// Finds the first item in the list that exactly matches the specified string, starting the search at the given
        /// index.
        /// </summary>
        /// <param name="s">The string to search for. The comparison is case-insensitive and must match the entire item text.</param>
        /// <param name="startIndex">The zero-based index at which to start the search. Must be greater than or equal to -1 and less than the
        /// number of items.</param>
        /// <returns>The zero-based index of the first item that exactly matches the specified string; otherwise, -1 if no match
        /// is found.</returns>
        public int FindStringExact(string s, int startIndex) => _innerComboBox.FindStringExact(s, startIndex);

        /// <summary>
        /// Retrieves the height, in pixels, of the item at the specified index within the combo box.
        /// </summary>
        /// <param name="index">The zero-based index of the item whose height is to be retrieved. Must be greater than or equal to 0 and
        /// less than the total number of items.</param>
        /// <returns>The height, in pixels, of the specified item.</returns>
        public int GetItemHeight(int index) => _innerComboBox.GetItemHeight(index);

        /// <summary>
        /// Returns the display text for the specified item as it would appear in the combo box.
        /// </summary>
        /// <param name="item">The item for which to retrieve the display text. Can be null if the combo box supports null items.</param>
        /// <returns>A string representing the display text of the specified item. Returns an empty string if the item is null or
        /// has no display text.</returns>
        public string GetItemText(object item) => _innerComboBox.GetItemText(item);

        /// <summary>
        /// Maintains performance when items are added to the <see cref="ComboBox"/> one at a time.
        /// </summary>
        public void BeginUpdate() => _innerComboBox.BeginUpdate();

        /// <summary>
        /// Resumes painting the <see cref="ComboBox"/> control after painting is suspended by the <see cref="ComboBox.BeginUpdate"/> method.
        /// </summary>
        public void EndUpdate() => _innerComboBox.EndUpdate();
        #endregion
    }
}
