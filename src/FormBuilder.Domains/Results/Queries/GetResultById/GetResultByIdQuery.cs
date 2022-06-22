using FormBuilder.Domains.Results.Models;
using MediatR;

namespace FormBuilder.Domains.Results.Queries.GetResultById;

public class GetResultByIdQuery : IRequest<ResultModel>
{
    public GetResultByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}