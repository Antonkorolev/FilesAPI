using BackendService.BusinessLogic.Exceptions;
using BackendService.BusinessLogic.Tasks.PathsPreparationTask;
using BackendService.BusinessLogic.Tasks.PathsPreparationTask.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests.Tasks;

[TestClass]
public sealed class PathsPreparationTaskTests : UnitTestsBase
{
    private readonly IPathsPreparationTask _pathsPreparationTask;

    public PathsPreparationTaskTests()
    {
        _pathsPreparationTask = new PathsPreparationTask();
    }

    [TestMethod]
    public void PathsPreparationTask_ReturnPathsPreparationTaskResponse()
    {
        var pathsPreparationTaskResponses = _pathsPreparationTask.PreparePaths(new PathsPreparationTaskRequest(
            new List<PathsPreparationTaskFileInfo>
            {
                new(FileCode1, FileName1),
                new(FileCode2, FileName2)
            }));

        Assert.AreEqual(2, pathsPreparationTaskResponses.FileData.Count());

        var firstPathsPreparationTaskResponse = pathsPreparationTaskResponses.FileData.First(f => f.FileName == FileName1);

        Assert.AreEqual(Path1, firstPathsPreparationTaskResponse.Path);

        var secondPathsPreparationTaskResponse = pathsPreparationTaskResponses.FileData.First(f => f.FileName == FileName2);

        Assert.AreEqual(Path2, secondPathsPreparationTaskResponse.Path);
    }

    [TestMethod]
    public void PathsPreparationTask_WithEmptyPathsPreparationTaskFileInfo_ReturnZeroFileData()
    {
        var pathsPreparationTaskResponses = _pathsPreparationTask.PreparePaths(new PathsPreparationTaskRequest(new List<PathsPreparationTaskFileInfo>()));

        Assert.AreEqual(0, pathsPreparationTaskResponses.FileData.Count());
    }

    [TestMethod]
    public void PathsPreparationTask_WithFileCodeWithOnlyOneChar_ReturnException()
    {
        var pathsPreparationTaskRequest = new PathsPreparationTaskRequest(
            new List<PathsPreparationTaskFileInfo>
            {
                new(ShortFileCode, FileName1)
            });

        var exception = Assert.ThrowsException<FileCodeLengthException>(() => _pathsPreparationTask.PreparePaths(pathsPreparationTaskRequest));
        
        Assert.AreEqual($"FileCode length should 2 or more. Current value: {ShortFileCode.Length}", exception.Message);
    }
}