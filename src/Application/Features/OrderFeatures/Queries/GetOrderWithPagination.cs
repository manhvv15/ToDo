using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;
using ToDo.Application.Features.ProductFeatures.Queries;
namespace ToDo.Application.Features.OrderFeatures.Queries;
public class GetOrderWithPagination
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string? CustomerName { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> orderItem { get; set; } = new List<OrderItemDto>();
    }
    public class GetOrderWithPaginationQuery : IRequest<PaginatedList<OrderDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? CustomerNameSearch { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        public DateTime? DateTimeFrom { get; set; }
        public DateTime? DateTimeTo { get; set; }
    }

    public class GetOrderWithPaginationQueryHandler : IRequestHandler<GetOrderWithPaginationQuery, PaginatedList<OrderDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetOrderWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PaginatedList<OrderDto>> Handle(GetOrderWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = from order in _context.Orders
                        join customer in _context.Customers on order.CustomerId equals customer.Id
                        join orderDetail in _context.OrderDetails on order.Id equals orderDetail.OrderId
                        join product in _context.Products on orderDetail.ProductId equals product.Id
                        select new OrderDto
                        {
                            CustomerName = customer.Name,
                            TotalPrice = order.TotalPrice,
                            orderItem = _context.OrderDetails.Where(od => od.OrderId == order.Id).Select(od => new OrderItemDto
                            {
                                ProductName = product.Name!,
                                ProductPrice = product.Price,
                                Quantity = orderDetail.Quantity,
                                TotalPrice = od.Quantity * product.Price
                            }).ToList(),
                            OrderDate = order.OrderDate,
                        };
            query = query
       .WhereIf(!string.IsNullOrEmpty(request.CustomerNameSearch), q => q.CustomerName!.Contains(request.CustomerNameSearch!))
       .WhereIf(request.PriceFrom.HasValue, q => q.TotalPrice >= request.PriceFrom)
       .WhereIf(request.PriceTo.HasValue, q => q.TotalPrice <= request.PriceTo)
       .WhereIf(request.DateTimeFrom.HasValue, q => q.OrderDate >= request.DateTimeFrom)
       .WhereIf(request.DateTimeTo.HasValue, q => q.OrderDate <= request.DateTimeTo)
       .OrderBy(q => q.CustomerName);
            return await PaginatedList<OrderDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}

