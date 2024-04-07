using Common;
using Microsoft.Extensions.Logging;
using ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent.Models;

namespace ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent;

public sealed class PublishFileUpdateEventOperation : IPublishFileUpdateEventOperation
{
    private readonly IMessageSession _messageSession;
    private readonly ILogger<PublishFileUpdateEventOperation> _logger;

    public PublishFileUpdateEventOperation(IMessageSession messageSession, ILogger<PublishFileUpdateEventOperation> logger)
    {
        _messageSession = messageSession;
        _logger = logger;
    }

    public async Task PublishEventAsync(PublishFileUpdateEventOperationRequest request)
    {
        var @event = new UpdateFileEvent
        {
            UpdateFileType = request.UpdateFileType,
            FilesNames = request.FilesNames
        };

        await _messageSession.Publish(@event).ConfigureAwait(false);

        _logger.LogInformation($"Event with params: [UpdateFileType: {request.UpdateFileType}, FilesNames : {string.Join(", ", request.FilesNames)}] successfully published");
    }
}