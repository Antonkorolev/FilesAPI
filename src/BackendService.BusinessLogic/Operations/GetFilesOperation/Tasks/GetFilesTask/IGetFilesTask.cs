namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;

public interface IGetFilesTask
{
    IEnumerable<Stream> Get(IEnumerable<string> paths);
}