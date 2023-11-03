namespace Chapter._4._1.H.處方診斷系統.Utility;

public static class FileUtility
{
    public static string GetFilePath(string fileName)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var indexOf = currentDirectory.LastIndexOf("bin", StringComparison.Ordinal);
        var trimmedPath = currentDirectory[..indexOf];
        return Path.Combine(trimmedPath, fileName);
    }
}