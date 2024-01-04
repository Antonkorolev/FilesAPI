using BackendService.BusinessLogic.Helpers.Models.Request;
using BackendService.BusinessLogic.Helpers.Models.Response;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFileInfosTask.Models;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask.Models;

namespace BackendService.BusinessLogic.Mappers;

public static class TypeMapper
{
    public static FileStream ToFileStream(this Stream stream)
    {
        return stream as FileStream ?? throw new InvalidCastException();
    }

    public static PathBuilderRequest ToPathBuilderRequest(this GetFileInfosTaskResponse getFileInfosTaskResponse)
    {
        return new PathBuilderRequest(getFileInfosTaskResponse.FileInfos.Select(f => new PathBuilderFileInfo(f.Code.ToString(), f.Name)));
    }

    public static GetFilesTaskRequest ToGetFilesTaskRequest(this PathBuilderResponse pathBuilderResponse)
    {
        return new GetFilesTaskRequest(pathBuilderResponse.FileData.Select(f => new GetFilesTaskFileData(f.FileName, f.Path)));
    }
}