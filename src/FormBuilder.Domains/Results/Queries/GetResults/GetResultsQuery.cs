using FormBuilder.Domains.Results.Models;
using kr.bbon.Core.Models;
using MediatR;

namespace FormBuilder.Domains.Results.Queries.GetResults;

public class GetResultsQuery : IRequest<PagedModel<ResultModel>>
{
    public Guid? FormId { get; set; }
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
}