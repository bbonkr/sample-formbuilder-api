using FluentValidation;

namespace FormBuilder.Domains.Languages.Queries.GetLanguages;

public class GetLanguagesQueryValidator : AbstractValidator<GetLanguagesQuery>
{
    public GetLanguagesQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage(payload => $"Page must be greater than 0");
        RuleFor(x => x.Limit).GreaterThan(0).WithMessage(payload => $"Limit must be greater than 0");
    }
}