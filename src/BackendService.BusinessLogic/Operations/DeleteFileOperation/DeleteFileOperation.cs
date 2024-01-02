using BackendService.BusinessLogic.Operations.DeleteFileOperation.Models;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using DatabaseContext.FileDb;

namespace BackendService.BusinessLogic.Operations.DeleteFileOperation;

public sealed class DeleteFileOperation : IDeleteFileOperation
{
    private readonly IFileDbContext _fileDbContext;
    private readonly IAuthorizationTask _authorizationTask;

    public DeleteFileOperation(IFileDbContext fileDbContext, IAuthorizationTask authorizationTask)
    {
        _fileDbContext = fileDbContext;
        _authorizationTask = authorizationTask;
    }

    public Task DeleteFileAsync(DeleteFileOperationRequest request)
    {
        throw new NotImplementedException();
    }
}