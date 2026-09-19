using System;
using System.Linq;
using System.Windows.Forms;

namespace Salem.Utils {
    /// <summary>
    /// Provides static methods for handling various control events to achieve special behaviors.
    /// </summary>
    public static class SpecialEventHandlers {
        #region Fields/Constants
        private const char NEGATIVE_SIGN = '-';
        #endregion

        #region Public Methods
        /// <summary>
        /// Handles the <see cref="Control.KeyPress"/> event for a <see cref="TextBox"/> to allow only signed decimal input.
        /// </summary>
        /// <param name="sender">The <see cref="TextBox"/> control that receives the input.</param>
        /// <param name="e">The event data associated with the key press.</param>
        /// <param name="decimalPointChar">The character used as the decimal point, defaulting to '.'.</param>
        /// <exception cref="ArgumentNullException">Thrown when the sender parameter is <see langword="null"/>.</exception>
        public static void SignedDecimalInput(TextBox sender, KeyPressEventArgs e, char decimalPointChar = '.') {
            if (sender is null)
                throw new ArgumentNullException(nameof(sender));

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != decimalPointChar && e.KeyChar != NEGATIVE_SIGN && !char.IsControl(e.KeyChar)) {
                e.Handled = true;
                return;
            }

            if (sender.TextLength > 0 && char.IsDigit(e.KeyChar) && sender.SelectionStart == 0 && sender.Text[0] == NEGATIVE_SIGN) {
                sender.SelectionStart++; //...and it will be inserted automatically by the <see cref="TextBox"/>
            } else if (e.KeyChar == decimalPointChar) {
                if (sender.SelectionLength > 0) {
                    e.Handled = HandleDecimalPointKeyPressWithSelection(sender, decimalPointChar);
                    return;
                }

                int _firstPos = sender.Text.IndexOf(e.KeyChar);
                
                if (_firstPos == -1) {
                    if (sender.SelectionStart == 0 || sender.SelectionStart == 1 && sender.Text[0] == NEGATIVE_SIGN) {
                        
                        if (sender.SelectionStart == 0 && sender.TextLength > 0 && sender.Text[0] == NEGATIVE_SIGN)
                            sender.SelectionStart++;

                        _firstPos = sender.SelectionStart; //_originalSelectionStart
                        sender.Text = sender.Text.Insert(sender.SelectionStart, $"0{decimalPointChar}");
                        sender.SelectionStart = _firstPos + 2;

                        e.Handled = true;
                    }
                } else {
                    sender.SelectionStart = _firstPos + 1;
                    e.Handled = true;
                }
            } else if (e.KeyChar == NEGATIVE_SIGN)
                e.Handled = HandleNegativeSignKeyPress(sender, NEGATIVE_SIGN);
        }

        /// <summary>
        /// Handles the <see cref="Control.KeyPress"/> event for a <see cref="TextBox"/> to restrict input to unsigned decimal numbers using the specified
        /// decimal point character.
        /// </summary>
        /// <param name="sender">The <see cref="TextBox"/> control receiving the input.</param>
        /// <param name="e">The event data associated with the key press.</param>
        /// <param name="decimalPointChar">The character to use as the decimal point. Defaults to '.'.</param>
        /// <exception cref="ArgumentNullException">Thrown when sender is <see langword="null"/>.</exception>
        public static void UnsignedDecimalInput(TextBox sender, KeyPressEventArgs e, char decimalPointChar = '.') {
            if (sender is null)
                throw new ArgumentNullException(nameof(sender));

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != decimalPointChar && !char.IsControl(e.KeyChar)) {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == decimalPointChar) {
                if (sender.SelectionLength > 0) {
                    e.Handled = HandleDecimalPointKeyPressWithSelection(sender, decimalPointChar);
                    return;
                }
                
                int _firstPos = sender.Text.IndexOf(e.KeyChar);

                if (_firstPos == -1) {
                    if (sender.SelectionStart == 0) {
                        sender.Text = sender.Text.Insert(sender.SelectionStart, $"0{decimalPointChar}");
                        sender.SelectionStart = 2;

                        e.Handled = true;
                    }
                } else {
                    sender.SelectionStart = _firstPos + 1;
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="Control.KeyPress"/> event for a <see cref="TextBox"/> to restrict input to signed integer values.
        /// </summary>
        /// <param name="sender">The <see cref="TextBox"/> control receiving the input.</param>
        /// <param name="e">The event data associated with the key press.</param>
        /// <exception cref="ArgumentNullException">Thrown when the sender parameter is null.</exception>
        public static void SignedIntegerInput(TextBox sender, KeyPressEventArgs e) {
            if (sender is null)
                throw new ArgumentNullException(nameof(sender));

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != NEGATIVE_SIGN && !char.IsControl(e.KeyChar)) {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == NEGATIVE_SIGN)
                e.Handled = HandleNegativeSignKeyPress(sender, NEGATIVE_SIGN);
        }

        /// <summary>
        /// Handles the <see cref="Control.KeyPress"/> event for a <see cref="TextBox"/> to allow only digit and control characters.
        /// </summary>
        /// <param name="e">The event data associated with the key press.</param>
        public static void UnsignedIntegerInput(KeyPressEventArgs e) => e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        /// <summary>
        /// Handles numeric input for a <see cref="TextBox"/>, allowing for optional negative values and decimal points.
        /// </summary>
        /// <param name="sender">The <see cref="TextBox"/> control receiving the input.</param>
        /// <param name="e">The event data for the key press.</param>
        /// <param name="allowNegative"><see langword="true"/> to permit negative values; otherwise, <see langword="false"/>.</param>
        /// <param name="allowDecimal"><see langword="true"/> to permit decimal values; otherwise, <see langword="false"/>.</param>
        /// <param name="decimalPointChar">The character to use as the decimal point. Defaults to '.'.</param>
        public static void NumericInput(TextBox sender, KeyPressEventArgs e, bool allowNegative, bool allowDecimal, char decimalPointChar = '.') {
            if (allowNegative) {
                if (allowDecimal)
                    SignedDecimalInput(sender, e, decimalPointChar);
                else
                    SignedIntegerInput(sender, e);
            } else {
                if (allowDecimal)
                    UnsignedDecimalInput(sender, e, decimalPointChar);
                else
                    UnsignedIntegerInput(e);
            }
        }
        #endregion

        #region Private Methods
        private static bool HandleDecimalPointKeyPressWithSelection(TextBox sender, char decimalPointChar) {
            if (sender.SelectionLength == sender.TextLength) {
                sender.Text = $"0{decimalPointChar}";
                sender.SelectionStart = 2;

                return true;
            } else if (sender.SelectedText.Contains(decimalPointChar)) {
                if (sender.SelectionStart == 0) {
                    sender.Text = $"0{decimalPointChar}{sender.Text.Substring(sender.SelectionLength)}";
                    sender.SelectionStart = 2;

                    return true;
                }

                return false;
            } else {
                string _result = sender.Text.Remove(sender.SelectionStart, sender.SelectionLength);

                if (_result[0] == '.')
                    _result = $"0{_result}";

                sender.Text = _result;
                sender.SelectionStart = sender.Text.IndexOf(decimalPointChar) + 1;

                return true;
            }
        }

        private static bool HandleNegativeSignKeyPress(TextBox sender, char negativeSign) {
            if (sender.TextLength == 0)
                return false;

            int _originalSelectionStart = sender.SelectionStart;
            int _originalSelectionLength = sender.SelectionLength;

            if (sender.Text[0] == negativeSign) {
                sender.Text = sender.Text.Substring(1);
                sender.SelectionStart = _originalSelectionStart == 0 ? 0 : _originalSelectionStart - 1;

                if (_originalSelectionLength > 0) {
                    if (_originalSelectionStart == 0)
                        sender.Select(_originalSelectionStart, _originalSelectionLength - 1);
                    else
                        sender.Select(_originalSelectionStart - 1, _originalSelectionLength);
                }
            } else {
                sender.Text = $"{negativeSign}{sender.Text}";
                sender.SelectionStart = _originalSelectionStart + 1;

                if (_originalSelectionLength > 0)
                    sender.Select(_originalSelectionStart + 1, _originalSelectionLength);
            }

            return true;
        }
        #endregion
    }
}
