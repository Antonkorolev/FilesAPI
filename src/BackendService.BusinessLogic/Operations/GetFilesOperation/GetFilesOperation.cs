using BackendService.BusinessLogic.Operations.GetFilesOperation.Models;

namespace BackendService.BusinessLogic.Operations.GetFilesOperation;

public class GetFilesOperation : IGetFilesOperation
{
    public Task<IEnumerable<Stream>> GetFiles(GetFilesOperationRequest request)
    {
        throw new NotImplementedException();
    }
}