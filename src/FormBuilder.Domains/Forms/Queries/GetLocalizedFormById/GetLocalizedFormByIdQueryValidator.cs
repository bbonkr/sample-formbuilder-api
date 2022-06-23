using FluentValidation;

namespace FormBuilder.Domains.Forms.Queries.GetLocalizedFormById;

public class GetLocalizedFormByIdQueryValidator : AbstractValidator<GetLocalizedFormByIdQuery>
{
    public GetLocalizedFormByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage(payload => $"Id is required");
    }
}