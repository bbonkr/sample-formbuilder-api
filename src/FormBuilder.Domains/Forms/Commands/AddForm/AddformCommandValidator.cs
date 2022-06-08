using FluentValidation;

namespace FormBuilder.Domains.Forms.Commands.AddForm;

public class AddformCommandValidator : AbstractValidator<AddFormCommand>
{
    public AddformCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage(payload => $"Title is required");
        //RuleFor(x => x.Content).NotEmpty().NotNull().WithMessage(payload => $"Content is required");
    }
}