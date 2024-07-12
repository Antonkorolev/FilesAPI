namespace ProcessingService.BusinessLogic.Tasks.PathBuilder;

public interface IPathBuilderTask
{
    Task<string> BuildAsync(string folderName, string fileCode, string fileName);
}