using BackendService.BusinessLogic.Operations.UpdateFileOperation.Models;

namespace BackendService.BusinessLogic.Operations.UpdateFileOperation;

public interface IUpdateFileOperation
{
    Task UpdateAsync(UpdateFileOperationRequest request);
}