using Common.Notification;
using ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent;
using ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent.Models;

namespace ProcessingService.Handlers;

public sealed class NotificationCommandHandler : IHandleMessages<NotificationCommand>
{
    private readonly IPublishNotificationEventOperation _publishNotificationEventOperation;

    public NotificationCommandHandler(IPublishNotificationEventOperation publishNotificationEventOperation)
    {
        _publishNotificationEventOperation = publishNotificationEventOperation;
    }

    public Task Handle(NotificationCommand message, IMessageHandlerContext context)
    {
        return _publishNotificationEventOperation.PublishEventAsync(new PublishNotificationEventOperationRequest((int)message.UpdateFileType, message.FilesNames));
    }
}