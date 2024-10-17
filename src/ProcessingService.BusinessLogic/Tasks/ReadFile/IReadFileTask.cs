namespace ProcessingService.BusinessLogic.Tasks.ReadFile;

public interface IReadFileTask
{
    Task<Stream> ReadFileAsync(string path);
}