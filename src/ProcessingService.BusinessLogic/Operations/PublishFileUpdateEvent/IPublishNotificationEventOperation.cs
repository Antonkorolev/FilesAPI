using ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent.Models;

namespace ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent;

public interface IPublishNotificationEventOperation
{
    Task PublishEventAsync(PublishNotificationEventOperationRequest request);
}