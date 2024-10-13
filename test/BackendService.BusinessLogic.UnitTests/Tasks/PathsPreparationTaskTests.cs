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
    public async Task PathsPreparationTask_ReturnPathsPreparationTaskResponse()
    {
        var pathsPreparationTaskResponses = await _pathsPreparationTask.PreparePathsAsync(new PathsPreparationTaskRequest(
                new List<PathsPreparationTaskFileInfo>
                {
                    new(DefaultFileCode, DefaultFileName),
                    new(NewFileCode, NewFileName)
                }))
            .ConfigureAwait(false);

        Assert.AreEqual(2, pathsPreparationTaskResponses.FileData.Count());

        var firstPathsPreparationTaskResponse = pathsPreparationTaskResponses.FileData.First(f => f.FileName == DefaultFileName);

        Assert.AreEqual(Path1, firstPathsPreparationTaskResponse.Path);

        var secondPathsPreparationTaskResponse = pathsPreparationTaskResponses.FileData.First(f => f.FileName == NewFileName);

        Assert.AreEqual(Path2, secondPathsPreparationTaskResponse.Path);
    }

    [TestMethod]
    public async Task PathsPreparationTask_WithEmptyPathsPreparationTaskFileInfo_ReturnZeroFileData()
    {
        var pathsPreparationTaskResponses = await _pathsPreparationTask.PreparePathsAsync(new PathsPreparationTaskRequest(new List<PathsPreparationTaskFileInfo>()));

        Assert.AreEqual(0, pathsPreparationTaskResponses.FileData.Count());
    }

    [TestMethod]
    public async Task PathsPreparationTask_WithFileCodeWithOnlyOneChar_ReturnException()
    {
        var pathsPreparationTaskRequest = new PathsPreparationTaskRequest(
            new List<PathsPreparationTaskFileInfo>
            {
                new(ShortFileCode, DefaultFileName)
            });

        var exception = await Assert.ThrowsExceptionAsync<FileCodeLengthException>(() => _pathsPreparationTask.PreparePathsAsync(pathsPreparationTaskRequest));

        Assert.AreEqual($"FileCode length should 2 or more. Current value: {ShortFileCode.Length}", exception.Message);
    }
}