using BackendService.BusinessLogic.Mappers;
using BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand.Models;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand;

public sealed class SendUploadFilesCommandTask : ISendUploadFilesCommandTask
{
    private readonly IMessageSession _messageSession;
    private readonly ILogger<SendUploadFilesCommandTask> _logger;

    public SendUploadFilesCommandTask(IMessageSession messageSession, ILogger<SendUploadFilesCommandTask> logger)
    {
        _messageSession = messageSession;
        _logger = logger;
    }

    public async Task SendAsync(SendUploadFilesCommandTaskRequest request)
    {
        var command = request.ToUploadFilesCommand();

        var sendOption = new SendOptions();
        sendOption.SetDestination(Endpoints.BackendEndpoint);

        var status = await _messageSession.Request<Status>(command, sendOption).ConfigureAwait(false);

        if (status != Status.OK)
            throw new Exception("Status not OK");

        _logger.LogInformation(
            $"Command with params: [FileNames: {string.Join(", ", request.SendUploadFilesData.Select(t => t.FileName))}, " +
            $"FilesPaths : {string.Join(", ", request.SendUploadFilesData.Select(t => t.FilePath))}] successfully sent");
    }
}