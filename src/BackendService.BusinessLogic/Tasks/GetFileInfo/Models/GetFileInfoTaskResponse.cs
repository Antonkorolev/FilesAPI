namespace BackendService.BusinessLogic.Tasks.GetFileInfo.Models;

public sealed class GetFileInfoTaskResponse
{
    public GetFileInfoTaskResponse(int fileInfoId, string code, string name)
    {
        FileInfoId = fileInfoId;
        Code = code;
        Name = name;
    }


    public int FileInfoId { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }
}