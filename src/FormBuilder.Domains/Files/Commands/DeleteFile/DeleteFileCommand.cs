using FluentValidation;
using MediatR;

namespace FormBuilder.Domains.Files.Commands.DeleteFile;

public class DeleteFileCommand : IRequest<Unit>
{
    public string Uri { get; set; }
}

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