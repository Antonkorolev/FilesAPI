using BackendService.BusinessLogic.Operations.GetFile;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Tasks.SendNotificationCommand;

public sealed class SendNotificationCommandTask : ISendNotificationCommandTask
{
    private readonly IMessageSession _messageSession;
    private readonly ILogger<GetFileOperation> _logger;

    public SendNotificationCommandTask(IMessageSession messageSession, ILogger<GetFileOperation> logger)
    {
        _messageSession = messageSession;
        _logger = logger;
    }

    public async Task SendAsync(SendNotificationCommandTaskRequest request)
    {
        var command = new NotificationCommand
        {
            UpdateFileType = request.UpdateFileType,
            FilesNames = request.FilesNames
        };

        var sendOption = new SendOptions();
        sendOption.SetDestination(Endpoints.ProcessingEndpoint);

        await _messageSession.Send(command, sendOption).ConfigureAwait(false);

        _logger.LogInformation($"Command with params: [UpdateFileType: {request.UpdateFileType}, FilesNames : {string.Join(", ", request.FilesNames)}] successfully sent");
    }
}