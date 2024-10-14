using BackendService.BusinessLogic.Mappers;
using BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.Models;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UpdateFiles.Tasks;

public sealed class SendUpdateFilesCommandTask : ISendUpdateFilesCommandTask
{
    private readonly IMessageSession _messageSession;
    private readonly ILogger<SendUpdateFilesCommandTask> _logger;

    public SendUpdateFilesCommandTask(IMessageSession messageSession, ILogger<SendUpdateFilesCommandTask> logger)
    {
        _messageSession = messageSession;
        _logger = logger;
    }

    public async Task SendAsync(SendUpdateFilesCommandTaskRequest request)
    {
        var command = request.ToUpdateFilesCommand();

        var sendOption = new SendOptions();
        sendOption.SetDestination(Endpoints.ProcessingEndpoint);

        var filesChangeResponseMessage = await _messageSession.Request<FilesChangeResponseMessage>(command, sendOption).ConfigureAwait(false);

        if (filesChangeResponseMessage.Status != Status.OK)
            throw new Exception($"Status not OK, error message: {filesChangeResponseMessage.ErrorMessage}");

        _logger.LogInformation(
            $"Command with params: [ " 
                        + $"FileNames: {string.Join(", ", request.SendUpdateFilesData.Select(t => t.FileName))}, "
                        + $"FilesCodes : {string.Join(", ", request.SendUpdateFilesData.Select(t => t.FileCode))}] "
                        + "successfully sent");
    }
}