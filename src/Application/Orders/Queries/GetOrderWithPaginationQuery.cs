using ToDo.Application.Features.OrderFeatures.Queries;

public class GetOrderWithPaginationQueryValidator : AbstractValidator<GetOrderWithPaginationQuery>
{
    public GetOrderWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

        RuleFor(x => x.CustomerNameSearch)
            .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.CustomerNameSearch))
            .WithMessage("CustomerNameSearch must be at maximum 100 characters long.");

        RuleFor(x => x.PriceFrom)
            .GreaterThanOrEqualTo(0).WithMessage("PriceFrom must be greater than or equal to 0.");
    }

}
