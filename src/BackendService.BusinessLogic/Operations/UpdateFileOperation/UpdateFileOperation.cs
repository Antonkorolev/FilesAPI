using BackendService.BusinessLogic.Operations.UpdateFileOperation.Models;
using BackendService.BusinessLogic.Tasks;
using DatabaseContext.FileDb;

namespace BackendService.BusinessLogic.Operations.UpdateFileOperation;

public sealed class UpdateFileOperation : IUpdateFileOperation
{
    private readonly IFileDbContext _fileDbContext;
    private readonly IAuthorizationTask _authorizationTask;

    public UpdateFileOperation(IFileDbContext fileDbContext, IAuthorizationTask authorizationTask)
    {
        _fileDbContext = fileDbContext;
        _authorizationTask = authorizationTask;
    }

    public Task<string> UpdateFileAsync(UpdateFileOperationRequest request)
    {
        throw new NotImplementedException();
    }
}