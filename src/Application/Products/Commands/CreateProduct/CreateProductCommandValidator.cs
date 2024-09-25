using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.Features.ProductFeatures.Command;
public class CreateProductCommandValidator : AbstractValidator<CreateProduct>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MustAsync(BeUniqueName).WithMessage("Product name must be unique.");
        RuleFor(x => x.Detail).MaximumLength(1000).WithMessage("Product detail has max 1000 characters").When(x => !string.IsNullOrEmpty(x.Detail));
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price muse be greater than 0");
    }

    private async Task<bool> BeUniqueName(string? name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return !await _context.Products.AnyAsync(p => p.Name == name, cancellationToken);
    }
}
