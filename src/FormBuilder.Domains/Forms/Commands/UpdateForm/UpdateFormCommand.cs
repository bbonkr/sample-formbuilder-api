using FormBuilder.Domains.Forms.Models;
using MediatR;

namespace FormBuilder.Domains.Forms.Commands.UpdateForm;

public class UpdateFormCommand : IRequest<FormModel>
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public IList<FormItemModel> Items { get; set; } = new List<FormItemModel>();

    public IEnumerable<FormLocaledModel> Locales { get; set; } = new List<FormLocaledModel>();
}