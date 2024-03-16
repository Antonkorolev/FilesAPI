namespace BackendService.BusinessLogic.Operations.UploadFile.Tasks.EnsurePathExists;

public sealed class EnsurePathExistsTask : IEnsurePathExistsTask
{
    public void EnsureExisting(string path)
    {
        var directory = Path.GetDirectoryName(path);

        if (string.IsNullOrEmpty(directory))
            throw new DirectoryNotFoundException($"Can't get directory from path = '{path}'");

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }
}