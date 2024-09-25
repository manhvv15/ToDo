using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Features.OrderFeatures.Commands;
public class CreateOrderCommandValidator : AbstractValidator<CreateOrder>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.Products)
                .NotEmpty().WithMessage("At least one product is required.")
                .Must(products => products.All(p =>  p.QuantityPurchased > 0))
                .WithMessage("Each product must have a valid Product ID and quantity greater than zero.");
    }
}
