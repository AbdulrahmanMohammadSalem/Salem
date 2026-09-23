using System.Linq;
using System.Text;

namespace Salem.Extensions {
    public static class StringExtensions {
        public static string ToIdentifier(this string str, bool replaceInvalidCharsWithUnderscores = true) {
            var _result = new StringBuilder();
            int i = 0;
            const string FORBIDDEN = " '\"@#$%^?~\\+-*/&|!.,;:(){}[]\t\n";

            if (char.IsDigit(str[i])) {
                _result.Append('_');
                i++;
            } else if (str[i] == '@') {
                _result.Append('@');
                i++;
            }

            if (replaceInvalidCharsWithUnderscores) {
                for (; i < str.Length; i++) {
                    if (FORBIDDEN.Contains(str[i]) || char.IsControl(str[i]))
                        _result.Append('_');
                    else
                        _result.Append(str[i]);
                }
            } else {
                for (; i < str.Length; i++)
                    if (!FORBIDDEN.Contains(str[i]) && !char.IsControl(str[i]))
                        _result.Append(str[i]);
            }

            return _result.ToString();
        }
    }
}
