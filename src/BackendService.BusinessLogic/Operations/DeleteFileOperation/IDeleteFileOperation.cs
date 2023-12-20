using BackendService.BusinessLogic.Operations.DeleteFileOperation.Models;

namespace BackendService.BusinessLogic.Operations.DeleteFileOperation;

public interface IDeleteFileOperation
{
    Task DeleteFileAsync(DeleteFileOperationRequest request);
}