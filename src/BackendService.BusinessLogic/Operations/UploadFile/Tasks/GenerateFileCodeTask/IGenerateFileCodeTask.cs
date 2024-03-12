namespace BackendService.BusinessLogic.Operations.UploadFile.Tasks.GenerateFileCodeTask;

public interface IGenerateFileCodeTask
{
    Task<string> GenerateAsync(Stream stream);
}