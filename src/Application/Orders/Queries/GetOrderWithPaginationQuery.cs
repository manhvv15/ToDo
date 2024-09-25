using System.Globalization;
using FluentValidation;
using static ToDo.Application.Features.OrderFeatures.Queries.GetOrderWithPagination;

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

        RuleFor(x => x.DateTimeFrom)
             .Must(BeValidUtcDate).WithMessage("Invalid format for DateTimeFrom. Use format yyyy-MM-ddTHH:mm:ss.fffffffZ.");

        RuleFor(x => x.DateTimeTo)
            .Must(BeValidUtcDate).WithMessage("Invalid format for DateTimeTo. Use format yyyy-MM-ddTHH:mm:ss.fffffffZ.")
            .GreaterThanOrEqualTo(x => x.DateTimeFrom)
            .When(x => x.DateTimeFrom.HasValue && x.DateTimeTo.HasValue)
            .WithMessage("DateTimeTo must be greater than or equal to DateTimeFrom.");
    }
    private bool BeValidUtcDate(DateTime? dateTime)
    {
        if (!dateTime.HasValue) return true;
        return dateTime.Value.Kind == DateTimeKind.Utc;
    }
}
