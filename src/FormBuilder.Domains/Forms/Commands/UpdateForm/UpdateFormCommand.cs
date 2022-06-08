using FormBuilder.Domains.Forms.Models;
using MediatR;

namespace FormBuilder.Domains.Forms.Commands.UpdateForm;

public class UpdateFormCommand : IRequest<FormModel>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    [Obsolete]
    public string Content { get; set; }

    public IList<FormItemModel> Items { get; set; } = new List<FormItemModel>();
}