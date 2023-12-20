using BackendService.BusinessLogic.Operations.UploadFileOperation.Models;
using BackendService.BusinessLogic.Tasks;
using DatabaseContext.FileDb;

namespace BackendService.BusinessLogic.Operations.UploadFileOperation;

public sealed class UploadFileOperation : IUploadFileOperation
{
    private readonly IFileDbContext _fileDbContext;
    private readonly IAuthorizationTask _authorizationTask;

    public UploadFileOperation(IFileDbContext fileDbContext, IAuthorizationTask authorizationTask)
    {
        _fileDbContext = fileDbContext;
        _authorizationTask = authorizationTask;
    }

    public Task<string> UploadFileAsync(UploadFileOperationRequest request)
    {
        throw new NotImplementedException();
    }
}