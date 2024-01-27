using System.IO.Compression;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask.Models;

namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;

public sealed class GetFilesTask : IGetFilesTask
{
    public byte[] Get(GetFilesTaskRequest request)
    {
        var filesData = request.FileData.Select(f => (f.Path, f.FileName)).ToArray();

        using var memoryStream = new MemoryStream();

        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
        {
            foreach (var (path, name) in filesData)
            {
                archive.CreateEntryFromFile(path, name);
            }
        }

        return memoryStream.ToArray();
    }
}