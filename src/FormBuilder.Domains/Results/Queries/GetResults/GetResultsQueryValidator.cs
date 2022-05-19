using FluentValidation;

namespace FormBuilder.Domains.Results.Queries.GetResults;

public class GetResultsQueryValidator : AbstractValidator<GetResultsQuery>
{
    public GetResultsQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage(payload => $"Page must be greater than 0");
        RuleFor(x => x.Limit).GreaterThan(0).WithMessage(payload => $"Limit must be greater than 0");
    }
}