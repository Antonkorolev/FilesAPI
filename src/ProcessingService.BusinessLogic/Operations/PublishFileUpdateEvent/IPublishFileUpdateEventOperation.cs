using ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent.Models;

namespace ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent;

public interface IPublishFileUpdateEventOperation
{
    Task PublishEventAsync(PublishFileUpdateEventOperationRequest request);
}