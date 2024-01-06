using System.IO.Compression;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask.Models;

namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;

public sealed class GetFilesTask : IGetFilesTask
{
    public byte[] Get(GetFilesTaskRequest request)
    {
        using var memoryStream = new MemoryStream();

        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
        {
            foreach (var getFilesTaskFileData in request.FileData)
            {
                archive.CreateEntryFromFile(getFilesTaskFileData.Path, getFilesTaskFileData.FileName);
            }
        }

        return memoryStream.ToArray();
    }
}