using BackendService.BusinessLogic.Operations.DeleteFiles.Models;

namespace BackendService.BusinessLogic.Operations.DeleteFiles;

public interface IDeleteFilesOperation
{
    Task DeleteAsync(DeleteFilesOperationRequest request);
}