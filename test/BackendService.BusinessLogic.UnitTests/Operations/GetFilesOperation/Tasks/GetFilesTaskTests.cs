using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests.Operations.GetFilesOperation.Tasks;

[TestClass]
public sealed class GetFilesTaskTests : UnitTestsBase
{
    private IGetFilesTask _getFilesTask = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _getFilesTask = new GetFilesTask();
    }

    [Ignore("Should be refactor")]
    [TestMethod]
    public void GetFilesTask_ExecuteSuccessfully()
    {
        // TODO: should refactor task and create unit tests for it
    }
}