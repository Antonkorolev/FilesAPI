using BackendService.BusinessLogic.Operations.UploadFile.Tasks.GenerateFileCode;
using BackendService.BusinessLogic.Operations.UploadFiles.Models;
using BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand;
using BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand.Models;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.EnsurePathExists;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;
using BackendService.BusinessLogic.Tasks.WriteFile;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicUploadFilesOperation = BackendService.BusinessLogic.Operations.UploadFiles.UploadFilesOperation;

namespace BackendService.BusinessLogic.UnitTests.Operations.UploadFilesOperation;

[TestClass]
public sealed class UploadFilesOperationTests : UnitTestsBase
{
    private Mock<IAuthorizationTask> _authorizationTask = default!;
    private Mock<IWriteFileTask> _writeFileTask = default!;
    private Mock<IGenerateFileCodeTask> _generateFileCodeTask = default!;
    private Mock<IEnsurePathExistsTask> _ensurePathExistsTask = default!;
    private Mock<ISendUploadFilesCommandTask> _sendUploadFilesCommandTask = default!;
    private Mock<ISendNotificationCommandTask> _sendNotificationCommandTask = default!;
    private Mock<IPathBuilderTask> _pathBuilderTask = default!;
    private Mock<ILogger<BusinessLogicUploadFilesOperation>> _logger = default!;
    private BusinessLogicUploadFilesOperation _uploadFilesOperation = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _authorizationTask = new Mock<IAuthorizationTask>();
        _writeFileTask = new Mock<IWriteFileTask>();
        _generateFileCodeTask = new Mock<IGenerateFileCodeTask>();
        _ensurePathExistsTask = new Mock<IEnsurePathExistsTask>();
        _sendUploadFilesCommandTask = new Mock<ISendUploadFilesCommandTask>();
        _sendNotificationCommandTask = new Mock<ISendNotificationCommandTask>();
        _pathBuilderTask = new Mock<IPathBuilderTask>();
        _logger = new Mock<ILogger<BusinessLogicUploadFilesOperation>>();

        _uploadFilesOperation = new BusinessLogicUploadFilesOperation(
            _authorizationTask.Object,
            _writeFileTask.Object,
            _generateFileCodeTask.Object,
            _ensurePathExistsTask.Object,
            _sendUploadFilesCommandTask.Object,
            _sendNotificationCommandTask.Object,
            _logger.Object,
            _pathBuilderTask.Object);
    }

    [TestMethod]
    public async Task UploadFileOperation_ExecuteSuccessfully()
    {
        var uploadFilesOperationRequest = new UploadFilesOperationRequest(
            new[] { new UploadFileData(It.IsAny<Stream>(), DefaultFileName) },
            DefaultFileCode);

        _generateFileCodeTask.Setup(g => g.GenerateAsync(It.IsAny<Stream>())).ReturnsAsync(DefaultFileCode);
        _ensurePathExistsTask.Setup(e => e.EnsureExisting(It.IsAny<string>()));
        _writeFileTask.Setup(w => w.WriteAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        _sendUploadFilesCommandTask.Setup(s => s.SendAsync(new SendUploadFilesCommandTaskRequest(new List<SendUploadFilesData>())));
        _sendNotificationCommandTask.Setup(s => s.SendAsync(new SendNotificationCommandTaskRequest(It.IsAny<UpdateFileType>(), It.IsAny<IEnumerable<string>>())));

        await _uploadFilesOperation.UploadAsync(uploadFilesOperationRequest).ConfigureAwait(false);

        _authorizationTask.Verify(a => a.UserAuthorizationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _generateFileCodeTask.Verify(g => g.GenerateAsync(It.IsAny<Stream>()), Times.Once);
        _ensurePathExistsTask.Verify(e => e.EnsureExisting(It.IsAny<string>()), Times.Once);
        _writeFileTask.Verify(w => w.WriteAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _sendUploadFilesCommandTask.Verify( s => s.SendAsync(It.IsAny<SendUploadFilesCommandTaskRequest>()), Times.Once);
        _sendNotificationCommandTask.Verify( s => s.SendAsync(It.IsAny<SendNotificationCommandTaskRequest>()), Times.Once);
        
    }
}