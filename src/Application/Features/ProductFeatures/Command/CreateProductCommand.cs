using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Application.Features.ProductFeatures.Command;
public class CreateProductCommand : IRequest
{
    public string? Name { get; set; }
    public string? Detail { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{

    private readonly IApplicationDbContext _context;
    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Detail = request.Detail,
            Price = request.Price,
            Quantity = request.Quantity
        };
        _context.Products.Add(product);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }

}
