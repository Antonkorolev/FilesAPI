namespace BackendService.BusinessLogic.Operations.UploadFiles.Models;

public sealed class UploadFileData
{
    public UploadFileData(Stream stream, string fileName)
    {
        Stream = stream;
        FileName = fileName;
    }

    public Stream Stream { get; set; }

    public string FileName { get; set; }
}