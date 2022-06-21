using FluentValidation;
using FormBuilder.Domains.Results.Models;
using MediatR;

namespace FormBuilder.Domains.Results.Commands.AddResult;

public class AddResultCommand : IRequest<ResultModel>
{
    public Guid FormId { get; set; }

    public string Content { get; set; }

    public IEnumerable<ResultItemModel> Items { get; set; } = Enumerable.Empty<ResultItemModel>();
}

public class AddResultCommandValidator : AbstractValidator<AddResultCommand>
{
    public AddResultCommandValidator()
    {
        RuleFor(x => x.FormId)
            .NotEqual(Guid.Empty)
            .WithMessage(payload => $"Form id is required");
        RuleFor(x => x.Content)
            .NotNull()
            .NotEmpty()
            .WithMessage(payload => $"Content is required");
        RuleFor(x => x.Items).NotEmpty().WithMessage(payload => $"Respond items are required");
    }
}