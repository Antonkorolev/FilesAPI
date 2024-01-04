using System.IO.Compression;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask.Models;

namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;

public sealed class GetFilesTask : IGetFilesTask
{
    public byte[] Get(GetFilesTaskRequest request)
    {
        using var memoryStream = new MemoryStream();
        using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

        foreach (var getFilesTaskFileData in request.FileData)
        {
            var zipArchiveEntry = archive.CreateEntry(getFilesTaskFileData.FileName);
            using var entryStream = zipArchiveEntry.Open();

            using var fileToCompressStream = new MemoryStream(File.ReadAllBytes(getFilesTaskFileData.Path));
            fileToCompressStream.CopyTo(entryStream);
        }

        return memoryStream.ToArray();
    }
}