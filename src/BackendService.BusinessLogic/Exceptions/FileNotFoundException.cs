using BackendService.BusinessLogic.Exceptions.Abstraction;

namespace BackendService.BusinessLogic.Exceptions;

public sealed class FileNotFoundException : FileException
{
    public FileNotFoundException() : base("File not found")
    {
    }
}