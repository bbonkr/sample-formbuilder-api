using FormBuilder.Domains.Files.Models;
using MediatR;

namespace FormBuilder.Domains.Files.Queries.GetFileByUri;

public class GetFileByUriQuery : IRequest<DownloadFileMediaModel>
{
    public string Uri { get; set; } = string.Empty;
}
