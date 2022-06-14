using FormBuilder.Domains.Forms.Models;
using MediatR;

namespace FormBuilder.Domains.Forms.Commands.AddForm;

public class AddFormCommand : IRequest<FormModel>
{
    public string Title { get; set; }

    public IEnumerable<FormItemModel> Items { get; set; } = new List<FormItemModel>();

    public IEnumerable<FormLocaledModel> Locales { get; set; } = new List<FormLocaledModel>();
}