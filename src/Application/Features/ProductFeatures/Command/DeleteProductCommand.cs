using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.Features.ProductFeatures.Command;
public class DeleteProductCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var product = await _context.Products.FindAsync(request.Id);
        if (product == null)
        {
            throw new ApplicationException($"Product with ID {request.Id} not found.");
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
