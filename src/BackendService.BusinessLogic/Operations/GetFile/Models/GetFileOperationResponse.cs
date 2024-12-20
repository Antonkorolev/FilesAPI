namespace BackendService.BusinessLogic.Operations.GetFile.Models;

public sealed class GetFileOperationResponse
{
    public GetFileOperationResponse(string fileName, Stream stream)
    {
        FileName = fileName;
        Stream = stream;
    }

    public string FileName { get; set; }

    public Stream Stream { get; set; }
}