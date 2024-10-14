using BackendService.BusinessLogic.Operations.DeleteFile.Tasks.DeleteFileInfo;
using BackendService.BusinessLogic.Operations.DeleteFiles.Models;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.DeleteFile;
using BackendService.BusinessLogic.Tasks.GetFileInfos;
using BackendService.BusinessLogic.Tasks.GetFileInfos.Models;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicDeleteFilesOperation = BackendService.BusinessLogic.Operations.DeleteFiles.DeleteFilesOperation;
using FileInfo = BackendService.BusinessLogic.Tasks.GetFileInfos.Models.FileInfo;

namespace BackendService.BusinessLogic.UnitTests.Operations.DeleteFiles;

[TestClass]
public sealed class DeleteFilesOperationTests : UnitTestsBase
{
    private Mock<IAuthorizationTask> _authorizationTask = default!;
    private Mock<IGetFileInfosTask> _getFileInfosTask = default!;
    private Mock<IPathBuilderTask> _pathBuilderTask = default!;
    private Mock<IDeleteFileTask> _deleteFileTask = default!;
    private Mock<IDeleteFileInfoTask> _deleteFileInfoTask = default!;
    private Mock<ISendNotificationCommandTask> _sendUpdateFilesCommandTask = default!;
    private Mock<ILogger<BusinessLogicDeleteFilesOperation>> _logger = default!;
    private BusinessLogicDeleteFilesOperation _deleteFilesOperation = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _authorizationTask = new Mock<IAuthorizationTask>();
        _getFileInfosTask = new Mock<IGetFileInfosTask>();
        _pathBuilderTask = new Mock<IPathBuilderTask>();
        _deleteFileTask = new Mock<IDeleteFileTask>();
        _deleteFileInfoTask = new Mock<IDeleteFileInfoTask>();
        _sendUpdateFilesCommandTask = new Mock<ISendNotificationCommandTask>();
        _logger = new Mock<ILogger<BusinessLogicDeleteFilesOperation>>();

        _deleteFilesOperation = new BusinessLogicDeleteFilesOperation(
            _authorizationTask.Object,
            _getFileInfosTask.Object,
            _pathBuilderTask.Object,
            _deleteFileTask.Object,
            _deleteFileInfoTask.Object,
            _sendUpdateFilesCommandTask.Object,
            _logger.Object);
    }

    [TestMethod]
    public async Task DeleteFilesOperation_ExecuteSuccessfully()
    {
        _getFileInfosTask
            .Setup(d => d.GetAsync(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(() => new GetFileInfosTaskResponse(new[]
            {
                new FileInfo(DefaultFileInfoId, DefaultFileCode, DefaultFileName),
                new FileInfo(NewFileInfoId, NewFileCode, NewFileName)
            }));

        _pathBuilderTask
            .Setup(p => p.BuildAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync("DefaultPath");

        _deleteFileTask.Setup(d => d.DeleteAsync(It.IsAny<string>()));
        _deleteFileInfoTask.Setup(d => d.DeleteFileAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));

        await _deleteFilesOperation.DeleteAsync(new DeleteFilesOperationRequest(FileCodes, DefaultUserCode)).ConfigureAwait(false);

        _authorizationTask.Verify(a => a.UserAuthorizationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _getFileInfosTask.Verify(d => d.GetAsync(It.IsAny<IEnumerable<string>>()), Times.Once);
        _deleteFileTask.Verify(d => d.DeleteAsync(It.IsAny<string>()), Times.AtMost(2));
        _deleteFileInfoTask.Verify(d => d.DeleteFileAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()),Times.AtMost(2));
    }
}