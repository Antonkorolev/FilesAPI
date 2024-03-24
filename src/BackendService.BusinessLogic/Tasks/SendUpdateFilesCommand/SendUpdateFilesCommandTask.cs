using BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand.Models;
using Common;

namespace BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand;

public sealed class SendUpdateFilesCommandTask : ISendUpdateFilesCommandTask
{
    private readonly IMessageSession _messageSession;

    public SendUpdateFilesCommandTask(IMessageSession messageSession)
    {
        _messageSession = messageSession;
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
    }
}