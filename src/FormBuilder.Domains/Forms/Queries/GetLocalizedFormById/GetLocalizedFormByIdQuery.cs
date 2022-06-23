using System;
using FormBuilder.Domains.Forms.Models;
using MediatR;

namespace FormBuilder.Domains.Forms.Queries.GetLocalizedFormById;

public class GetLocalizedFormByIdQuery : IRequest<FormModel>
{
    public GetLocalizedFormByIdQuery(Guid id, string languageCode = "en")
    {
        Id = id;
        LanguageCode = languageCode;
    }

    public Guid Id { get; }

    public string LanguageCode { get; }
}
