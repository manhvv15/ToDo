using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace ToDo.Application.Features.ProductFeatures.Command;
public class DeleteProduct : IRequest<Guid>
{
    public Guid Id { get; set; }
}
public class DeleteProductCommandHandler : IRequestHandler<DeleteProduct, Guid>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(DeleteProduct request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var product = await _context.Products.FindAsync(request.Id);
        if (product == null)
        {
            throw new ApplicationException($"Product with ID {request.Id} not found.");
        }
        _context.Products.Remove(product);
        //await _context.Products.Where(x => x.Id == request.Id).AsQueryable().ExecuteDeleteAsync();
        await _context.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
