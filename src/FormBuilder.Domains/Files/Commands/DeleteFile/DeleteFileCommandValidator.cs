using FluentValidation;

namespace FormBuilder.Domains.Files.Commands.DeleteFile;

public class DeleteFileCommandValidator : AbstractValidator<DeleteFileCommand>
{
    public DeleteFileCommandValidator()
    {
        RuleFor(x => x.Uri)
            .NotNull()
            .NotEmpty()
            .WithMessage(payload => $"Uri is required");
    }
}