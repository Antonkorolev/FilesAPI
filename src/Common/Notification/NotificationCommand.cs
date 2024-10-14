namespace Common.Notification;

public sealed class NotificationCommand : ICommand
{
    public UpdateFileType UpdateFileType { get; set; }
    
    public IEnumerable<string> FilesNames { get; set; } = default!;
}