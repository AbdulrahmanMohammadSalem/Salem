using System.Drawing;
using System.Drawing.Drawing2D;

namespace Salem.Drawing {
    /// <summary>
    /// Represents the border and corner radius settings for a rectangular shape.
    /// </summary>
    /// <remarks>Use this structure to specify the appearance of a shape's border, including the size and
    /// style of each corner radius, border thickness, color, and dash style. All values are expressed in
    /// device-independent units where applicable.</remarks>
    public struct ShapeInfo {
        public ushort TopLeftRadius { get; set; }
        public ushort TopRightRadius { get; set; }
        public ushort BottomLeftRadius { get; set; }
        public ushort BottomRightRadius { get; set; }
        public ushort BorderSize { get; set; }
        public Color BorderColor { get; set; }
        public DashStyle BorderDashStyle { get; set; }
    }
}
