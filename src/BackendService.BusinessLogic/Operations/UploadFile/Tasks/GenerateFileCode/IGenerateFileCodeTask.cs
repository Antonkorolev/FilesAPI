namespace BackendService.BusinessLogic.Operations.UploadFile.Tasks.GenerateFileCode;

public interface IGenerateFileCodeTask
{
    Task<string> GenerateAsync(Stream stream);
}