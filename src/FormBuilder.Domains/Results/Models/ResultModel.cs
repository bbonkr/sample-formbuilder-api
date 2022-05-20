using FormBuilder.Domains.Forms.Models;

namespace FormBuilder.Domains.Results.Models;

public class ResultModel
{
    public Guid Id { get; set; }

    public Guid FormId { get; set; }

    public string Content { get; set; }

    public FormModel Form { get; set; }
}