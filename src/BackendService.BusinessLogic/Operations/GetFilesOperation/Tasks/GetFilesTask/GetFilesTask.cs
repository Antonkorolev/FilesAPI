using BackendService.BusinessLogic.Abstractions;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask.Models;

namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;

public sealed class GetFilesTask : IGetFilesTask
{
    private readonly IZipArchiveWriter _zipArchiveWriter;

    public GetFilesTask(IZipArchiveWriter zipArchiveWriter)
    {
        _zipArchiveWriter = zipArchiveWriter;
    }

    public byte[] Get(GetFilesTaskRequest request)
    {
        var filesData = request.FileData.Select(f => (f.Path, f.FileName)).ToArray();

        return _zipArchiveWriter.WriteFilesToZip(filesData);
    }
}