using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfos.Models;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFilesTask.Models;
using BackendService.BusinessLogic.Tasks.PathsPreparation.Models.Request;
using BackendService.BusinessLogic.Tasks.PathsPreparation.Models.Response;

namespace BackendService.BusinessLogic.Mappers;

public static class TypeMapper
{
    public static PathsPreparationTaskRequest ToPathsPreparationTaskRequest(this GetFileInfosTaskResponse getFileInfosTaskResponse)
    {
        return new PathsPreparationTaskRequest(getFileInfosTaskResponse.FileInfos.Select(f => new PathsPreparationTaskFileInfo(f.Code.ToString(), f.Name)));
    }

    public static GetFilesTaskRequest ToGetFilesTaskRequest(this PathsPreparationTaskResponse pathsPreparationTaskResponse)
    {
        return new GetFilesTaskRequest(pathsPreparationTaskResponse.FileData.Select(f => new GetFilesTaskFileData(f.FileName, f.Path)));
    }
}