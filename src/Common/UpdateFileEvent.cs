using NServiceBus;

namespace Common;

public sealed class UpdateFileEvent : IEvent
{
    public int UpdateFileType { get; set; }
    
    public string[] FilesNames { get; set; }
}