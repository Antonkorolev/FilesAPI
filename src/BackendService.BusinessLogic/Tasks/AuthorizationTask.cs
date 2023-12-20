namespace BackendService.BusinessLogic.Tasks;

public sealed class AuthorizationTask : IAuthorizationTask
{
    public Task<bool> UserAuthorizationAsync(string userCode, string permission)
    {
        throw new NotImplementedException();
    }
}