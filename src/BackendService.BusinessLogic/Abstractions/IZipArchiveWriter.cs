namespace BackendService.BusinessLogic.Abstractions;

public interface IZipArchiveWriter
{
    byte[] WriteFilesToZip(params (string path, string name)[] filesData);
}