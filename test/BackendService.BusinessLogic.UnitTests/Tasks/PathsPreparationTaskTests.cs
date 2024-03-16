using BackendService.BusinessLogic.Exceptions;
using BackendService.BusinessLogic.Tasks.PathsPreparation;
using BackendService.BusinessLogic.Tasks.PathsPreparation.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests.Tasks;

[TestClass]
public sealed class PathsPreparationTaskTests : UnitTestsBase
{
    private IPathsPreparationTask _pathsPreparationTask = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _pathsPreparationTask = new PathsPreparationTask();
    }

    [TestMethod]
    public void PathsPreparationTask_ReturnPathsPreparationTaskResponse()
    {
        var pathsPreparationTaskResponses = _pathsPreparationTask.PreparePaths(new PathsPreparationTaskRequest(
            new List<PathsPreparationTaskFileInfo>
            {
                new(DefaultFileCode, DefaultFileName),
                new(NewFileCode, NewFileName)
            }));

        Assert.AreEqual(2, pathsPreparationTaskResponses.FileData.Count());

        var firstPathsPreparationTaskResponse = pathsPreparationTaskResponses.FileData.First(f => f.FileName == DefaultFileName);

        Assert.AreEqual(Path1, firstPathsPreparationTaskResponse.Path);

        var secondPathsPreparationTaskResponse = pathsPreparationTaskResponses.FileData.First(f => f.FileName == NewFileName);

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
                new(ShortFileCode, DefaultFileName)
            });

        var exception = Assert.ThrowsException<FileCodeLengthException>(() => _pathsPreparationTask.PreparePaths(pathsPreparationTaskRequest));

        Assert.AreEqual($"FileCode length should 2 or more. Current value: {ShortFileCode.Length}", exception.Message);
    }
}