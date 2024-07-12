namespace Common;

public sealed class FilesChangeResponseMessage : IMessage
{
    public FilesChangeResponseMessage(Status status, string? errorMessage)
    {
        ErrorMessage = errorMessage;
        Status = status;
    }

    public Status Status { get;  }
    
    public string? ErrorMessage { get; }
}