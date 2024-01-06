using BackendService.BusinessLogic.Helpers.Models.Request;
using BackendService.BusinessLogic.Helpers.Models.Response;

namespace BackendService.BusinessLogic.Helpers;

public static class PathBuilder
{
    public static string Build(string fileCode, string fileName)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "repo", fileCode[0].ToString(), fileCode[1].ToString(), fileName);
    }

    public static PathBuilderResponse Build(PathBuilderRequest request)
    {
        var fileData = request.FileInfos.Select(r => new PathBuilderFileData(r.FileName, Path.Combine("repo", r.FileCode[0].ToString(), r.FileCode[1].ToString(), r.FileName)));

        return new PathBuilderResponse(fileData);
    }
}