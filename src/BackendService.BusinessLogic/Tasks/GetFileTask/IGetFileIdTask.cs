namespace BackendService.BusinessLogic.Tasks.GetFileTask;

public interface IGetFileIdTask
{
    Task<int> GetFileIdAsync(Guid fileCode, CancellationToken cancellationToken);
}