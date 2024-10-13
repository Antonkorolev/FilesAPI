using BackendService.BusinessLogic.Operations.UploadFile.Models;
using BackendService.BusinessLogic.Operations.UploadFile.Tasks.SaveFileInfo;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.EnsurePathExists;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using BackendService.BusinessLogic.Tasks.WriteFile;
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
    private Mock<ISendNotificationCommandTask> _sendUpdateFilesCommandTask = default!;
    private Mock<IPathBuilderTask> _pathBuilderTask = default!;
    private Mock<ILogger<BusinessLogicUploadFileOperation>> _logger = default!;
    private BusinessLogicUploadFileOperation _uploadFileOperation = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _authorizationTask = new Mock<IAuthorizationTask>();
        _writeFileTask = new Mock<IWriteFileTask>();
        _saveFileInfoTask = new Mock<ISaveFileInfoTask>();
        _ensurePathExistsTask = new Mock<IEnsurePathExistsTask>();
        _sendUpdateFilesCommandTask = new Mock<ISendNotificationCommandTask>();
        _pathBuilderTask = new Mock<IPathBuilderTask>();
        _logger = new Mock<ILogger<BusinessLogicUploadFileOperation>>();

        _uploadFileOperation = new BusinessLogicUploadFileOperation(
            _authorizationTask.Object,
            _writeFileTask.Object,
            _saveFileInfoTask.Object,
            _ensurePathExistsTask.Object,
            _sendUpdateFilesCommandTask.Object,
            _pathBuilderTask.Object,
            _logger.Object);
    }

    [TestMethod]
    public async Task UploadFileOperation_ExecuteSuccessfully()
    {
        _ensurePathExistsTask.Setup(e => e.EnsureExistingAsync((It.IsAny<string>())));
        _writeFileTask.Setup(w => w.WriteAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        _saveFileInfoTask.Setup(s => s.SaveAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        var stream = await GetStreamAsync(DefaultFileContent).ConfigureAwait(false);

        await _uploadFileOperation.UploadAsync(new UploadFileOperationRequest(stream, DefaultFileName, DefaultUserCode));

        _authorizationTask.Verify(a => a.UserAuthorizationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _ensurePathExistsTask.Verify(e => e.EnsureExistingAsync(It.IsAny<string>()), Times.Once);
        _writeFileTask.Verify(w => w.WriteAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _saveFileInfoTask.Verify(s => s.SaveAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}