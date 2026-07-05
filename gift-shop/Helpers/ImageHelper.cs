using System.Drawing;
using System.Drawing.Imaging;

namespace gift_shop.Helpers;

public static class ImageHelper
{
    private const int MaxImageWidth = 2000;
    private const int MaxImageHeight = 2000;
    private const int ThumbNailWidth = 200;
    private const int ThumbNailHeight = 200;

    /// <summary>
    /// Validates if file is a valid image
    /// </summary>
    public static bool IsValidImage(string filePath)
    {
        try
        {
            using (var image = Image.FromFile(filePath))
            {
                return image.Width > 0 && image.Height > 0;
            }
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets image dimensions
    /// </summary>
    public static (int width, int height) GetImageDimensions(string filePath)
    {
        try
        {
            using (var image = Image.FromFile(filePath))
            {
                return (image.Width, image.Height);
            }
        }
        catch
        {
            return (0, 0);
        }
    }

    /// <summary>
    /// Resizes image to specified dimensions
    /// </summary>
    public static bool ResizeImage(string sourcePath, string destPath, int width, int height)
    {
        try
        {
            using (var image = Image.FromFile(sourcePath))
            {
                var resized = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resized))
                {
                    graphics.DrawImage(image, 0, 0, width, height);
                    resized.Save(destPath, ImageFormat.Jpeg);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Creates thumbnail of the image
    /// </summary>
    public static bool CreateThumbnail(string sourcePath, string destPath, int width = ThumbNailWidth, int height = ThumbNailHeight)
    {
        return ResizeImage(sourcePath, destPath, width, height);
    }

    /// <summary>
    /// Crops image to specified dimensions
    /// </summary>
    public static bool CropImage(string sourcePath, string destPath, int x, int y, int width, int height)
    {
        try
        {
            using (var image = Image.FromFile(sourcePath))
            {
                var cropped = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(cropped))
                {
                    graphics.DrawImage(image, new Rectangle(0, 0, width, height), 
                        new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
                    cropped.Save(destPath, ImageFormat.Jpeg);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Rotates image
    /// </summary>
    public static bool RotateImage(string sourcePath, string destPath, float angle)
    {
        try
        {
            using (var image = Image.FromFile(sourcePath))
            {
                var rotated = new Bitmap(image.Width, image.Height);
                using (var graphics = Graphics.FromImage(rotated))
                {
                    graphics.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
                    graphics.RotateTransform(angle);
                    graphics.TranslateTransform(-(float)image.Width / 2, -(float)image.Height / 2);
                    graphics.DrawImage(image, new Point(0, 0));
                    rotated.Save(destPath, ImageFormat.Jpeg);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validates image dimensions
    /// </summary>
    public static bool IsValidImageDimension(string filePath)
    {
        var (width, height) = GetImageDimensions(filePath);
        return width > 0 && height > 0 && width <= MaxImageWidth && height <= MaxImageHeight;
    }

    /// <summary>
    /// Converts image format
    /// </summary>
    public static bool ConvertImageFormat(string sourcePath, string destPath, ImageFormat format)
    {
        try
        {
            using (var image = Image.FromFile(sourcePath))
            {
                image.Save(destPath, format);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Generates image URL for display
    /// </summary>
    public static string GenerateImageUrl(string baseUrl, string fileName)
    {
        return $"{baseUrl.TrimEnd('/')}/images/{fileName}";
    }
}
