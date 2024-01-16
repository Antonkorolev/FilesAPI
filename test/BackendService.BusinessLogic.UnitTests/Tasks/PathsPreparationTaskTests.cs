using BackendService.BusinessLogic.Exceptions;
using BackendService.BusinessLogic.Tasks.PathsPreparationTask;
using BackendService.BusinessLogic.Tasks.PathsPreparationTask.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests.Tasks;

[TestClass]
public sealed class PathsPreparationTaskTests
{
    private readonly IPathsPreparationTask _pathsPreparationTask;

    public PathsPreparationTaskTests()
    {
        _pathsPreparationTask = new PathsPreparationTask();
    }

    [TestMethod]
    public void PathsPreparationTask_ReturnPathsPreparationTaskResponse()
    {
        const string fileCode1 = "testCode1";
        const string fileName1 = "testName1";

        var firstPath = Path.Combine("repo", fileCode1[0].ToString(), fileCode1[1].ToString(), fileName1);

        const string fileCode2 = "testCode2";
        const string fileName2 = "testName2";

        var secondPath = Path.Combine("repo", fileCode2[0].ToString(), fileCode2[1].ToString(), fileName2);

        var pathsPreparationTaskResponses = _pathsPreparationTask.PreparePaths(new PathsPreparationTaskRequest(
            new List<PathsPreparationTaskFileInfo>
            {
                new(fileCode1, fileName1),
                new(fileCode2, fileName2)
            }));

        Assert.AreEqual(2, pathsPreparationTaskResponses.FileData.Count());

        var firstPathsPreparationTaskResponse = pathsPreparationTaskResponses.FileData.First(f => f.FileName == fileName1);

        Assert.AreEqual(firstPath, firstPathsPreparationTaskResponse.Path);

        var secondPathsPreparationTaskResponse = pathsPreparationTaskResponses.FileData.First(f => f.FileName == fileName2);

        Assert.AreEqual(secondPath, secondPathsPreparationTaskResponse.Path);
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
        const string fileCode = "t";
        
        var pathsPreparationTaskRequest = new PathsPreparationTaskRequest(
            new List<PathsPreparationTaskFileInfo>
            {
                new(fileCode, "test")
            });

        var exception = Assert.ThrowsException<FileCodeLengthException>(() => _pathsPreparationTask.PreparePaths(pathsPreparationTaskRequest));
        
        Assert.AreEqual($"FileCode length should 2 or more. Current value: {fileCode.Length}", exception.Message);
    }
}