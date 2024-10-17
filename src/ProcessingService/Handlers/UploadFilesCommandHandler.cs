using Common;
using Common.UploadFiles;
using ProcessingService.BusinessLogic.Operations.UploadFiles;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Models;
using UploadFiles = ProcessingService.BusinessLogic.Operations.UploadFiles.Models.UploadFiles;

namespace ProcessingService.Handlers;

public sealed class UploadFilesCommandHandler : IHandleMessages<UploadFilesCommand>
{
    private readonly IUploadFilesOperation _uploadFilesOperation;

    public UploadFilesCommandHandler(IUploadFilesOperation uploadFilesOperation)
    {
        _uploadFilesOperation = uploadFilesOperation;
    }

    public async Task Handle(UploadFilesCommand message, IMessageHandlerContext context)
    {
        try
        {
            await _uploadFilesOperation.UploadFilesAsync(new UploadFilesOperationRequest(message.UploadFiles.Select(t => new UploadFiles(t.FileName, t.FileCode))))
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            await context.Reply(new FilesChangeResponseMessage(Status.Error, e.ToString())).ConfigureAwait(false);
            return;
        }

        await context.Reply(new FilesChangeResponseMessage(Status.OK, null)).ConfigureAwait(false);
    }
}