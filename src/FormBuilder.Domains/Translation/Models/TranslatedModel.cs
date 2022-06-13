namespace FormBuilder.Domains.Translation.Models;

public class TranslatedModel
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