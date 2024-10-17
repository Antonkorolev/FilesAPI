namespace ProcessingService.BusinessLogic.Tasks.EnsurePathExists;

public sealed class EnsurePathExistsTask : IEnsurePathExistsTask
{
    public Task EnsureExistingAsync(string path)
    {
        try
        {
            var directory = Path.GetDirectoryName(path);

            if (string.IsNullOrEmpty(directory))
                throw new DirectoryNotFoundException($"Can't get directory from path = '{path}'");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            return Task.FromException(e);
        }
    }
}