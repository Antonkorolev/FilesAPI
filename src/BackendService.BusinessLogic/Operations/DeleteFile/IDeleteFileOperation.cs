using BackendService.BusinessLogic.Operations.DeleteFile.Models;

namespace BackendService.BusinessLogic.Operations.DeleteFile;

public interface IDeleteFileOperation
{
    Task DeleteAsync(DeleteFileOperationRequest request);
}