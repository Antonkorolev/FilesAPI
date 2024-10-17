using System.Net.Http.Json;
using BackendService.Contracts;
using BackendService.Contracts.DeleteFile;
using BackendService.Contracts.DeleteFiles;
using BackendService.Contracts.GetFile;
using BackendService.Contracts.GetFiles;
using BackendService.Contracts.UpdateFile;
using BackendService.Contracts.UpdateFiles;
using BackendService.Contracts.UploadFile;
using BackendService.Contracts.UploadFIles;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BackendService.Client;

public sealed class FilesApiService : IFilesApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _httpClientName;

    public FilesApiService(IHttpClientFactory httpClientFactory, string httpClientName)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentException("HttpClientFactory not configured");
        _httpClientName = httpClientName;
    }

    public async Task<UploadFileResponse> UploadFileAsync(UploadFileRequest request)
    {
        var httpClient = CreateHttpClient();

        var multipartFormDataContent = new MultipartFormDataContent();
        multipartFormDataContent.Add(new StreamContent(request.File.OpenReadStream()), nameof(request.File));

        var response = await httpClient.PostAsync("file/upload", multipartFormDataContent).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Response StatusCode: {response.StatusCode}, Content: {response.Content}");

        var stringResponse = await response.Content.ReadAsStringAsync();

        var uploadFileResponse = JsonConvert.DeserializeObject<UploadFileResponse>(stringResponse);
        if (uploadFileResponse == null)
            throw new Exception("UploadFileResponse deserialize exception. Value is null");

        return uploadFileResponse;
    }

    public async Task<UploadFilesResponse> UploadFilesAsync(UploadFilesRequest request)
    {
        var httpClient = CreateHttpClient();

        var multipartFormDataContent = new MultipartFormDataContent();

        foreach (var file in request.Files)
        {
            multipartFormDataContent.Add(new StreamContent(file.OpenReadStream()), nameof(file));
        }

        var response = await httpClient.PostAsync("file/uploadArray", multipartFormDataContent).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Response StatusCode: {response.StatusCode}, Content: {response.Content}");

        var stringResponse = await response.Content.ReadAsStringAsync();

        var uploadFileResponse = JsonConvert.DeserializeObject<UploadFilesResponse>(stringResponse);
        if (uploadFileResponse == null)
            throw new Exception("UploadFileResponse deserialize exception. Value is null");

        return uploadFileResponse;
    }

    public async Task UpdateFileAsync(UpdateFileRequest request)
    {
        var httpClient = CreateHttpClient();

        var multipartFormDataContent = new MultipartFormDataContent();
        multipartFormDataContent.Add(new StreamContent(request.File.OpenReadStream()), nameof(request.File));
        multipartFormDataContent.Add(new StringContent(request.FileCode), nameof(request.FileCode));

        var response = await httpClient.PostAsync("file/update", multipartFormDataContent).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Response StatusCode: {response.StatusCode}, Content: {response.Content}");
    }

    public async Task UpdateFilesAsync(IFormFileCollection fileCollection, UpdateFilesRequest request)
    {
        var httpClient = CreateHttpClient();

        var multipartFormDataContent = new MultipartFormDataContent();

        foreach (var (file, fileCode) in fileCollection.Zip(request.FileCodes))
        {
            multipartFormDataContent.Add(new StreamContent(file.OpenReadStream()), nameof(file));
            multipartFormDataContent.Add(new StringContent(fileCode), nameof(fileCode));
            multipartFormDataContent.Add(new StringContent(file.FileName), nameof(file.FileName));
        }

        var response = await httpClient.PostAsync("file/updateArray", multipartFormDataContent).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Response StatusCode: {response.StatusCode}, Content: {response.Content}");
    }

    public async Task DeleteFileAsync(DeleteFileRequest request)
    {
        var httpClient = CreateHttpClient();

        var response = await httpClient.PostAsync("file/delete", JsonContent.Create(request)).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Response StatusCode: {response.StatusCode}, Content: {response.Content}");
    }

    public async Task DeleteFilesAsync(DeleteFilesRequest request)
    {
        var httpClient = CreateHttpClient();

        var response = await httpClient.PostAsync("file/deleteArray", JsonContent.Create(request)).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Response StatusCode: {response.StatusCode}, Content: {response.Content}");
    }

    public async Task<FileResponse> GetFileAsync(GetFileRequest request)
    {
        var httpClient = CreateHttpClient();

        var response = await httpClient.GetAsync($"file/get?FileCode={request.FileCode}").ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Response StatusCode: {response.StatusCode}, Content: {response.Content}");

        return await GetFileResponse(response).ConfigureAwait(false);
    }

    public async Task<FileResponse> GetFilesAsync(GetFilesRequest request)
    {
        var httpClient = CreateHttpClient();

        var response = await httpClient.PostAsync($"file/getArray", JsonContent.Create(request)).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Response StatusCode: {response.StatusCode}, Content: {response.Content}");

        return await GetFileResponse(response).ConfigureAwait(false);
    }

    private HttpClient CreateHttpClient()
    {
        return _httpClientFactory.CreateClient(_httpClientName);
    }

    private static async Task<FileResponse> GetFileResponse(HttpResponseMessage response)
    {
        var stream = await response.Content.ReadAsStreamAsync();

        var name = response.Content.Headers.ContentDisposition?.FileName ?? throw new Exception("FileName is null in response");
        var contentType = response.Content.Headers.ContentType?.MediaType ?? throw new Exception("MediaType is null in response");

        return new FileResponse(name, stream, contentType);
    }
}