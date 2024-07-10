using BackendService.Contracts.DeleteFile;
using BackendService.Contracts.GetFile;
using BackendService.Contracts.GetFiles;
using BackendService.Contracts.UpdateFile;
using BackendService.Contracts.UploadFile;
using BackendService.Contracts.UploadFIles;

namespace BackendService.Contracts;

public interface IFilesApiService
{
    Task<UploadFileResponse> UploadFileAsync(UploadFileRequest request);

    Task<UploadFilesResponse> UploadFilesAsync(UploadFilesRequest request);

    Task UpdateFileAsync(UpdateFileRequest request);

    Task DeleteFileAsync(DeleteFileRequest request);

    Task<FileResponse> GetFileAsync(GetFileRequest request);

    Task<FileResponse> GetFilesAsync(GetFilesRequest request);
}