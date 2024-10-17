namespace ProcessingService.BusinessLogic.Tasks.GetFileInfos.Models;

public sealed class FileInfo(int fileInfoId, string code, string name)
{
    public int FileInfoId { get; set; } = fileInfoId;

    public string Code { get; set; } = code;

    public string Name { get; set; } = name;
}