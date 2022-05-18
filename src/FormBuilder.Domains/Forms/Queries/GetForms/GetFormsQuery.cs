using FormBuilder.Domains.Forms.Models;
using kr.bbon.Core.Models;
using MediatR;

namespace FormBuilder.Domains.Forms.Queries.GetForms;

public class GetFormsQuery : IRequest<PagedModel<FormModel>>
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
    
    public string Keyword { get; set; } = string.Empty;
}