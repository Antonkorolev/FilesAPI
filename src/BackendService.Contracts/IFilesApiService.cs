using BackendService.Contracts.DeleteFile;
using BackendService.Contracts.GetFile;
using BackendService.Contracts.GetFiles;
using BackendService.Contracts.UpdateFile;
using BackendService.Contracts.UploadFile;

namespace BackendService.Contracts;

public interface IFilesApiService
{
    Task<UploadFileResponse> UploadFIleAsync(UploadFileRequest request);

    Task UpdateFileAsync(UpdateFileRequest request);

    Task DeleteFileAsync(DeleteFileRequest request);

    Task<FileResponse> GetFileAsync(GetFileRequest request);

    Task<FileResponse> GetFilesAsync(GetFilesRequest request);
}