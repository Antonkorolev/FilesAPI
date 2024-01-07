namespace BackendService.Contracts;

public sealed class FileResponse
{
    public FileResponse(string name, Stream stream, string contentType)
    {
        Name = name;
        Stream = stream;
        ContentType = contentType;
    }

    public string Name { get; set; }

    public Stream Stream { get; set; }

    public string ContentType { get; set; }
}