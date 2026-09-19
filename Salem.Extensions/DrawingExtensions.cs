using System.Drawing;

namespace Salem.Extensions {
    public static class DrawingExtensions {
        public static float GetLuminance(this Color color) => (float) ((0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255);

        public static Color GetContrastingTextColor(this Color color) => GetLuminance(color) > 0.5 ? Color.Black : Color.White;

        public static string ToHex(this Color color, bool includeHash = true) {
            string _result = color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

            return includeHash ? $"#{_result}" : _result;
        }

        public static string ToLongHex(this Color color, bool includeHash = true) {
            string _result = color.A.ToString("X2") + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

            return includeHash ? $"#{_result}" : _result;
        }

        public static bool IsDark(this Color color) => color.GetLuminance() < 0.5;

        public static bool IsLight(this Color color) => color.GetLuminance() >= 0.5;

        public static (float H, float S, float L) ToHsl(this Color color) => (color.GetHue(), color.GetSaturation(), color.GetBrightness());
    }
}
