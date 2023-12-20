using System.Net;

namespace BackendService.BusinessLogic.Tasks;

public interface IAuthorizationTask
{
    Task<bool> UserAuthorizationAsync(string userCode, string permission);
}