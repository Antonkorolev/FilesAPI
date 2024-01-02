using System.Net;

namespace BackendService.BusinessLogic.Tasks.AuthorizationTask;

public interface IAuthorizationTask
{
    Task<bool> UserAuthorizationAsync(string userCode, string permission);
}