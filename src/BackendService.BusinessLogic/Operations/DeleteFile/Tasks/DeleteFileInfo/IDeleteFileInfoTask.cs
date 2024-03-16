namespace BackendService.BusinessLogic.Operations.DeleteFile.Tasks.DeleteFileInfo;

public interface IDeleteFileInfoTask
{
    Task DeleteFileAsync(int fileInfoId, CancellationToken cancellationToken);
}