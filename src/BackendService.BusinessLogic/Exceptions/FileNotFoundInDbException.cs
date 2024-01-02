using BackendService.BusinessLogic.Exceptions.Abstraction;

namespace BackendService.BusinessLogic.Exceptions;

public class FileNotFoundInDbException : FileException
{
    public FileNotFoundInDbException() : base("File not found in database")
    {
        
    }
}