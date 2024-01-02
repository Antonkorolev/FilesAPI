namespace BackendService.BusinessLogic.Exceptions.Abstraction;

public abstract class FileException : Exception
{
    protected FileException(string message) : base(message)
    {
        
    }   
}