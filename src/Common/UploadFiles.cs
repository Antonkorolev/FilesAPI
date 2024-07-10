namespace Common;

public sealed class UploadFiles
{
    public UploadFiles(string fileName, string filePath)
    {
        FileName = fileName;
        FilePath = filePath;
    }

    public string FileName { get; set; }

    public string FilePath { get; set; }
}