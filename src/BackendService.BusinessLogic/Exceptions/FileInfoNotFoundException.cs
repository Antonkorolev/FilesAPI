using BackendService.BusinessLogic.Exceptions.Abstraction;

namespace BackendService.BusinessLogic.Exceptions;

public sealed class FileInfoNotFoundException : FileException
{
    public FileInfoNotFoundException() : base("FileInfo not found in database")
    {
    }
}