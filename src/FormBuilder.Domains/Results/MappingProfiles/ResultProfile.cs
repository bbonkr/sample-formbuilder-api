using AutoMapper;
using FormBuilder.Domains.Results.Models;
using FormBuilder.Entities;

namespace FormBuilder.Domains.Results.MappingProfiles;

public class ResultProfile : Profile
{
    public ResultProfile()
    {
        CreateMap<Result, ResultModel>();
        CreateMap<ResultModel, Result>();

        CreateMap<ResultItem, ResultItemModel>();
        CreateMap<ResultItemValue, ResultItemValueModel>();
    }
}