using FormBuilder.Domains.Results.Models;
using MediatR;

namespace FormBuilder.Domains.Results.Commands.AddResult;

public class AddResultCommand : IRequest<ResultModel>
{
    public Guid FormId { get; set; }

    public string Content { get; set; } = string.Empty;

    public IEnumerable<ResultItemModel> Items { get; set; } = Enumerable.Empty<ResultItemModel>();
}
