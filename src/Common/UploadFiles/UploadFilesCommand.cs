namespace Common;

public sealed class UploadFilesCommand : ICommand
{
    public UploadFilesCommand(IEnumerable<UploadFiles.UploadFiles> uploadFiles)
    {
        UploadFiles = uploadFiles;
    }

    public IEnumerable<UploadFiles.UploadFiles> UploadFiles { get; set; }
}