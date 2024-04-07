using Common;
using ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent;
using ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent.Models;

namespace ProcessingService.Handlers;

public sealed class UpdateFilesCommandHandler : IHandleMessages<UpdateFilesCommand>
{
    private readonly IPublishFileUpdateEventOperation _publishFileUpdateEventOperation;

    public UpdateFilesCommandHandler(IPublishFileUpdateEventOperation publishFileUpdateEventOperation)
    {
        _publishFileUpdateEventOperation = publishFileUpdateEventOperation;
    }

    public Task Handle(UpdateFilesCommand message, IMessageHandlerContext context)
    {
        return _publishFileUpdateEventOperation.PublishEventAsync(new PublishFileUpdateEventOperationRequest((int)message.UpdateFileType, message.FilesNames));
    }
}