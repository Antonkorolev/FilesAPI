namespace BackendService.BusinessLogic.Abstractions;

public interface IZipArchiveWriter
{
    byte[] WriteFilesToZip(MemoryStream stream, params (string path, string name)[] fileData);
}