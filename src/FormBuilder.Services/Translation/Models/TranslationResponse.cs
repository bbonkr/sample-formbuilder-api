namespace FormBuilder.Services.Translation.Models;

public class TranslationResponse
{
    /// <summary>
    /// Origin language code
    /// </summary>
    public string OriginLanguageCode { get; set; } = string.Empty;

    /// <summary>
    /// Translated language code
    /// </summary>
    public string TranslatedLanguageCode { get; set; } = string.Empty;

    /// <summary>
    /// Translated text
    /// </summary> 
    public string Text { get; set; } = string.Empty;
}