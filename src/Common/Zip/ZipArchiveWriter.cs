using System.IO.Compression;
using BackendService.BusinessLogic.Abstractions;

namespace Common.Zip;

public sealed class ZipArchiveWriter : IZipArchiveWriter
{
    public byte[] WriteFilesToZip(params (string path, string name)[] filesData)
    {
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