using FluentValidation;
using FormBuilder.Domains.Files.Models;
using MediatR;

namespace FormBuilder.Domains.Files.Queries.GetFileByUri;

public class GetFileByUriQuery :IRequest<DownloadFileMediaModel>
{
    public string Uri { get; set; }
}

public class GetFileByUriQueryValidator : AbstractValidator<GetFileByUriQuery>
{
    public GetFileByUriQueryValidator()
    {
        RuleFor(x => x.Uri)
            .NotNull()
            .NotEmpty()
            .WithMessage(payload => $"Uri is required");
    }
}