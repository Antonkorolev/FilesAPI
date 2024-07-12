namespace Common;

public sealed class UpdateFilesCommand : ICommand
{
    public UpdateFileType UpdateFileType { get; set; }
    
    public IEnumerable<string> FilesNames { get; set; } = default!;
}