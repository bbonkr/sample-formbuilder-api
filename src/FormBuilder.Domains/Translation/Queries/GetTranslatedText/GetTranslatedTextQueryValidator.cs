using FluentValidation;

namespace FormBuilder.Domains.Translation.Queries.GetTranslatedText;

public class GetTranslatedTextQueryValidator : AbstractValidator<GetTranslatedTextQuery>
{
    public GetTranslatedTextQueryValidator()
    {
        RuleFor(x => x.TranslateToLanguageCode).NotEmpty();
        RuleFor(x => x.Text).NotEmpty();
    }
}