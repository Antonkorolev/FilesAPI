using BackendService.BusinessLogic.Abstractions;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;
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

    [Ignore("Should be refactor")]
    [TestMethod]
    public void GetFilesTask_ExecuteSuccessfully()
    {
        // TODO: should refactor task and create unit tests for it
    }
}