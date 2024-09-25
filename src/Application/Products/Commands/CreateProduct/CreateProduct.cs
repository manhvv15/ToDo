using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Application.Features.ProductFeatures.Command;
public class CreateProduct : IRequest<Product>
{
    public string? Name { get; set; }
    public string? Detail { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}
public class CreateProductCommandHandler : IRequestHandler<CreateProduct,Product>
{

    private readonly IApplicationDbContext _context;
    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var product = new Product
        {
            Name = request.Name,
            Detail = request.Detail,
            Price = request.Price,
            Quantity = request.Quantity
        };
        await _context.Products.AddAsync(product,cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return product;
    }

}
