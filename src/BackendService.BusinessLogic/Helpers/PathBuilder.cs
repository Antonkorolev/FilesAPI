namespace BackendService.BusinessLogic.Helpers;

public static class PathBuilder
{
    public static string Build(string fileCode)
    {
        return Path.Combine("repo", fileCode[0].ToString(), fileCode[1].ToString());
    }
}