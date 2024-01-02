namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.WriteFileTask;

public sealed class UpdateFileTask : IUpdateFileTask
{
    public async Task UpdateAsync(Stream stream, string path)
    {
        await using var streamToWrite = File.Open(path, FileMode.Create);

        stream.Position = 0;
        await stream.CopyToAsync(streamToWrite).ConfigureAwait(false);
        stream.Close();
    }
}