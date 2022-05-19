using FormBuilder.Domains.Results.Models;
using MediatR;

namespace FormBuilder.Domains.Results.Queries.GetResultById;

public class GetResultByIdQuery : IRequest<ResultModel>
{
    public Guid Id { get; set; }
}