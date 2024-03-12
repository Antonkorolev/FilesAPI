using BackendService.BusinessLogic.Operations.UpdateFile.Models;

namespace BackendService.BusinessLogic.Operations.UpdateFile;

public interface IUpdateFileOperation
{
    Task UpdateAsync(UpdateFileOperationRequest request);
}