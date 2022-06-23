using FluentValidation;

namespace FormBuilder.Domains.Results.Commands.AddResult;

public class AddResultCommandValidator : AbstractValidator<AddResultCommand>
{
    public AddResultCommandValidator()
    {
        RuleFor(x => x.FormId)
            .NotEqual(Guid.Empty)
            .WithMessage(payload => $"Form id is required");

        RuleFor(x => x.Items).NotEmpty().WithMessage(payload => $"Respond items are required");
    }
}