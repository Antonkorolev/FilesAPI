using BackendService.BusinessLogic.Exceptions;

namespace BackendService.BusinessLogic.Helpers;

public static class PathBuilder
{
    public static string Build(string folderName, string fileCode, string fileName)
    {
        if (fileCode.Length < 2) throw new FileCodeLengthException(fileCode.Length);
        return Path.Combine(folderName, fileCode[0].ToString(), fileCode[1].ToString(), fileName);
    }
}