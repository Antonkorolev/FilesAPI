using BackendService.BusinessLogic.Operations.UploadFileOperation.Models;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.EnsurePathExistsTask;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.SaveFileInfoTask;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.WriteFileTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicUploadFileOperation = BackendService.BusinessLogic.Operations.UploadFileOperation.UploadFileOperation;

namespace BackendService.BusinessLogic.UnitTests.Operations.UploadFileOperation;

[TestClass]
public sealed class UploadFileOperationTests : UnitTestsBase
{
    private Mock<IAuthorizationTask> _authorizationTask;
    private Mock<IWriteFileTask> _writeFileTask;
    private Mock<ISaveFileInfoTask> _saveFileInfoTask;
    private Mock<IEnsurePathExistsTask> _ensurePathExistsTask;
    private Mock<ILogger<BusinessLogicUploadFileOperation>> _logger;
    private BusinessLogicUploadFileOperation _uploadFileOperation;

    [TestInitialize]
    public void TestInitialize()
    {
        _authorizationTask = new Mock<IAuthorizationTask>();
        _writeFileTask = new Mock<IWriteFileTask>();
        _saveFileInfoTask = new Mock<ISaveFileInfoTask>();
        _ensurePathExistsTask = new Mock<IEnsurePathExistsTask>();
        _logger = new Mock<ILogger<BusinessLogicUploadFileOperation>>();

        _uploadFileOperation = new BusinessLogicUploadFileOperation(
            _authorizationTask.Object,
            _writeFileTask.Object,
            _saveFileInfoTask.Object,
            _ensurePathExistsTask.Object,
            _logger.Object);
    }

    [TestMethod]
    public async Task UploadFileOperation_ExecuteSuccessfully()
    {
        _ensurePathExistsTask.Setup(e => e.EnsureExisting(It.IsAny<string>()));
        _writeFileTask.Setup(w => w.WriteAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        _saveFileInfoTask.Setup(s => s.SaveAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        var stream = await GetStreamAsync(FileContent).ConfigureAwait(false);

        await _uploadFileOperation.UploadAsync(new UploadFileOperationRequest(stream, DefaultFileName, UserCode));

        _ensurePathExistsTask.Verify(e => e.EnsureExisting(It.IsAny<string>()), Times.Once);
        _writeFileTask.Verify(w => w.WriteAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _saveFileInfoTask.Verify(s => s.SaveAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}