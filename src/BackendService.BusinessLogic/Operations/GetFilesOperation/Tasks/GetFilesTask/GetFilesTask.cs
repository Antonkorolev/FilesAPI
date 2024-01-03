using BackendService.BusinessLogic.Exceptions;

namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;

public sealed class GetFilesTask : IGetFilesTask
{
    public IEnumerable<Stream> Get(IEnumerable<string> paths)
    {
        var pathsArray = paths as string[] ?? paths.ToArray();
        var streams = pathsArray.Select(File.OpenRead).ToArray();

        if (streams.Length == pathsArray.Length)
            throw new FilesCountException(pathsArray.Length, streams.Length);

        return streams;
    }
}