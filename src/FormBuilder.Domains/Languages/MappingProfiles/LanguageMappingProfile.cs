using AutoMapper;
using FormBuilder.Domains.Languages.Models;
using FormBuilder.Entities;

namespace FormBuilder.Domains.Languages.MappingProfiles;

public class LanguageMappingProfile:Profile
{
    public LanguageMappingProfile()
    {
        CreateMap<Language, LanguageModel>();
    }
}