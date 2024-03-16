using BackendService.BusinessLogic.Tasks.PathsPreparation.Models.Request;
using BackendService.BusinessLogic.Tasks.PathsPreparation.Models.Response;

namespace BackendService.BusinessLogic.Tasks.PathsPreparation;

public interface IPathsPreparationTask
{
    PathsPreparationTaskResponse PreparePaths(PathsPreparationTaskRequest request);
}