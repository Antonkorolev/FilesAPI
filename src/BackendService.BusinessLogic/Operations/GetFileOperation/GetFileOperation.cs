using BackendService.BusinessLogic.Operations.GetFileOperation.Models;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using DatabaseContext.FileDb;

namespace BackendService.BusinessLogic.Operations.GetFileOperation;

public sealed class GetFileOperation : IGetFileOperation
{
    private readonly IFileDbContext _fileDbContext;
    private readonly IAuthorizationTask _authorizationTask;

    public GetFileOperation(IFileDbContext fileDbContext, IAuthorizationTask authorizationTask)
    {
        _fileDbContext = fileDbContext;
        _authorizationTask = authorizationTask;
    }

    public Task<Stream> GetFile(GetFileOperationRequest operationRequest)
    {
        throw new NotImplementedException();
    }
}