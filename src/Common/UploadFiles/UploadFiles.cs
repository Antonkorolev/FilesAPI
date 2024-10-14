namespace Common.UploadFiles;

public sealed class UploadFiles(string fileName, string fileCode)
{
    public string FileName { get; set; } = fileName;

    public string FileCode { get; set; } = fileCode;
}