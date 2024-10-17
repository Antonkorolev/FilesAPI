using ProcessingService.BusinessLogic.Operations.UpdateFiles.Models;

namespace ProcessingService.BusinessLogic.Operations.UpdateFiles;

public interface IUpdateFilesOperation
{
    Task UpdateFilesAsync(UpdateFilesOperationRequest request);
}