using BackendService.BusinessLogic.Operations.UploadFile.Models;
using BackendService.BusinessLogic.Operations.UploadFile.Tasks.EnsurePathExistsTask;
using BackendService.BusinessLogic.Operations.UploadFile.Tasks.GenerateFileCodeTask;
using BackendService.BusinessLogic.Operations.UploadFile.Tasks.SaveFileInfoTask;
using BackendService.BusinessLogic.Operations.UploadFile.Tasks.WriteFileTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicUploadFileOperation = BackendService.BusinessLogic.Operations.UploadFile.UploadFileOperation;

namespace BackendService.BusinessLogic.UnitTests.Operations.UploadFileOperation;

[TestClass]
public sealed class UploadFileOperationTests : UnitTestsBase
{
    private Mock<IAuthorizationTask> _authorizationTask = default!;
    private Mock<IWriteFileTask> _writeFileTask = default!;
    private Mock<ISaveFileInfoTask> _saveFileInfoTask = default!;
    private Mock<IEnsurePathExistsTask> _ensurePathExistsTask = default!;
    private Mock<IGenerateFileCodeTask> _generateFileCodeTask = default!;
    private Mock<ILogger<BusinessLogicUploadFileOperation>> _logger = default!;
    private BusinessLogicUploadFileOperation _uploadFileOperation = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _authorizationTask = new Mock<IAuthorizationTask>();
        _writeFileTask = new Mock<IWriteFileTask>();
        _saveFileInfoTask = new Mock<ISaveFileInfoTask>();
        _ensurePathExistsTask = new Mock<IEnsurePathExistsTask>();
        _generateFileCodeTask = new Mock<IGenerateFileCodeTask>();
        _logger = new Mock<ILogger<BusinessLogicUploadFileOperation>>();

        _uploadFileOperation = new BusinessLogicUploadFileOperation(
            _authorizationTask.Object,
            _writeFileTask.Object,
            _saveFileInfoTask.Object,
            _ensurePathExistsTask.Object,
            _generateFileCodeTask.Object,
            _logger.Object);
    }

    [TestMethod]
    public async Task UploadFileOperation_ExecuteSuccessfully()
    {
        _generateFileCodeTask.Setup(g => g.GenerateAsync(It.IsAny<Stream>())).ReturnsAsync(DefaultFileCode);
        _ensurePathExistsTask.Setup(e => e.EnsureExisting(It.IsAny<string>()));
        _writeFileTask.Setup(w => w.WriteAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        _saveFileInfoTask.Setup(s => s.SaveAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        var stream = await GetStreamAsync(DefaultFileContent).ConfigureAwait(false);

        await _uploadFileOperation.UploadAsync(new UploadFileOperationRequest(stream, DefaultFileName, DefaultUserCode));

        _authorizationTask.Verify(a => a.UserAuthorizationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _generateFileCodeTask.Verify(g => g.GenerateAsync(It.IsAny<Stream>()), Times.Once);
        _ensurePathExistsTask.Verify(e => e.EnsureExisting(It.IsAny<string>()), Times.Once);
        _writeFileTask.Verify(w => w.WriteAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _saveFileInfoTask.Verify(s => s.SaveAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}