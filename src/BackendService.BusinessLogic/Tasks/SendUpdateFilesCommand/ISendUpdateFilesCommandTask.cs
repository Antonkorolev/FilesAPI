using BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand.Models;

namespace BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand;

public interface ISendUpdateFilesCommandTask
{
    Task SendAsync(SendUpdateFilesCommandTaskRequest request);
}