using BackendService.BusinessLogic.Exceptions;
using BackendService.BusinessLogic.Tasks.PathsPreparation.Models.Request;
using BackendService.BusinessLogic.Tasks.PathsPreparation.Models.Response;

namespace BackendService.BusinessLogic.Tasks.PathsPreparation;

public sealed class PathsPreparationTask : IPathsPreparationTask
{
    public PathsPreparationTaskResponse PreparePaths(PathsPreparationTaskRequest request)
    {
        var nonConsistenceValue = request.FileInfos.FirstOrDefault(t => t.FileCode.Length < 2);

        if (nonConsistenceValue != null)
            throw new FileCodeLengthException(nonConsistenceValue.FileCode.Length);

        var fileData = request.FileInfos
            .Select(r => new PathsPreparationTaskFileData(r.FileName, Path.Combine("repo", r.FileCode[0].ToString(), r.FileCode[1].ToString(), r.FileName)));

        return new PathsPreparationTaskResponse(fileData);
    }
}