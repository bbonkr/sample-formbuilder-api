using FormBuilder.Domains.Languages.Models;
using kr.bbon.Core.Models;
using MediatR;

namespace FormBuilder.Domains.Languages.Queries.GetLanguages;

public class GetLanguagesQuery :IRequest<PagedModel<LanguageModel>>
{
    public int Page { get; set; } = 1;
    
    public int Limit { get; set; } = 10;
    
    public string? Keyword { get; set; } = string.Empty;
}