namespace ProcessingService.BusinessLogic.Tasks.PathBuilder;

public sealed class PathBuilderTask : IPathBuilderTask
{
    private readonly string _rootFolder;

    public PathBuilderTask(string rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public Task<string> BuildAsync(string folderName, string fileCode, string fileName)
    {
        if (fileCode.Length < 2) throw new Exception($"FileCode should have at least 2 symbols, actual symbols: {fileCode.Length}");
        var path = Path.Combine(_rootFolder, folderName, fileCode[0].ToString(), fileCode[1].ToString(), fileName);

        return Task.FromResult(path);
    }
}