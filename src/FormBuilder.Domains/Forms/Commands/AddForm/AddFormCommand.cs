using FormBuilder.Domains.Forms.Models;
using MediatR;

namespace FormBuilder.Domains.Forms.Commands.AddForm;

public class AddFormCommand : IRequest<FormModel>
{
    public string Title { get; set; }

    public string Content { get; set; }

    public IList<FormItemModel> Items { get; set; } = new List<FormItemModel>();
}