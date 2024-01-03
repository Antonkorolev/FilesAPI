using BackendService.BusinessLogic.Exceptions.Abstraction;

namespace BackendService.BusinessLogic.Exceptions;

public sealed class FileInfosCountException : FileException
{
    public FileInfosCountException(int expected, int actual) : base($"FileInfo count {actual} not equal {expected}")
    {
    }
}