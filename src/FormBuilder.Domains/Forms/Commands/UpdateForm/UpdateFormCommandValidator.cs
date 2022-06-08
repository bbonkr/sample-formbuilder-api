using FluentValidation;

namespace FormBuilder.Domains.Forms.Commands.UpdateForm;

public class UpdateFormCommandValidator : AbstractValidator<UpdateFormCommand>
{
    public UpdateFormCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage(payload => $"Title is required");
        //RuleFor(x => x.Content).NotEmpty().NotNull().WithMessage(payload => $"Content is required");
    }
}