using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Salem.PInvoke;

namespace Salem.Controls {
    /// <summary>
    /// Represents a label-like control that displays read-only, selectable text with optional parent color matching.
    /// </summary>
    /// <remarks>SalSelectableLabel is derived from TextBox and is designed to behave visually like a label
    /// while allowing optional text selection. By default, it disables editing, border, and tab stop, and matches its
    /// background and foreground colors to its parent control for seamless integration. The control can be configured
    /// to allow or prevent text selection and to inherit the parent's foreground color. This makes it suitable for
    /// scenarios where label-like appearance is desired but text selection may be needed, such as copying displayed
    /// information.</remarks>
    [ToolboxItem(true)]
    public partial class SalSelectableLabel : TextBox {
        private bool _textSelectionEnabled = true; //Default value
        private bool _matchParentForeColor = true; //Default value

        /// <summary>
        /// Gets or sets a value indicating whether users can select text within the label using the mouse.
        /// </summary>
        /// <remarks>When text selection is enabled, the cursor changes to an I-beam and users can
        /// highlight and copy text. When disabled, the cursor appears as an arrow and text selection is not
        /// possible.</remarks>
        [DefaultValue(true)]
        public bool TextSelectionEnabled {
            get => _textSelectionEnabled;
            set {
                if (value == _textSelectionEnabled)
                    return;

                if (value) {
                    MouseDown -= SelectableLabel_MouseDown;
                    Cursor = Cursors.IBeam;
                } else {
                    MouseDown += SelectableLabel_MouseDown;
                    Cursor = Cursors.Arrow;
                }

                _textSelectionEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <remarks>If the control is configured to match its parent's foreground color, setting this
        /// property will update the color based on the parent's value. Otherwise, the specified color is applied
        /// directly. Changes to this property may affect the appearance of text or graphics rendered by the
        /// control.</remarks>
        public override Color ForeColor { 
            get => base.ForeColor;
            set => base.ForeColor = _matchParentForeColor ? (Parent == null ? value : Parent.ForeColor) : value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control's foreground color matches its parent control's
        /// foreground color.
        /// </summary>
        /// <remarks>When this property is set to <see langword="true"/>, the control automatically
        /// updates its foreground color to match that of its parent. Setting this property to <see langword="false"/>
        /// allows the control to use its own specified foreground color.</remarks>
        [DefaultValue(true)]
        public bool MatchParentForeColor {
            get => _matchParentForeColor;
            set {
                _matchParentForeColor = value;

                if (_matchParentForeColor)
                    ForeColor = base.ForeColor;
            }
        }

        private void SelectableLabel_MouseDown(object sender, MouseEventArgs e) {
            Enabled = false;
            SelectionStart = SelectionLength = 0;
            Enabled = true;
        }
        
        /// <summary>
        /// Initializes a new instance of the SalSelectableLabel class with default property values.
        /// </summary>
        /// <remarks>This constructor configures the control to be read-only, non-multiline, and disables
        /// tab stop and word wrapping by default. The background color is set to match the parent control if available.
        /// The caret is hidden when the control receives focus or when the mouse button is released over the
        /// control.</remarks>
        public SalSelectableLabel() {
            BorderStyle = BorderStyle.None;
            ReadOnly = Multiline = true;
            TabStop = WordWrap = false;
            BackColor = Parent != null ? Parent.BackColor : SystemColors.Control;

            GotFocus += (s, e) => User32.HideCaret(Handle);
            MouseUp += (s, e) => User32.HideCaret(Handle);
        }

        protected override void OnParentBackColorChanged(EventArgs e) {
            base.OnParentBackColorChanged(e);
            BackColor = Parent.BackColor;
        }

        protected override void OnParentChanged(EventArgs e) {
            base.OnParentChanged(e);

            if (Parent != null)
                BackColor = Parent.BackColor;
        }

        protected override void WndProc(ref Message m) {
            if (!TextSelectionEnabled && m.Msg == 0x007B)
                return;

            base.WndProc(ref m);
        }

        protected override void OnParentForeColorChanged(EventArgs e) {
            base.OnParentForeColorChanged(e);

            if (_matchParentForeColor)
                ForeColor = Parent.ForeColor;
        }
    }
}
