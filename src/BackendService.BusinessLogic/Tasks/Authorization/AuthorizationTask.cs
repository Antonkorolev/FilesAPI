namespace BackendService.BusinessLogic.Tasks.Authorization;

public sealed class AuthorizationTask : IAuthorizationTask
{
    public async Task<bool> UserAuthorizationAsync(string userCode, string permission)
    {
        // TODO: implements after Authz service realisation
        return await Task.FromResult(true);
    }
}