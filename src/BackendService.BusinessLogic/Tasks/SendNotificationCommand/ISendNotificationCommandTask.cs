using BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;

namespace BackendService.BusinessLogic.Tasks.SendNotificationCommand;

public interface ISendNotificationCommandTask
{
    Task SendAsync(SendNotificationCommandTaskRequest request);
}