using BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand.Models;

namespace BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand;

public interface ISendUploadFilesCommandTask
{
    Task SendAsync(SendUploadFilesCommandTaskRequest request);
}