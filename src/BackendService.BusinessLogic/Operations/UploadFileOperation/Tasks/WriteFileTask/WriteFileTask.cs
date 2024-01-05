using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.WriteFileTask;

public sealed class WriteFileTask : IWriteFileTask
{
    private readonly ILogger<WriteFileTask> _logger;

    public WriteFileTask(ILogger<WriteFileTask> logger)
    {
        _logger = logger;
    }


    public async Task WriteAsync(Stream stream, string path, CancellationToken cancellationToken)
    {
        try
        {
            await using var streamToWrite = File.Create(path);

            await stream.CopyToAsync(streamToWrite, cancellationToken).ConfigureAwait(false);
            await stream.DisposeAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error occured: {e}");
        }
    }
}