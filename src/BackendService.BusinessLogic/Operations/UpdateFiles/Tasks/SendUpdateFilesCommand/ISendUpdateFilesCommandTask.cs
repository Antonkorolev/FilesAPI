using BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.SendUpdateFilesCommand.Models;

namespace BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.SendUpdateFilesCommand;

public interface ISendUpdateFilesCommandTask
{
    Task SendAsync(SendUpdateFilesCommandTaskRequest request);
}