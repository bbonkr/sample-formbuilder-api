using FormBuilder.Domains.Forms.Models;
using MediatR;

namespace FormBuilder.Domains.Forms.Commands.UpdateForm;

public class UpdateFormCommand :IRequest<FormModel>
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
}