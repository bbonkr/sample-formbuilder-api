using AutoMapper;
using FormBuilder.Domains.Forms.Models;
using FormBuilder.Entities;

namespace FormBuilder.Domains.Forms.MappingProfiles;

public class FormProfile : Profile
{
    public FormProfile()
    {
        CreateMap<Form, FormModel>()
            .ForMember(dest => dest.ResultsCount, opt => opt.MapFrom(src => src.Results == null ? 0 : src.Results.Count));
        CreateMap<FormModel, Form>();


        CreateMap<FormItem, FormItemModel>();
        CreateMap<FormItemModel, FormItem>();

        CreateMap<FormItemOption, FormItemOptionModel>();
        CreateMap<FormItemOptionModel, FormItemOption>();

        CreateMap<FormItemTempModel, FormItemModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(dest => dest.ElementType, opt => opt.MapFrom(src => Enum.Parse<ElementTypes>(src.ElementType)));
    }
}