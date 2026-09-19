using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Salem.Extensions {
    public static class ImagingExtensions {
        /// <summary>
        /// Creates a grayscale instance of the source image and returns a reference to that instance. It doesn't take ownership of the new instance and thus you should dispose of it yourself.
        /// </summary>
        /// <returns>A <see cref="Bitmap"/> representing a grayscale version of the source image.</returns>
        public static Bitmap ToGrayscale(this Image src) {
            var _matrix = new ColorMatrix(
                new float[][] {
                    new float[] { 0.299F, 0.299F, 0.299F, 0, 0 },
                    new float[] { 0.587F, 0.587F, 0.587F, 0, 0 },
                    new float[] { 0.114F, 0.114F, 0.114F, 0, 0 },
                    new float[] { 0, 0, 0, 1, 0 },
                    new float[] { 0, 0, 0, 0, 1 }
                }
            );

            var _grayscale = new Bitmap(src.Width, src.Height);
            
            using (var _graphics = Graphics.FromImage(_grayscale))
            using (var _attrs = new ImageAttributes()) {
                _attrs.SetColorMatrix(_matrix);
                _graphics.DrawImage(src, new Rectangle(0, 0, src.Width, src.Height), 0, 0, src.Width, src.Height, GraphicsUnit.Pixel, _attrs);
                return _grayscale;
            }
        }

        public static byte[] ToArray(this Image src, ImageFormat format) {
            using (var _stream = new MemoryStream()) {
                src.Save(_stream, format);
                return _stream.ToArray();
            }
        }
    }
}
