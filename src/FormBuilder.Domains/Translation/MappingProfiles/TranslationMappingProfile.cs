using AutoMapper;
using FormBuilder.Domains.Translation.Models;
using FormBuilder.Domains.Translation.Queries.GetTranslatedText;
using FormBuilder.Services.Translation.Models;

namespace FormBuilder.Domains.Translation.MappingProfiles;

public class TranslationMappingProfile:Profile
{
    public TranslationMappingProfile()
    {
        CreateMap<GetTranslatedTextQuery, TranslationRequest>();
        CreateMap<TranslationResponse, TranslatedModel>();
    }
}