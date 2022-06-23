using FluentValidation;

namespace FormBuilder.Domains.Files.Queries.GetFileByUri;

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