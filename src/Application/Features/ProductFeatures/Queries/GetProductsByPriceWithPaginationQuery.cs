using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;

namespace ToDo.Application.Features.ProductFeatures.Queries;
public class GetProductsByPriceWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
}
public class GetProductsByPriceWithPaginationQueryHandler : IRequestHandler<GetProductsByPriceWithPaginationQuery, PaginatedList<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IOptions<AppSettingsOptions> _options;

    public GetProductsByPriceWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper, IConfiguration configuration, IOptions<AppSettingsOptions> options)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
        _options = options;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetProductsByPriceWithPaginationQuery request, CancellationToken cancellationToken)
    {
        // var minPrice = _configuration.GetValue<double>("AppSettings:MinProductPrice");
        var minPrice = _options.Value.MinProductPrice;

        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(p =>
                (p.Price > minPrice && p.Name != null && p.Name.Contains(request.SearchTerm)) ||
                (p.Detail != null && p.Detail.Contains(request.SearchTerm)));
        }
        else
        {
            query = query.Where(p => p.Price > minPrice);
        }

        var productDtos = query.OrderBy(p => p.Name)
                               .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

        return await PaginatedList<ProductDto>.CreateAsync(productDtos, request.PageNumber, request.PageSize);
    }
}
