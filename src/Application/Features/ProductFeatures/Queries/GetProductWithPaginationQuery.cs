using Newtonsoft.Json;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;


namespace ToDo.Application.Features.ProductFeatures.Queries;
public class GetProductWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public double? PriceFrom { get; set; }
    public double? PriceTo { get; set; }
}
public class GetProductWithPaginationQueryHandler : IRequestHandler<GetProductWithPaginationQuery, PaginatedList<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    public GetProductWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper, ICacheService cacheService)
    {
        _context = context;
        _mapper = mapper;
        _cacheService = cacheService;
    }
    public async Task<PaginatedList<ProductDto>> Handle(GetProductWithPaginationQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var cacheKey = $"products";
        var paginatedProducts = await _cacheService.GetOrSetAsync(cacheKey, async () =>
        {
            var query = _context.Products.AsQueryable()
                .WhereIf(request.PriceFrom.HasValue, p => p.Price >= request.PriceFrom!.Value)
                .WhereIf(request.PriceTo.HasValue, p => p.Price >= request.PriceTo!.Value)
                .WhereIf(!String.IsNullOrEmpty(request.SearchTerm), p => (p.Name != null && p.Name.Contains(request.SearchTerm!)) ||(p.Detail != null && p.Detail.Contains(request.SearchTerm!)));
            var productDtos = query.OrderBy(p => p.Name)
                                  .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
            var result = await PaginatedList<ProductDto>.CreateAsync(productDtos, request.PageNumber, request.PageSize);
            await _cacheService.SetAsync(cacheKey, result.Items);
            return result.Items;
        });
        return new PaginatedList<ProductDto>(paginatedProducts, paginatedProducts.Count, request.PageNumber, request.PageSize);
    }
}

