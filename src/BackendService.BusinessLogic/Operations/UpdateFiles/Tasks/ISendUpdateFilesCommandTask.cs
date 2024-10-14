using BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.Models;

namespace BackendService.BusinessLogic.Operations.UpdateFiles.Tasks;

public interface ISendUpdateFilesCommandTask
{
    Task SendAsync(SendUpdateFilesCommandTaskRequest request);
}