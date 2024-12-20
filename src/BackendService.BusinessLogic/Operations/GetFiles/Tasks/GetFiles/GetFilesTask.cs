using System.IO.Compression;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFilesTask.Models;

namespace BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFiles;

public sealed class GetFilesTask : IGetFilesTask
{
    public Task<byte[]> GetAsync(GetFilesTaskRequest request)
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

        return Task.FromResult(memoryStream.ToArray());
    }
}