using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Salem.Controls {
    /// <summary>
    /// Represents a custom control box for a form, providing minimize, maximize/restore, and close window actions with
    /// customizable button icons and enablement.
    /// </summary>
    /// <remarks>Use this control to add standard window control buttons to a custom form, especially when the
    /// default form border is hidden or replaced. The control allows customization of the button icons via Unicode
    /// characters and enables or disables each action independently. It is typically anchored to the top-right corner
    /// of the parent form. The control automatically interacts with its parent form to perform window state changes and
    /// closing actions.</remarks>
    [Designer(typeof(FormControlBoxDesigner))]
    public partial class SalFormControlBox : UserControl {
        private Form _parentForm = null;

        private char _closeChar = '\ue8bb';
        private char _maximizeChar = '\ue922';
        private char _minimizeChar = '\ue921';
        private char _restoreChar = '\ue923';

        /// <summary>
        /// Gets or sets the character displayed on the close button.
        /// </summary>
        [DefaultValue('\ue8bb')]
        public char CloseChar {
            get => _closeChar;
            set {
                _closeChar = value;
                btn_close.Text = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the character displayed on the maximize button when the parent form is in the normal window
        /// state.
        /// </summary>
        [DefaultValue('\ue922')]
        public char MaximizeChar {
            get => _maximizeChar;
            set {
                _maximizeChar = value;

                if (_parentForm != null && _parentForm.WindowState == FormWindowState.Normal)
                    btn_maximize.Text = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the character displayed on the minimize button.
        /// </summary>
        [DefaultValue('\ue921')]
        public char MinimizeChar {
            get => _minimizeChar;
            set {
                _minimizeChar = value;
                btn_minimize.Text = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the character displayed on the maximize button when the parent form is maximized.
        /// </summary>
        [DefaultValue('\ue923')]
        public char RestoreChar {
            get => _restoreChar;
            set {
                _restoreChar = value;

                if (_parentForm != null && _parentForm.WindowState == FormWindowState.Maximized)
                    btn_maximize.Text = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the minimize button is enabled and visible.
        /// </summary>
        /// <remarks>Setting this property to <see langword="true"/> ensures that the minimize button is
        /// both enabled and visible. Setting it to <see langword="false"/> disables and hides the minimize button.
        /// Changing this property may also affect the visibility of the maximize button.</remarks>
        [DefaultValue(true)]
        public bool EnableMinimize {
            get => btn_minimize.Enabled && btn_minimize.Visible;
            set {
                btn_minimize.Enabled = btn_minimize.Visible = value;

                if (value)
                    btn_maximize.Visible = true;
                else
                    btn_maximize.Visible = btn_maximize.Enabled;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the maximize button is enabled and visible on the window.
        /// </summary>
        /// <remarks>When set to <see langword="true"/>, the maximize button is enabled and visible. When
        /// set to <see langword="false"/>, the maximize button is disabled and its visibility depends on the value of
        /// <see cref="EnableMinimize"/>. This property is typically used to control whether users can maximize the
        /// window.</remarks>
        [DefaultValue(true)]
        public bool EnableMaximize {
            get => btn_maximize.Enabled;
            set {
                btn_maximize.Enabled = value;

                if (value)
                    btn_maximize.Visible = true;
                else
                    btn_maximize.Visible = EnableMinimize;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the close button is enabled.
        /// </summary>
        [DefaultValue(true)]
        public bool EnableClose {
            get => btn_close.Enabled;
            set => btn_close.Enabled = value;
        }

        public override Color BackColor { 
            get => base.BackColor; 
            set {
                base.BackColor = value;
                btn_close.ForeColor = btn_maximize.ForeColor = btn_minimize.ForeColor = value.GetBrightness() > 0.6 ? Color.Black : Color.White;
            }
        }

        public SalFormControlBox() {
            InitializeComponent();
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        private void btn_close_MouseEnter(object sender, EventArgs e) => btn_close.ForeColor = Color.White;

        private void btn_close_MouseLeave(object sender, EventArgs e) => btn_close.ForeColor = btn_maximize.ForeColor;

        private void btn_close_Click(object sender, EventArgs e) => _parentForm?.Close();

        private void btn_maximize_Click(object sender, EventArgs e) {
            if (_parentForm == null)
                return;

            if (_parentForm.WindowState == FormWindowState.Normal) {
                _parentForm.WindowState = FormWindowState.Maximized;
                btn_maximize.Text = _restoreChar.ToString();
            } else {
                _parentForm.WindowState = FormWindowState.Normal;
                btn_maximize.Text = _maximizeChar.ToString();
            }
        }

        private void btn_minimize_Click(object sender, EventArgs e) {
            if (_parentForm != null)
                _parentForm.WindowState = FormWindowState.Minimized;
        }

        protected override void OnParentChanged(EventArgs e) {
            base.OnParentChanged(e);
            _parentForm = FindForm();
        }

        /// <summary>
        /// Adjusts the control's location so that it aligns with the top-right corner of its parent form's client area.
        /// </summary>
        /// <remarks>This method has no effect if the control does not have a parent form. Typically used
        /// to reposition the control after the parent form is resized or its layout changes.</remarks>
        public void CorrectLocation() {
            if (_parentForm != null)
                Location = new Point(_parentForm.ClientSize.Width - ClientSize.Width, 0);
        }
    }

    public class FormControlBoxDesigner : ControlDesigner {
        public override DesignerActionListCollection ActionLists {
            get => new DesignerActionListCollection { new FormControlBoxActionList(Component) };
        }
    }

    public class FormControlBoxActionList : DesignerActionList {
        private readonly SalFormControlBox _controlBox;

        public FormControlBoxActionList(IComponent component) : base(component) => _controlBox = (SalFormControlBox) component;

        public void RunCorrectLocationMethod() => _controlBox.CorrectLocation();

        public override DesignerActionItemCollection GetSortedActionItems() => new DesignerActionItemCollection { new DesignerActionMethodItem(this, nameof(RunCorrectLocationMethod), "Correct location", "Actions", "Puts this control in the upper-right corner of the form.", true) };
    }
}
