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

    public async Task<PaginatedList<ProductDto>> Handle(GetProductsByPriceWithPaginationQuery request, CancellationToken cancellationToken)
    {
        // var minPrice = _configuration.GetValue<double>("AppSettings:MinProductPrice");
        cancellationToken.ThrowIfCancellationRequested();
        var minPrice = _options.MinProductPrice;

        var searchTerm = request.SearchTerm ?? "";

        var query = _context.Products.AsQueryable();
        query = query.WhereIf(!string.IsNullOrEmpty(request.SearchTerm),
     p => (p.Price > minPrice &&
          !string.IsNullOrEmpty(p.Name) &&
          p.Name.Contains(searchTerm)) ||
         (!string.IsNullOrEmpty(p.Detail) && p.Detail.Contains(searchTerm)));
        query = query.Where(p => p.Price > minPrice);
        var productDtos = query.OrderBy(p => p.Name)
                               .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

        return await PaginatedList<ProductDto>.CreateAsync(productDtos, request.PageNumber, request.PageSize);
    }
}
