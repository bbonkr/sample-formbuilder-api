using FluentValidation;
using FormBuilder.Domains.Forms.Models;
using MediatR;

namespace FormBuilder.Domains.Forms.Queries.GetFormById;

public class GetFormByIdQuery : IRequest<FormModel>
{
    public Guid Id { get; set; }
}