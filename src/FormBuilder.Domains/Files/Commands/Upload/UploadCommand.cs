using FormBuilder.Domains.Files.Models;
using MediatR;

namespace FormBuilder.Domains.Files.Commands.Upload;

public class UploadCommand:IRequest<UploadFileMediaModel>
{
    public string Name { get; set; }
    
    public string ContainerName { get; set; }
    
    public string Extension { get; set; }

    public long Size { get; set; } = 0;

    public string ContentType { get; set; } = "application/octet-stream";

    public IEnumerable<byte> FileContent { get; set; }
}