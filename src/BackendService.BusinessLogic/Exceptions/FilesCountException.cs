using BackendService.BusinessLogic.Exceptions.Abstraction;

namespace BackendService.BusinessLogic.Exceptions;

public sealed class FilesCountException : FileException
{
    public FilesCountException(int expected, int actual) : base($"Files count {actual} not equal {expected}")
    {
    }
}