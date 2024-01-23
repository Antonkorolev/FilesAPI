using BackendService.BusinessLogic.Abstractions;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations.GetFilesOperation.Tasks;

[TestClass]
public sealed class GetFilesTaskTests : UnitTestsBase
{
    private Mock<IZipArchiveWriter> _zipArchiveWriter = default!;
    private IGetFilesTask _getFilesTask = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _zipArchiveWriter = new Mock<IZipArchiveWriter>();
        _getFilesTask = new GetFilesTask(_zipArchiveWriter.Object);
    }

    [TestMethod]
    public void GetFilesTask_ExecuteSuccessfully()
    {
        _zipArchiveWriter
            .Setup(z => z.WriteFilesToZip(It.IsAny<(string path, string name)[]>()))
            .Returns(Array.Empty<byte>());

        var bytes = _getFilesTask.Get(new GetFilesTaskRequest(new[] { new GetFilesTaskFileData(DefaultFileName, Path1), new GetFilesTaskFileData(NewFileName, Path2) }));

        _zipArchiveWriter.Verify(z => z.WriteFilesToZip(It.IsAny<(string path, string name)[]>()), Times.Once);
    }
}