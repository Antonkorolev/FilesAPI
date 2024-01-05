using BackendService.BusinessLogic.Helpers.Models.Request;
using BackendService.BusinessLogic.Helpers.Models.Response;

namespace BackendService.BusinessLogic.Helpers;

public static class PathBuilder
{
    public static string Build(string fileCode, string fileName)
    {
        var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "repo", fileCode[0].ToString(), fileCode[1].ToString());

        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);

        return Path.Combine(dirPath, fileName);
    }

    public static PathBuilderResponse Build(PathBuilderRequest request)
    {
        var fileData =  request.FileInfos.Select(r => new PathBuilderFileData(r.FileName, Path.Combine("repo", r.FileCode[0].ToString(), r.FileCode[1].ToString(), r.FileName)));

        return new PathBuilderResponse(fileData);
    }
}