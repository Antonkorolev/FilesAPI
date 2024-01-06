namespace BackendService.BusinessLogic.Helpers;

public static class PathBuilder
{
    public static string Build(string fileCode, string fileName)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "repo", fileCode[0].ToString(), fileCode[1].ToString(), fileName);
    }
}