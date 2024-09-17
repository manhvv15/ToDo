using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;


namespace ToDo.Application.Features.ProductFeatures.Queries;
public class GetProductWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
}
public class GetProductWithPaginationQueryHandler : IRequestHandler<GetProductWithPaginationQuery, PaginatedList<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public GetProductWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetProductWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(p => (p.Name != null && p.Name.Contains(request.SearchTerm)) ||
                                     (p.Detail != null && p.Detail.Contains(request.SearchTerm)));
        }

        var productDtos = query.OrderBy(p => p.Name)
                               .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

        return await PaginatedList<ProductDto>.CreateAsync(productDtos, request.PageNumber, request.PageSize);
    }
}
