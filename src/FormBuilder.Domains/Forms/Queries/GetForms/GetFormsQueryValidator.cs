using FluentValidation;

namespace FormBuilder.Domains.Forms.Queries.GetForms;

public class GetFormsQueryValidator : AbstractValidator<GetFormsQuery>
{
    public GetFormsQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage(payload => $"Page must be greater than 0");
        RuleFor(x => x.Limit).GreaterThan(0).WithMessage(payload => $"Limit must be greater than 0");
    }
}