namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.GenerateFileCodeTask;

public interface IGenerateFileCodeTask
{
    Task<string> GenerateAsync(Stream stream);
}