using AutoMapper;
using FormBuilder.Domains.Results.Models;
using FormBuilder.Entities;

namespace FormBuilder.Domains.Results.Profiles;

public class ResultProfile :Profile
{
    public ResultProfile()
    {
        CreateMap<Result, ResultModel>();
        CreateMap<ResultModel, Result>();
    }
}