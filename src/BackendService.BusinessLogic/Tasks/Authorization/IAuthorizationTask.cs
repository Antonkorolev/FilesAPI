namespace BackendService.BusinessLogic.Tasks.Authorization;

public interface IAuthorizationTask
{
    Task<bool> UserAuthorizationAsync(string userCode, string permission);
}