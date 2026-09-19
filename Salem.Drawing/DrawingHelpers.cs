using Microsoft.Win32;
using Salem.Drawing.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace Salem.Drawing {
    public static class DrawingHelpers {
        //[Style, Direction]
        private static readonly char[,] _arrowStyles = new char[7, 4] {
            {'\uE70E', '\uE70D', '\uE76B', '\uE76C' },
            {'\uE96D', '\uE96E', '\uE96F', '\uE970' },
            {'\uE971', '\uE972', '\uE973', '\uE974' },
            {'\uF0AD', '\uF0AE', '\uF0B0', '\uF0AF' },
            {'\uE74A', '\uE74B', '\uE72B', '\uF0D2' },
            {'\uEDDB', '\uEDDC', '\uEDD9', '\uEDDA' },
            {'\uE935', '\uE936', '\uE938', '\uE937' }
        };

        /// <summary>
        /// Fills the specified rectangle on the provided graphics surface with the given color.
        /// </summary>
        /// <param name="graphics">The graphics surface on which to fill the rectangle. Cannot be null.</param>
        /// <param name="rect">The rectangle to fill, specified in the coordinate space of the graphics surface.</param>
        /// <param name="color">The color to use when filling the rectangle.</param>
        public static void FillRect(Graphics graphics, Rectangle rect, Color color) => graphics.FillRectangle(new SolidBrush(color), rect);

        /// <summary>
        /// Draws a simple rectangular border on the specified graphics surface using the given color and line width.
        /// </summary>
        /// <remarks>The border is drawn inside the bounds of the specified rectangle. This method does
        /// not fill the rectangle; it only draws the outline.</remarks>
        /// <param name="graphics">The graphics surface on which to draw the border. Cannot be null.</param>
        /// <param name="rect">The rectangle that defines the bounds of the border to draw.</param>
        /// <param name="color">The color of the border.</param>
        /// <param name="width">The width, in pixels, of the border line. Must be greater than 0.</param>
        public static void DrawSimpleBorder(Graphics graphics, Rectangle rect, Color color, float width) {
            using (GraphicsPath _path = new GraphicsPath())
            using (Pen _pen = new Pen(color, width)) {
                _pen.Alignment = PenAlignment.Inset;

                _path.AddRectangle(rect);
                graphics.DrawPath(_pen, _path);
            }
        }

        /// <summary>
        /// Returns a new Rectangle that is offset by the specified horizontal and vertical amounts.
        /// </summary>
        /// <param name="rect">The Rectangle to offset.</param>
        /// <param name="x">The amount, in pixels, to offset the Rectangle horizontally. Positive values move the Rectangle to the
        /// right; negative values move it to the left.</param>
        /// <param name="y">The amount, in pixels, to offset the Rectangle vertically. Positive values move the Rectangle downward;
        /// negative values move it upward.</param>
        /// <returns>A Rectangle that is the result of offsetting the input Rectangle by the specified amounts.</returns>
        public static Rectangle OffsetRect(Rectangle rect, int x, int y) {
            rect.Offset(x, y);
            return rect;
        }

        /// <summary>
        /// Returns a new Rectangle that is offset from the specified rectangle by the coordinates of the specified
        /// point.
        /// </summary>
        /// <param name="rect">The Rectangle to offset.</param>
        /// <param name="p">A Point whose X and Y values specify the horizontal and vertical distances to offset the rectangle.</param>
        /// <returns>A Rectangle that is the result of offsetting the original rectangle by the specified point.</returns>
        public static Rectangle OffsetRect(Rectangle rect, Point p) => OffsetRect(rect, p.X, p.Y);

        /// <summary>
        /// Returns a new rectangle with the same size as the specified rectangle, but with its origin translated to (0,
        /// 0).
        /// </summary>
        /// <param name="rect">The rectangle to translate so that its top-left corner is at the origin.</param>
        /// <returns>A rectangle with the same width and height as <paramref name="rect"/>, but with its X and Y coordinates set
        /// to 0.</returns>
        public static Rectangle TranslateOrigin(Rectangle rect) => OffsetRect(rect, -rect.X, -rect.Y);

        /// <summary>
        /// Applies a custom shape to the specified control and renders it using the provided graphics context.
        /// </summary>
        /// <remarks>This method both draws the custom shape onto the control and updates the control's
        /// region to match the specified shape. Use this method to visually and interactively apply non-rectangular
        /// shapes to controls.</remarks>
        /// <param name="control">The control to which the custom shape will be applied. Cannot be null.</param>
        /// <param name="graphics">The graphics context used to render the custom shape. Cannot be null.</param>
        /// <param name="shapeInfo">The shape information that defines the appearance and region of the custom shape. Cannot be null.</param>
        public static void ApplyCustomShape(Control control, Graphics graphics, ShapeInfo shapeInfo) {
            DrawCustomShape(control, graphics, shapeInfo, 1);
            ApplyCustomRegion(control, shapeInfo);
        }

        /// <summary>
        /// Draws a custom-shaped border onto the specified control using the provided graphics context and shape
        /// information.
        /// </summary>
        /// <remarks>If the control has a parent, the background is cleared using the parent's background
        /// color; otherwise, black is used. The method supports both rectangles and rectangles with individually
        /// rounded corners, as specified by the shape information.</remarks>
        /// <param name="control">The control on which the custom shape is to be drawn. The control's background color is used to fill the
        /// shape.</param>
        /// <param name="graphics">The graphics context used to render the shape. This must be valid and associated with the target control.</param>
        /// <param name="shapeInfo">An object containing details about the shape to draw, including border size, border color, dash style, and
        /// corner radii.</param>
        /// <param name="shrinkingAmount">The number of pixels by which to shrink the shape from each edge. Must be greater than or equal to 0. The
        /// default is 0.</param>
        public static void DrawCustomShape(Control control, Graphics graphics, ShapeInfo shapeInfo, short shrinkingAmount = 0) {
            graphics.Clear(control.Parent != null ? control.Parent.BackColor : Color.Black);

            ushort x = (ushort) (shapeInfo.BorderSize / 2 + shrinkingAmount);
            ushort y = (ushort) (shapeInfo.BorderSize / 2 + shrinkingAmount);
            ushort w = (ushort) (control.ClientRectangle.Width - shapeInfo.BorderSize - shrinkingAmount * 2);
            ushort h = (ushort) (control.ClientRectangle.Height - shapeInfo.BorderSize - shrinkingAmount * 2);

            if (shapeInfo.TopLeftRadius + shapeInfo.TopRightRadius + shapeInfo.BottomLeftRadius + shapeInfo.BottomRightRadius == 0) {
                using (Brush myBrush = new SolidBrush(control.BackColor))
                    graphics.FillRectangle(myBrush, x, y, w, h);

                if (shapeInfo.BorderSize > 0)
                    using (Pen _pen = new Pen(shapeInfo.BorderColor, shapeInfo.BorderSize) { DashStyle = shapeInfo.BorderDashStyle })
                        graphics.DrawRectangle(_pen, x, y, w, h);
            } else {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle rect = new Rectangle(x, y, w, h);

                using (GraphicsPath _basePath = InternalHelpers.CalculatePeripheralPath(rect, shapeInfo.TopLeftRadius, shapeInfo.TopRightRadius, shapeInfo.BottomLeftRadius, shapeInfo.BottomRightRadius)) {
                    using (Brush _brush = new SolidBrush(control.BackColor))
                        graphics.FillPath(_brush, _basePath);

                    if (shapeInfo.BorderSize > 0)
                        using (Pen _pen = new Pen(shapeInfo.BorderColor, shapeInfo.BorderSize) { DashStyle = shapeInfo.BorderDashStyle })
                            graphics.DrawPath(_pen, _basePath);
                }
            }
        }

        /// <summary>
        /// Applies a custom region to the specified control based on the provided shape information, enabling rounded
        /// corners or other non-rectangular shapes.
        /// </summary>
        /// <remarks>This method modifies the control's Region property to match the specified shape. If
        /// all corner radii are zero, the region will be set to the control's rectangular bounds. Otherwise, the region
        /// will reflect the custom shape defined by the corner radii. Changing the region may affect hit testing and
        /// rendering of the control.</remarks>
        /// <param name="control">The control to which the custom region will be applied. Cannot be null.</param>
        /// <param name="shapeInfo">An object containing the corner radius values and shape details used to define the region. Cannot be null.</param>
        public static void ApplyCustomRegion(Control control, ShapeInfo shapeInfo) {
            if (shapeInfo.TopLeftRadius + shapeInfo.TopRightRadius + shapeInfo.BottomLeftRadius + shapeInfo.BottomRightRadius == 0) {
                using (Region _region = new Region(control.ClientRectangle))
                    control.Region = _region;
            } else {
                using (Region _region = new Region(InternalHelpers.CalculatePeripheralPath(control.ClientRectangle, shapeInfo.TopLeftRadius, shapeInfo.TopRightRadius, shapeInfo.BottomLeftRadius, shapeInfo.BottomRightRadius)))
                    control.Region = _region;
            }
        }

        /// <summary>
        /// Returns the Unicode character representing an arrow for the specified direction and style in the "Segoe MDL2 Assets", "Segoe Fluent Icons" fonts.
        /// </summary>
        /// <param name="direction">The direction for which to retrieve the arrow character.</param>
        /// <param name="style">The visual style of the arrow to use.</param>
        public static char GetArrowChar(BasicDirections direction, ArrowStyles style) => _arrowStyles[(int) style, (int) (direction - 1)];
        
        /// <summary>
        /// Maps an ArrowDirection value to its corresponding BasicDirections value.
        /// </summary>
        /// <param name="direction">The ArrowDirection value to convert.</param>
        /// <returns>The matching BasicDirections value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the direction is not a valid ArrowDirection value.</exception>
        public static BasicDirections MapToBasicDirections(ArrowDirection direction) {
            switch (direction) {
                case ArrowDirection.Up: return BasicDirections.Up;
                case ArrowDirection.Down: return BasicDirections.Down;
                case ArrowDirection.Left: return BasicDirections.Left;
                case ArrowDirection.Right: return BasicDirections.Right;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Retrieves the current system color mode preference for applications.
        /// </summary>
        /// <returns>The current color mode as specified by the system theme setting.</returns>
        public static ColorModes GetSystemTheme() => (ColorModes) (1 - (int) (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 1) ?? 1));
        
        public static string[] GetInstalledFontFamilyNames(bool includeWeightSuffixes) {
            using (var _fonts = new InstalledFontCollection())
                return includeWeightSuffixes ? _fonts.Families.Select(f => f.Name).ToArray() : _fonts.Families.Select(f => StripFontWeightSuffix(f.Name)).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(name => name).ToArray();
        }
        
        public static string[] GetInstalledFontFamilyNames(bool includeWeightSuffixes, FontStyle filterByStyle) {
            using (var _fonts = new InstalledFontCollection())
                return includeWeightSuffixes ? _fonts.Families.Where(f => f.IsStyleAvailable(filterByStyle)).Select(f => f.Name).ToArray() : _fonts.Families.Where(f => f.IsStyleAvailable(filterByStyle)).Select(f => StripFontWeightSuffix(f.Name)).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(name => name).ToArray();
        }

        public static string StripFontWeightSuffix(string familyName) {
            int _suffixInx;

            foreach (string _suffix in DrawingHelpers.FontWeightSuffixes)
                if ((_suffixInx = familyName.IndexOf($" {_suffix}", StringComparison.OrdinalIgnoreCase)) > -1)
                    return familyName.Substring(0, _suffixInx);

            return familyName;
        }

        public static readonly HashSet<string> FontWeightSuffixes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "Thin", "ExtraLight", "UltraLight", "Light", "Lt", "Semil", "Regular", "Medium",
            "Semib", "SemBd", "Semi", "DemiBold", "Bold", "ExtraBold", "UltraBold", "Black",
            "Heavy", "Narrow", "Condensed", "Expanded", "Italic", "Oblique"
        };
    }
}
