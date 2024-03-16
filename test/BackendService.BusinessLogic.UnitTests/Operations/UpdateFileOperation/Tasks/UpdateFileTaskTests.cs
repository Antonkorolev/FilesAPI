using BackendService.BusinessLogic.Operations.UpdateFile.Tasks.UpdateFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests.Operations.UpdateFileOperation.Tasks;

[TestClass]
public sealed class UpdateFileTaskTests : UnitTestsBase
{
    private IUpdateFileTask _updateFileTask = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _updateFileTask = new UpdateFileTask();
    }

    [TestMethod]
    public async Task UpdateFileTask_PathIsNull_Error()
    {
        var stream = await GetStreamAsync(NewFileContent).ConfigureAwait(false);

        await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _updateFileTask.UpdateAsync(stream, null!, CancellationToken.None)).ConfigureAwait(false);
    }
}