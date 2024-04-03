using BackendService.BusinessLogic.Operations.GetFile;
using BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand.Models;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand;

public sealed class SendUpdateFilesCommandTask : ISendUpdateFilesCommandTask
{
    private readonly IMessageSession _messageSession;
    
    private readonly ILogger<GetFileOperation> _logger;

    public SendUpdateFilesCommandTask(IMessageSession messageSession, ILogger<GetFileOperation> logger)
    {
        _messageSession = messageSession;
        _logger = logger;
    }

    public async Task SendAsync(SendUpdateFilesCommandTaskRequest request)
    {
        var command = new UpdateFilesCommand
        {
            UpdateFileType = request.UpdateFileType,
            FilesNames = request.FilesNames
        };

        var sendOption = new SendOptions();
        sendOption.SetDestination(Endpoints.BackendEndpoint);

        await _messageSession.Send(command, sendOption).ConfigureAwait(false);
        
        _logger.LogInformation($"Command with params: [UpdateFileType: {request.UpdateFileType}, FilesNames : {string.Join(", ", request.FilesNames)}] successfully sent");
    }
}