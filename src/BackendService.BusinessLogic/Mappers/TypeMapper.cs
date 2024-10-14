using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFilesTask.Models;
using BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.Models;
using BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand.Models;
using BackendService.BusinessLogic.Tasks.GetFileInfos.Models;
using BackendService.BusinessLogic.Tasks.PathsPreparation.Models.Request;
using BackendService.BusinessLogic.Tasks.PathsPreparation.Models.Response;
using Common;
using Common.UpdateFiles;
using Common.UploadFiles;

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
    
    public static UploadFilesCommand ToUploadFilesCommand(this SendUploadFilesCommandTaskRequest sendUploadFilesCommandTaskRequest)
    {
        return new UploadFilesCommand(sendUploadFilesCommandTaskRequest.SendUploadFilesData.Select(f => new UploadFiles(f.FileName, f.FileCode)));
    }
    
    public static UpdateFilesCommand ToUpdateFilesCommand(this SendUpdateFilesCommandTaskRequest sendUpdateFilesCommandTaskRequest)
    {
        return new UpdateFilesCommand(sendUpdateFilesCommandTaskRequest.SendUpdateFilesData.Select(f => new UpdateFiles(f.FileName, f.FileCode)));
    }
}