namespace gift_shop.Helpers;

public static class FileHelper
{
    private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
    private static readonly string[] AllowedDocumentExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt" };
    private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

    /// <summary>
    /// Validates file extension
    /// </summary>
    public static bool IsValidImageFile(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return AllowedImageExtensions.Contains(extension);
    }

    /// <summary>
    /// Validates document file extension
    /// </summary>
    public static bool IsValidDocumentFile(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return AllowedDocumentExtensions.Contains(extension);
    }

    /// <summary>
    /// Checks if file size is within acceptable limit
    /// </summary>
    public static bool IsValidFileSize(long fileSize)
    {
        return fileSize > 0 && fileSize <= MaxFileSize;
    }

    /// <summary>
    /// Generates a unique file name
    /// </summary>
    public static string GenerateUniqueFileName(string originalFileName)
    {
        var fileName = Path.GetFileNameWithoutExtension(originalFileName);
        var extension = Path.GetExtension(originalFileName);
        var uniqueFileName = $"{fileName}_{Guid.NewGuid().ToString().Substring(0, 8)}_{DateTime.UtcNow.Ticks}{extension}";
        return uniqueFileName;
    }

    /// <summary>
    /// Gets file type from extension
    /// </summary>
    public static string GetFileType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return extension switch
        {
            ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" or ".webp" => "Image",
            ".pdf" or ".doc" or ".docx" or ".xls" or ".xlsx" or ".txt" => "Document",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Converts file size to human readable format
    /// </summary>
    public static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;

        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }

        return $"{len:0.##} {sizes[order]}";
    }

    /// <summary>
    /// Deletes a file if it exists
    /// </summary>
    public static bool DeleteFileIfExists(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Creates directory if it doesn't exist
    /// </summary>
    public static bool CreateDirectoryIfNotExists(string directoryPath)
    {
        try
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
