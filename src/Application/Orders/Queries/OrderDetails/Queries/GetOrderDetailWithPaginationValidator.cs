using ToDo.Application.OrderDetailFeatures.Queries;

namespace ToDo.Application.Features.OrderDetailFeatures.Queries;
public class GetOrderDetailWithPaginationValidator : AbstractValidator<GetOrderDetailWithPagination>
{
    public GetOrderDetailWithPaginationValidator()
    {
        RuleFor(x=>x.CustomerId).NotEmpty().WithMessage("CusomerId is required");
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
