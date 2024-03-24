namespace Common;

public sealed class UpdateFilesCommand
{
    public UpdateFileType UpdateFileType { get; set; }
    
    public string[] FilesNames { get; set; }
}