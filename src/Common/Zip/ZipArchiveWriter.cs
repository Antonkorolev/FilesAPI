using System.IO.Compression;
using BackendService.BusinessLogic.Abstractions;

namespace Common.Zip;

public sealed class ZipArchiveWriter : IZipArchiveWriter
{
    public byte[] WriteFilesToZip(MemoryStream stream, params (string path, string name)[] fileData)
    {
        using (var archive = new ZipArchive(stream, ZipArchiveMode.Create))
        {
            foreach (var (path, name) in fileData)
            {
                archive.CreateEntryFromFile(path, name);
            }
        }

        return stream.ToArray();
    }
}