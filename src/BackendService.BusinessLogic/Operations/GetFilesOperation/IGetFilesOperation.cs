using BackendService.BusinessLogic.Operations.GetFilesOperation.Models;

namespace BackendService.BusinessLogic.Operations.GetFilesOperation;

public interface IGetFilesOperation
{
    Task<IEnumerable<Stream>> GetFiles(GetFilesOperationRequest request);
}