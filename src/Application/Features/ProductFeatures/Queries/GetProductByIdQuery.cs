using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Application.Features.ProductFeatures.Queries;
public class GetProductByIdQuery : IRequest<Product>
{
    public Guid ProductId { get; set; }
}
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var product = await _context.Products
           .Where(p => p.Id == request.ProductId).FirstOrDefaultAsync(cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId.ToString());
        }
        return product;
    }

}
