using AutoMapper;
using FormBuilder.Domains.Forms.Models;
using FormBuilder.Entities;

namespace FormBuilder.Domains.Forms.MappingProfiles;

public class FormProfile :Profile
{
    public FormProfile()
    {
        CreateMap<Form, FormModel>();
        CreateMap<FormModel, Form>();
    }
}