using BackendService.BusinessLogic.Operations.UpdateFiles.Models;

namespace BackendService.BusinessLogic.Operations.UpdateFiles;

public interface IUpdateFilesOperation
{
    Task UpdateAsync(UpdateFilesOperationRequest request);
}