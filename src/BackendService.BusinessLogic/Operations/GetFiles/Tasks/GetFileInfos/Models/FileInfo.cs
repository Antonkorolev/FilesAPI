namespace BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfos.Models;

public sealed class FileInfo
{
    public FileInfo(int fileInfoId, string code, string name)
    {
        FileInfoId = fileInfoId;
        Code = code;
        Name = name;
    }


    public int FileInfoId { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }
}