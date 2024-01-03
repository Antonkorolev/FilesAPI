using BackendService.BusinessLogic.Helpers.Models;

namespace BackendService.BusinessLogic.Helpers;

public static class PathBuilder
{
    public static string Build(string fileCode, string fileName)
    {
        return Path.Combine("repo", fileCode[0].ToString(), fileCode[1].ToString(), fileName);
    }

    public static IEnumerable<string> Build(PathBuilderRequest request)
    {
        return request.FileInfos.Select(r => Path.Combine("repo", r.FileCode[0].ToString(), r.FileCode[1].ToString(), r.FileName));
    }
}