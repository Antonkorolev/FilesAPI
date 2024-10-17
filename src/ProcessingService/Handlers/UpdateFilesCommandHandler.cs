using Common;
using Common.UpdateFiles;
using ProcessingService.BusinessLogic.Operations.UpdateFiles;
using ProcessingService.BusinessLogic.Operations.UpdateFiles.Models;
using UpdateFiles = ProcessingService.BusinessLogic.Operations.UpdateFiles.Models.UpdateFiles;

namespace ProcessingService.Handlers;

public sealed class UpdateFilesCommandHandler : IHandleMessages<UpdateFilesCommand>
{
    private readonly IUpdateFilesOperation _uploadFilesOperation;

    public UpdateFilesCommandHandler(IUpdateFilesOperation uploadFilesOperation)
    {
        _uploadFilesOperation = uploadFilesOperation;
    }

    public async Task Handle(UpdateFilesCommand message, IMessageHandlerContext context)
    {
        try
        {
            await _uploadFilesOperation.UpdateFilesAsync(new UpdateFilesOperationRequest(message.UpdateFiles.Select(t => new UpdateFiles(t.FileName, t.FileCode))))
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