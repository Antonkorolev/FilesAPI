using BackendService.BusinessLogic.Operations.GetFile.Models;
using BackendService.BusinessLogic.Operations.GetFile.Tasks.GetFileTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicGetFileOperation = BackendService.BusinessLogic.Operations.GetFile.GetFileOperation;

namespace BackendService.BusinessLogic.UnitTests.Operations.GetFileOperation;

[TestClass]
public sealed class GetFileOperationTests : UnitTestsBase
{
    private Mock<IAuthorizationTask> _authorizationTask = default!;
    private Mock<IGetFileInfoTask> _getFileInfoTask = default!;
    private Mock<IGetFileTask> _getFileTask = default!;
    private Mock<ILogger<BusinessLogicGetFileOperation>> _logger = default!;
    private BusinessLogicGetFileOperation _getFileOperation = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _authorizationTask = new Mock<IAuthorizationTask>();
        _getFileInfoTask = new Mock<IGetFileInfoTask>();
        _getFileTask = new Mock<IGetFileTask>();
        _logger = new Mock<ILogger<BusinessLogicGetFileOperation>>();

        _getFileOperation = new BusinessLogicGetFileOperation(
            _authorizationTask.Object,
            _getFileInfoTask.Object,
            _getFileTask.Object,
            _logger.Object);
    }

    [TestMethod]
    public async Task GetFileOperation_ExecuteSuccessfully()
    {
        _getFileInfoTask.Setup(g => g.GetAsync(It.IsAny<string>())).ReturnsAsync(() => new GetFileInfoTaskResponse(DefaultFileInfoId, DefaultFileCode, DefaultFileName));
        _getFileTask.Setup(g => g.GetAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<Stream>);

        await _getFileOperation.GetFileAsync(new GetFileOperationRequest(DefaultFileCode, DefaultUserCode)).ConfigureAwait(false);
        
        _authorizationTask.Verify(a => a.UserAuthorizationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _getFileInfoTask.Verify(a => a.GetAsync(It.IsAny<string>()), Times.Once);
        _getFileTask.Verify(a => a.GetAsync(It.IsAny<string>()), Times.Once);
    }
}