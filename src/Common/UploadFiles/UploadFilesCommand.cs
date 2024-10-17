namespace Common.UploadFiles;

public sealed class UploadFilesCommand : ICommand
{
    public UploadFilesCommand(IEnumerable<UploadFiles> uploadFiles)
    {
        UploadFiles = uploadFiles;
    }

    public IEnumerable<UploadFiles> UploadFiles { get; set; }
}