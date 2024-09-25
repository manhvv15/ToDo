using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.Features.ProductFeatures.Command;
public class UpdateProductCommandValidator : AbstractValidator<UpdateProduct>
{
    private readonly IApplicationDbContext _context;
    public UpdateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product ID is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product Name is required.")
            .MustAsync(BeUniqueName).WithMessage("Product name must be unique.");
        RuleFor(x => x.Detail).MaximumLength(1000).WithMessage("Product detail has max 1000 characters").When(x => !string.IsNullOrEmpty(x.Detail));
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price muse be greater than 0");
    }

    private async Task<bool> BeUniqueName(UpdateProduct command,string? name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        var existingProduct = await _context.Products
        .Where(p => p.Id == command.Id && p.Name == name)
        .AnyAsync(cancellationToken);

        return !existingProduct;
    }
}
