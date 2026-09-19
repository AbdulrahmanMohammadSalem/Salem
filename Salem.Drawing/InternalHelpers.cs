using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Salem.Drawing.Helpers {
    internal static class InternalHelpers {
        internal static int Clamp(int num, int from, int to) => num < from ? from : num > to ? to : num;

        internal static double Clamp(double num, double from, double to) => num < from ? from : num > to ? to : num;

        internal static GraphicsPath CalculatePeripheralPath(Rectangle rect, int topLeftRadius, int topRightRadius, int bottomLeftRadius, int bottomRightRadius) {
            GraphicsPath _path = new GraphicsPath();

            if (topLeftRadius > 0)
                _path.AddArc(rect.X, rect.Y, topLeftRadius * 2, topLeftRadius * 2, 180, 90);
            else
                _path.AddLine(rect.X, rect.Y, rect.X, rect.Y);

            if (topRightRadius > 0)
                _path.AddArc(rect.X + rect.Width - topRightRadius * 2, rect.Y, topRightRadius * 2, topRightRadius * 2, 270, 90); //TopRight
            else
                _path.AddLine(rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y);

            if (bottomRightRadius > 0)
                _path.AddArc(rect.X + rect.Width - bottomRightRadius * 2, rect.Y + rect.Height - bottomRightRadius * 2, bottomRightRadius * 2, bottomRightRadius * 2, 0, 90); //BottomRight
            else
                _path.AddLine(rect.X + rect.Width, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);

            if (bottomLeftRadius > 0)
                _path.AddArc(rect.X, rect.Y + rect.Height - bottomLeftRadius * 2, bottomLeftRadius * 2, bottomLeftRadius * 2, 90, 90); //BottomLeft
            else
                _path.AddLine(rect.X, rect.Y + rect.Height, rect.X, rect.Y + rect.Height);

            _path.CloseFigure();

            return _path;
        }
    }
}
