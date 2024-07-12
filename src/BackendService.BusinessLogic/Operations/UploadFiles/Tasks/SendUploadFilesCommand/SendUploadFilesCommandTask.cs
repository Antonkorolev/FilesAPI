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
        sendOption.SetDestination(Endpoints.ProcessingEndpoint);

        var filesChangeResponseMessage = await _messageSession.Request<FilesChangeResponseMessage>(command, sendOption).ConfigureAwait(false);

        if (filesChangeResponseMessage.Status != Status.OK)
            throw new Exception($"Status not OK, error message: {filesChangeResponseMessage.ErrorMessage}");

        _logger.LogInformation(
            $"Command with params: [FileNames: {string.Join(", ", request.SendUploadFilesData.Select(t => t.FileName))}, " +
            $"FilesCodes : {string.Join(", ", request.SendUploadFilesData.Select(t => t.FileCode))}] successfully sent");
    }
}