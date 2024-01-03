namespace BackendService.BusinessLogic.Tasks.GetFileInfoTask.Models;

public sealed class GetFileInfoTaskResponse
{
    public GetFileInfoTaskResponse(int fileInfoId, Guid code, string name)
    {
        FileInfoId = fileInfoId;
        Code = code;
        Name = name;
    }


    public int FileInfoId { get; set; }

    public Guid Code { get; set; }

    public string Name { get; set; }
}