using FormBuilder.Domains.Forms.Models;
using MediatR;

namespace FormBuilder.Domains.Forms.Commands.DeleteForm;

public class DeleteFormCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}