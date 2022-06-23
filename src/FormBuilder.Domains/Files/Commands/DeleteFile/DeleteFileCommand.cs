using MediatR;

namespace FormBuilder.Domains.Files.Commands.DeleteFile;

public class DeleteFileCommand : IRequest<Unit>
{
    public string Uri { get; set; } = string.Empty;
}
