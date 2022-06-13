using FormBuilder.Domains.Translation.Models;
using MediatR;

namespace FormBuilder.Domains.Translation.Queries.GetTranslatedText;

public class GetTranslatedTextQuery : IRequest<TranslatedModel>
{
    /// <summary>
    /// Translate from language
    /// </summary>
    public string? OriginLanguageCode { get; set; } 
    
    /// <summary>
    /// Translate to language
    /// </summary>
    public string TranslateToLanguageCode { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Text is html content or not
    /// </summary>
    public bool IsHtml { get; set; } = false;
}