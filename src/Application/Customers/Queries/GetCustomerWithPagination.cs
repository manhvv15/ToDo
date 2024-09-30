using Microsoft.Extensions.Options;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;

namespace ToDo.Application.CustomerFeatures.Queries;
public class CustomerOrdersDto
{
    public string? CustomerName { get; set; }
    public int TotalOrders { get; set; }
    public double? TotalAmountSpent { get; set; }
}

public class GetCustomerWithPagination : IRequest<PaginatedList<CustomerOrdersDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetCustomerWithPaginationHanlder : IRequestHandler<GetCustomerWithPagination, PaginatedList<CustomerOrdersDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly AppSettingsOptions _options;
    public GetCustomerWithPaginationHanlder(IApplicationDbContext context, IOptions<AppSettingsOptions> options)
    {
        _context = context;
        _options = options.Value;
    }
    public async Task<PaginatedList<CustomerOrdersDto>> Handle(GetCustomerWithPagination request, CancellationToken cancellationToken)
    {
        int topCustomerBuyProduct = _options.topCustomerBuyProduct;
        var query = _context.Customers.Select(c => new CustomerOrdersDto()
        {   
            CustomerName = c.Name,
            TotalOrders = c.Orders!.Count(),
            TotalAmountSpent = c.Orders!.Sum(o=>o.OrderDetails!.Sum(od=>od.Price*od.Quantity))

        }).OrderByDescending(c => c.TotalAmountSpent)
            .Take(topCustomerBuyProduct);
        // var query = _context.Customers.AsQueryable();
        var customers = await PaginatedList<CustomerOrdersDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        return customers;
    }
}
