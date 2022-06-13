using System.Net;
using FormBuilder.Services.Translation.Models;
using kr.bbon.Core;

namespace FormBuilder.Services.Translation;

public interface ITranslationService
{
    Task<TranslationResponse> Translate(TranslationRequest request, CancellationToken cancellationToken = default);
}

public class TranslationService : ITranslationService
{
    public const string DEFAULT_LANGUAGE_CODE = "en";

    public Task<TranslationResponse> Translate(TranslationRequest request,
        CancellationToken cancellationToken = default)
    {
        var originLanguageCode = string.IsNullOrWhiteSpace(request.OriginLanguageCode)
            ? DEFAULT_LANGUAGE_CODE
            : request.OriginLanguageCode;

        if (string.IsNullOrWhiteSpace(request.TranslateToLanguageCode))
        {
            throw new ApiException(HttpStatusCode.BadRequest,
                message: "Language to Translate is required");
        }

        if (request.TranslateToLanguageCode == originLanguageCode)
        {
            throw new ApiException(HttpStatusCode.BadRequest,
                message: "Origin language code and language code to Translate could not have to be same");
        }

        if (string.IsNullOrWhiteSpace(request.Text))
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Text is required");
        }

        var translatedText = $"{request.Text} (Translated to {request.TranslateToLanguageCode})";

        return Task.FromResult(new TranslationResponse
        {
            OriginLanguageCode = originLanguageCode,
            TranslatedLanguageCode = request.TranslateToLanguageCode,
            Text = translatedText,
        });
    }
}