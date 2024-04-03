using NServiceBus;

namespace Common;

public sealed class UpdateFilesCommand : ICommand
{
    public UpdateFileType UpdateFileType { get; set; }
    
    public string[] FilesNames { get; set; }
}