namespace BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFilesTask.Models;

public class GetFilesTaskFileData
{
    public GetFilesTaskFileData(string fileName, string path)
    {
        FileName = fileName;
        Path = path;
    }

    public string FileName { get; set; }
    
    public string Path { get; set; }
}