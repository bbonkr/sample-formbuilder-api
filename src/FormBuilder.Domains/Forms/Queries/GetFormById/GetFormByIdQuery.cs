using FluentValidation;
using FormBuilder.Domains.Forms.Models;
using MediatR;

namespace FormBuilder.Domains.Forms.Queries.GetFormById;

public class GetFormByIdQuery : IRequest<FormModel>
{
    public GetFormByIdQuery(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get;  }
}