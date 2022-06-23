using AutoMapper;
using FormBuilder.Domains.Forms.Models;
using FormBuilder.Entities;

namespace FormBuilder.Domains.Forms.MappingProfiles;

public class FormProfile : Profile
{
    public FormProfile()
    {
        CreateMap<FormLocaled, FormLocaledModel>()
            .ForMember(dest => dest.LanguageCode,
                opt => opt.MapFrom(src => src.Language != null ? src.Language.Code : string.Empty))
            ;
        CreateMap<FormItemLocaled, FormItemLocaledModel>()
            .ForMember(dest => dest.LanguageCode,
                opt => opt.MapFrom(src => src.Language != null ? src.Language.Code : string.Empty))
            ;
        CreateMap<FormItemOptionLocaled, FormItemOptionLocaledModel>()
            .ForMember(dest => dest.LanguageCode,
                opt => opt.MapFrom(src => src.Language != null ? src.Language.Code : string.Empty))
        ;
        CreateMap<FormLocaledModel, FormLocaled>();

        CreateMap<FormItemLocaledModel, FormItemLocaled>()
            .ForMember(dest => dest.FormItemId, opt => opt.Ignore());
        CreateMap<FormItemOptionLocaledModel, FormItemOptionLocaled>()
            .ForMember(dest => dest.FormItemOptionId, opt => opt.Ignore());

        CreateMap<Form, FormModel>()
            .ForMember(dest => dest.ResultsCount, opt => opt.MapFrom(src => src.Results == null ? 0 : src.Results.Count));
        CreateMap<FormModel, Form>();

        CreateMap<FormItem, FormItemModel>();
        CreateMap<FormItemModel, FormItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<FormItemOption, FormItemOptionModel>();
        CreateMap<FormItemOptionModel, FormItemOption>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FormItemId, opt => opt.Ignore());

        CreateMap<FormItemTempModel, FormItemModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(dest => dest.ElementType, opt => opt.MapFrom(src => Enum.Parse<ElementTypes>(src.ElementType)));
    }
}