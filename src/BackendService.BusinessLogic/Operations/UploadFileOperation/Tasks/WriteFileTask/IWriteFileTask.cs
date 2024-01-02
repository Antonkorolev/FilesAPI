namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.WriteFileTask;

public interface IWriteFileTask
{
    Task WriteAsync(Stream stream, string path);
}