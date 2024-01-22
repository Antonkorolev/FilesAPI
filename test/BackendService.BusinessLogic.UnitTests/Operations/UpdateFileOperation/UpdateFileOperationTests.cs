using BackendService.BusinessLogic.Operations.UpdateFileOperation;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Models;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileInfoTask;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using BackendService.BusinessLogic.Tasks.DeleteFileTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicUpdateFileOperation = BackendService.BusinessLogic.Operations.UpdateFileOperation.UpdateFileOperation;

namespace BackendService.BusinessLogic.UnitTests.Operations.UpdateFileOperation;

[TestClass]
public sealed class UpdateFileOperationTests : UnitTestsBase
{
    private IUpdateFileOperation _updateFileOperation = default!;
    private Mock<IUpdateFileTask> _updateFileTask = default!;
    private Mock<IUpdateFileInfoTask> _updateFileInfoTask = default!;
    private Mock<IAuthorizationTask> _authorizationTask = default!;
    private Mock<IGetFileInfoTask> _getFileInfosTask = default!;
    private Mock<IDeleteFileTask> _deleteFileTask = default!;
    private Mock<ILogger<BusinessLogicUpdateFileOperation>> _logger = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _updateFileTask = new Mock<IUpdateFileTask>();
        _updateFileInfoTask = new Mock<IUpdateFileInfoTask>();
        _authorizationTask = new Mock<IAuthorizationTask>();
        _getFileInfosTask = new Mock<IGetFileInfoTask>();
        _deleteFileTask = new Mock<IDeleteFileTask>();
        _logger = new Mock<ILogger<BusinessLogicUpdateFileOperation>>();

        _updateFileOperation = new BusinessLogicUpdateFileOperation(
            _updateFileTask.Object,
            _updateFileInfoTask.Object,
            _authorizationTask.Object,
            _getFileInfosTask.Object,
            _deleteFileTask.Object,
            _logger.Object);
    }

    [TestMethod]
    public async Task UpdateFileOperation_ExecuteSuccessfully()
    {
        _updateFileTask.Setup(e => e.UpdateAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        _updateFileInfoTask.Setup(w => w.UpdateInfoAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        _deleteFileTask.Setup(s => s.Delete(It.IsAny<string>()));
        _getFileInfosTask
            .Setup(s => s.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(() => new GetFileInfoTaskResponse(DefaultFileInfoId, DefaultFileCode, DefaultFileName));

        var stream = await GetStreamAsync(DefaultFileContent).ConfigureAwait(false);

        await _updateFileOperation.UpdateAsync(new UpdateFileOperationRequest(DefaultFileCode, stream, DefaultFileName, DefaultUserCode));

        _authorizationTask.Verify(a => a.UserAuthorizationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _updateFileTask.Verify(e => e.UpdateAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _updateFileInfoTask.Verify(w => w.UpdateInfoAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _deleteFileTask.Verify(s => s.Delete(It.IsAny<string>()), Times.Once);
        _getFileInfosTask.Verify(s => s.GetAsync(It.IsAny<string>()), Times.Once);
    }
}