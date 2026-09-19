using System;
using System.Drawing;
using Salem.Drawing.Helpers;

namespace Salem.Drawing {
    /// <summary>
    /// Represents a color in the HSL (Hue, Saturation, Lightness) color space with an alpha (transparency) component.
    /// </summary>
    /// <remarks>The HSL color model describes colors in terms of hue, saturation, and lightness, which can be
    /// more intuitive for certain color manipulations compared to the RGB model. The HslColor struct provides methods
    /// for creating HSL colors from ARGB or Color values and for converting HSL values back to standard Color
    /// instances. All component values are expected to be within their standard ranges: hue in degrees (typically
    /// 0–360), saturation and lightness as fractions (0–1), and alpha as a byte (0–255).</remarks>
    public struct HslColor {
        /// <summary>
        /// Gets or sets the alpha (transparency) component of the color.
        /// </summary>
        public byte Alpha { get; set; }
        /// <summary>
        /// Gets or sets the hue component of the color.
        /// </summary>
        public float Hue { get; set; }
        /// <summary>
        /// Gets or sets the saturation component of the color.
        /// </summary>
        public float Saturation { get; set; }
        /// <summary>
        /// Gets or sets the lightness component of the color.
        /// </summary>
        public float Lightness { get; set; }

        /// <summary>
        /// Creates a new HSL color with the specified alpha, hue, saturation, and lightness components.
        /// </summary>
        /// <param name="a">The alpha component of the color. Represents opacity, where 0 is fully transparent and 255 is fully opaque.</param>
        /// <param name="h">The hue component of the color, in degrees. Typically ranges from 0 to 360.</param>
        /// <param name="s">The saturation component of the color, as a value between 0 and 1, where 0 is fully desaturated and 1 is
        /// fully saturated.</param>
        /// <param name="l">The lightness component of the color, as a value between 0 and 1, where 0 is black and 1 is white.</param>
        /// <returns>A new HslColor instance with the specified alpha, hue, saturation, and lightness values.</returns>
        public static HslColor FromAhsl(byte a, float h, float s, float l) {
            HslColor result = new HslColor {
                Alpha = a,
                Hue = h,
                Saturation = s,
                Lightness = l
            };

            return result;
        }

        /// <summary>
        /// Creates a new HSL color from the specified ARGB (alpha, red, green, blue) values.
        /// </summary>
        /// <remarks>The resulting HSL color is computed from the provided ARGB values using standard
        /// color conversion. The alpha value is preserved in the resulting HslColor.</remarks>
        /// <param name="a">The alpha component of the color. Specifies the opacity, where 0 is fully transparent and 255 is fully
        /// opaque.</param>
        /// <param name="r">The red component of the color. Valid values are 0 through 255.</param>
        /// <param name="g">The green component of the color. Valid values are 0 through 255.</param>
        /// <param name="b">The blue component of the color. Valid values are 0 through 255.</param>
        /// <returns>An HslColor instance representing the color defined by the specified ARGB values.</returns>
        public static HslColor FromArgb(byte a, byte r, byte g, byte b) {
            Color color = Color.FromArgb(a, r, g, b);
            HslColor result = new HslColor {
                Alpha = a,
                Hue = color.GetHue(),
                Saturation = color.GetSaturation(),
                Lightness = color.GetBrightness()
            };

            return result;
        }

        /// <summary>
        /// Creates a new HslColor instance from the specified ARGB color.
        /// </summary>
        /// <param name="color">The Color to convert to an HSL representation.</param>
        /// <returns>An HslColor instance that represents the equivalent hue, saturation, and lightness values of the specified
        /// color.</returns>
        public static HslColor FromColor(Color color) {
            return FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Creates a color from the specified alpha, hue, saturation, and lightness (HSL) values.
        /// </summary>
        /// <remarks>If the saturation is 0, the resulting color is a shade of gray determined by the
        /// lightness value. The method clamps all calculated color channel values to the valid byte range (0 to
        /// 255).</remarks>
        /// <param name="a">The alpha component of the color. Valid values are 0 (fully transparent) to 255 (fully opaque).</param>
        /// <param name="h">The hue of the color, in degrees. Valid values are from 0 to 360, where 0 and 360 represent red, 120
        /// represents green, and 240 represents blue.</param>
        /// <param name="s">The saturation of the color, as a value between 0 and 1. A value of 0 produces a shade of gray; 1 produces
        /// the most vivid color.</param>
        /// <param name="l">The lightness of the color, as a value between 0 and 1. A value of 0 produces black; 1 produces white.</param>
        /// <returns>A Color structure representing the color defined by the specified alpha, hue, saturation, and lightness
        /// values.</returns>
        public static Color ToColor(byte a, float h, float s, float l) {
            if (s == 0) {
                byte lightness = (byte) InternalHelpers.Clamp((short) Math.Round(255 * l), 0, 255);

                return Color.FromArgb(a, lightness, lightness, lightness);
            }

            float r1, g1, b1;
            float chroma = (1 - Math.Abs(2 * l - 1)) * s;
            float hueSector = h / 60;
            float x = chroma * (1 - Math.Abs(hueSector % 2 - 1));

            if (hueSector >= 5) {
                r1 = chroma;
                g1 = 0;
                b1 = x;
            } else if (hueSector >= 4) {
                r1 = x;
                g1 = 0;
                b1 = chroma;
            } else if (hueSector >= 3) {
                r1 = 0;
                g1 = x;
                b1 = chroma;
            } else if (hueSector >= 2) {
                r1 = 0;
                g1 = chroma;
                b1 = x;
            } else if (hueSector >= 1) {
                r1 = x;
                g1 = chroma;
                b1 = 0;
            } else {
                r1 = chroma;
                g1 = x;
                b1 = 0;
            }

            float m = l - chroma / 2;

            r1 += m;
            g1 += m;
            b1 += m;

            return Color.FromArgb(
                a,
                InternalHelpers.Clamp((short) Math.Round(255 * r1), 0, 255),
                InternalHelpers.Clamp((short) Math.Round(255 * g1), 0, 255),
                InternalHelpers.Clamp((short) Math.Round(255 * b1), 0, 255)
            );
        }
    }
}
