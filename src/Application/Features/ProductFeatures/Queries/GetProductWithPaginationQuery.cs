using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
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
    private readonly IDatabase _database;

    public GetProductWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper, IConnectionMultiplexer connectionMultiplexer)
    {
        _context = context;
        _mapper = mapper;
        _database = connectionMultiplexer.GetDatabase();
    }

    //public async Task<PaginatedList<ProductDto>> Handle(GetProductWithPaginationQuery request, CancellationToken cancellationToken)
    //{
    //    cancellationToken.ThrowIfCancellationRequested();
    //    var query = _context.Products.AsQueryable();

    //    if (!string.IsNullOrEmpty(request.SearchTerm))
    //    {
    //        query = query.Where(p => (p.Name != null && p.Name.Contains(request.SearchTerm)) ||
    //                                 (p.Detail != null && p.Detail.Contains(request.SearchTerm)));
    //    }
    //    if (request.PriceFrom.HasValue)
    //    {
    //        query = query.Where(p => p.Price >= request.PriceFrom.Value);
    //    }

    //    if (request.PriceTo.HasValue)
    //    {
    //        query = query.Where(p => p.Price <= request.PriceTo.Value);
    //    }

    //    var productDtos = query.OrderBy(p => p.Name)
    //                           .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

    //    return await PaginatedList<ProductDto>.CreateAsync(productDtos, request.PageNumber, request.PageSize);
    //}
    public async Task<PaginatedList<ProductDto>> Handle(GetProductWithPaginationQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var cacheKey = $"products-{request.PageNumber}-{request.PageSize}-{request.SearchTerm}-{request.PriceFrom}-{request.PriceTo}";

        var cachedProducts = await _database.StringGetAsync(cacheKey);
        if (cachedProducts.HasValue)
        {
            var cachedProductsValue = cachedProducts.ToString();
            if (!string.IsNullOrEmpty(cachedProductsValue))
            {
                var deserializeProductDto = JsonConvert.DeserializeObject<PaginatedList<ProductDto>>(cachedProductsValue);
                return deserializeProductDto ?? new PaginatedList<ProductDto>(new List<ProductDto>(), 0, 0, 0);
            }
        }
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(p => (p.Name != null && p.Name.Contains(request.SearchTerm)) ||
                                     (p.Detail != null && p.Detail.Contains(request.SearchTerm)));
        }

        if (request.PriceFrom.HasValue)
        {
            query = query.Where(p => p.Price >= request.PriceFrom.Value);
        }

        if (request.PriceTo.HasValue)
        {
            query = query.Where(p => p.Price <= request.PriceTo.Value);
        }

        var productDtos = query.OrderBy(p => p.Name)
                               .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

        var result = await PaginatedList<ProductDto>.CreateAsync(productDtos, request.PageNumber, request.PageSize);

        await _database.StringSetAsync(cacheKey, JsonConvert.SerializeObject(result), TimeSpan.FromMinutes(5));

        return result;
    }

}
