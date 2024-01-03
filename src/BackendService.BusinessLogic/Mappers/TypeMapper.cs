using BackendService.BusinessLogic.Helpers.Models;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFileInfosTask.Models;

namespace BackendService.BusinessLogic.Extensions;

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
}