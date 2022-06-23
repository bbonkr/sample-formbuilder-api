using FormBuilder.Domains.Files.Models;
using MediatR;

namespace FormBuilder.Domains.Files.Commands.Upload;

public class UploadCommand : IRequest<UploadFileMediaModel>
{
    public string Name { get; set; } = string.Empty;

    public string ContainerName { get; set; } = string.Empty;

    public string Extension { get; set; } = string.Empty;

    public long Size { get; set; } = 0;

    public string ContentType { get; set; } = "application/octet-stream";

    public IEnumerable<byte> FileContent { get; set; } = Enumerable.Empty<byte>();
}