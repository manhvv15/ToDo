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
    private readonly AppSettingsOptions _options;

    public GetProductsByPriceWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper, IOptions<AppSettingsOptions> options)
    {
        _context = context;
        _mapper = mapper;
        _options = options.Value;
    }

    //.WhereIf(!string.IsNullOrEmpty(request.SearchTerm),
    //             p => !string.IsNullOrEmpty(p.Name) && p.Name.Contains(searchTerm)) || (!string.IsNullOrEmpty(p.Detail) && p.Detail.Contains(searchTerm)))
    public async Task<PaginatedList<ProductDto>> Handle(GetProductsByPriceWithPaginationQuery request, CancellationToken cancellationToken)
    {
       
        cancellationToken.ThrowIfCancellationRequested();

        var topProductPrice = _options.topProductPrice;

        var minPrice = _options.MinProductPrice;

        var query = _context.Products.Where(p => p.Price > minPrice)
            .OrderByDescending(p => p.Price)
            .Take(topProductPrice);

        var productDtos = query.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
        // tai OrderBy(p => p.Name)
        return await PaginatedList<ProductDto>.CreateAsync(productDtos, request.PageNumber, request.PageSize);
    }
}
