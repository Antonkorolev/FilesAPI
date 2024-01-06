using BackendService.BusinessLogic.Tasks.PathsPreparationTask.Models.Request;
using BackendService.BusinessLogic.Tasks.PathsPreparationTask.Models.Response;

namespace BackendService.BusinessLogic.Tasks.PathsPreparationTask;

public interface IPathsPreparationTask
{
    PathsPreparationTaskResponse PreparePaths(PathsPreparationTaskRequest request);
}