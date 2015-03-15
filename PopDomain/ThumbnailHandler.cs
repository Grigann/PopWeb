//-----------------------------------------------------------------------
// <copyright file="ThumbnailHandler.cs" company="Laurent Perruche-Joubert">
//     © 2014-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain {
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;

    /// <summary>
    /// Dirty static class just use to resize an image.
    /// </summary>
    public class ThumbnailHandler {
        /// <summary>
        /// Gets the name of the medium thumbnail (for the timeline index).
        /// </summary>
        /// <param name="fileName">Name of the original file.</param>
        /// <returns>Name of the medium thumbnail.</returns>
        public static string GetMedThumbName(string fileName) {
            if (string.IsNullOrEmpty(fileName)) {
                return string.Empty;
            }

            var withoutExt = Path.GetFileNameWithoutExtension(fileName);
            return "m_" + withoutExt + ".jpg";
        }

        /// <summary>
        /// Gets the name of the small thumbnail (for the items index).
        /// </summary>
        /// <param name="fileName">Name of the original file.</param>
        /// <returns>Name of the small thumbnail.</returns>
        public static string GetSmallThumbName(string fileName) {
            if (string.IsNullOrEmpty(fileName)) {
                return string.Empty;
            }

            var withoutExt = Path.GetFileNameWithoutExtension(fileName);
            return "s_" + withoutExt + ".jpg";
        }

        /// <summary>
        /// Creates all thumbs fro a given image.
        /// </summary>
        /// <param name="filePath">Path of the original file.</param>
        public static void CreateAllThumbs(string filePath) {
            using (var image = Image.FromFile(filePath)) {
                var directory = Path.GetDirectoryName(filePath);
                var fileName = Path.GetFileNameWithoutExtension(filePath);

                if (directory == null) {
                    throw new DirectoryNotFoundException("Unable to find the Images directory");
                }

                // Med thumb
                var medThumbName = GetMedThumbName(fileName);
                using (var medThumb = ResizeImage(image, -1, 160)) {
                    var medThumbPath = Path.Combine(directory, medThumbName);
                    medThumb.Save(medThumbPath);
                }

                // Small thumb
                var smallThumbName = GetSmallThumbName(fileName);
                using (var smallThumb = ResizeImage(image, -1, 65)) {
                    smallThumb.Save(Path.Combine(directory, smallThumbName));
                }
            }
        }

        /// <summary>
        /// Resizes the given image.
        /// </summary>
        /// <param name="image">The original image.</param>
        /// <param name="newWidth">The target width. If it is -1, then the new width will be calculated from the new height.</param>
        /// <param name="newHeight">The target height. If it is -1, then the new height will be calculated from the new width.</param>
        /// <returns>The image resized.</returns>
        public static Image ResizeImage(Image image, int newWidth, int newHeight) {
            if ((newWidth == -1) && (newHeight == -1)) {
                throw new ArgumentException("Both new height and width can't be -1");
            }

            var originalWidth = image.Width;
            var originalHeight = image.Height;

            if (newWidth == -1) {
                var percentHeight = newHeight / (float)originalHeight;
                newWidth = (int)(originalWidth * percentHeight);
            } else if (newHeight == -1) {
                var percentWidth = newWidth / (float)originalWidth;
                newHeight = (int)(originalHeight * percentWidth);
            }

            var newImage = (Image)new Bitmap(newWidth, newHeight);
            using (var graphicsHandle = Graphics.FromImage(newImage)) {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }
    }
}
