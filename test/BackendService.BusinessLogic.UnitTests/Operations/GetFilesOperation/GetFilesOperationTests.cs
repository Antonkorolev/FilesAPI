using BackendService.BusinessLogic.Operations.GetFiles.Models;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfosTask;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfosTask.Models;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFilesTask;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFilesTask.Models;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using BackendService.BusinessLogic.Tasks.PathsPreparationTask;
using BackendService.BusinessLogic.Tasks.PathsPreparationTask.Models.Request;
using BackendService.BusinessLogic.Tasks.PathsPreparationTask.Models.Response;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicGetFilesOperation = BackendService.BusinessLogic.Operations.GetFiles.GetFilesOperation;
using FileInfo = BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfosTask.Models.FileInfo;

namespace BackendService.BusinessLogic.UnitTests.Operations.GetFilesOperation;

[TestClass]
public sealed class GetFilesOperationTests : UnitTestsBase
{
    private Mock<IAuthorizationTask> _authorizationTask = default!;
    private Mock<IGetFileInfosTask> _getFileInfosTask = default!;
    private Mock<IGetFilesTask> _getFilesTask = default!;
    private Mock<IPathsPreparationTask> _pathsPreparationTask = default!;
    private Mock<ILogger<BusinessLogicGetFilesOperation>> _logger = default!;
    private BusinessLogicGetFilesOperation _getFilesOperation = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _authorizationTask = new Mock<IAuthorizationTask>();
        _getFileInfosTask = new Mock<IGetFileInfosTask>();
        _getFilesTask = new Mock<IGetFilesTask>();
        _pathsPreparationTask = new Mock<IPathsPreparationTask>();
        _logger = new Mock<ILogger<BusinessLogicGetFilesOperation>>();

        _getFilesOperation = new BusinessLogicGetFilesOperation(
            _authorizationTask.Object,
            _getFileInfosTask.Object,
            _getFilesTask.Object,
            _pathsPreparationTask.Object,
            _logger.Object);
    }

    [TestMethod]
    public async Task GetFilesOperation_ExecuteSuccessfully()
    {
        _getFileInfosTask
            .Setup(g => g.GetAsync(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(() => new GetFileInfosTaskResponse(new[] { new FileInfo(DefaultFileInfoId, DefaultFileCode, DefaultFileName), new FileInfo(NewFileInfoId, NewFileCode, NewFileName) }));
        _pathsPreparationTask
            .Setup(p => p.PreparePaths(It.IsAny<PathsPreparationTaskRequest>()))
            .Returns(() => new PathsPreparationTaskResponse(new[] { new PathsPreparationTaskFileData(DefaultFileName, Path1), new PathsPreparationTaskFileData(NewFileName, Path2) }));

        await _getFilesOperation.GetFilesAsync(new GetFilesOperationRequest(FileCodes, DefaultUserCode)).ConfigureAwait(false);

        _authorizationTask.Verify(a => a.UserAuthorizationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _getFileInfosTask.Verify(g => g.GetAsync(It.IsAny<IEnumerable<string>>()), Times.Once);
        _pathsPreparationTask.Verify(p => p.PreparePaths(It.IsAny<PathsPreparationTaskRequest>()), Times.Once);
        _getFilesTask.Verify(g => g.Get(It.IsAny<GetFilesTaskRequest>()), Times.Once);
    }
}