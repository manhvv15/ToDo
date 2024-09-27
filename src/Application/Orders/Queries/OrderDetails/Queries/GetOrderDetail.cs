using ToDo.Application.Common.Interfaces;
using ToDo.Application.Features.OrderFeatures.Queries;
using static ToDo.Application.Features.OrderDetailFeatures.Queries.GetOrderDetail;

namespace ToDo.Application.Features.OrderDetailFeatures.Queries
{
    public class GetOrderDetail : IRequest<OrderDetailsDto>
    {
        public Guid OrderId { get; set; }



        public class OrderDetailsDto
        {
            public Guid OrderId { get; set; }
            public DateTime OrderDate { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public string CustomerPhone { get; set; } = string.Empty;
            public double TotalPrice { get; set; }
            public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        }

        public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetail, OrderDetailsDto>
        {
            private readonly IApplicationDbContext _context;

            public GetOrderDetailsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<OrderDetailsDto> Handle(GetOrderDetail request, CancellationToken cancellationToken)
            {
                var orderDetails = await _context.Orders
                             .Include(order => order.Customers)
                             .Include(order => order.OrderDetails!)
                             .ThenInclude(orderDetail => orderDetail.Products)
                             .Where(order => order.Id == request.OrderId)
                         .Select(order => new OrderDetailsDto
                         {
                             OrderId = order.Id,
                             OrderDate = order.OrderDate,
                             CustomerName = order.Customers!.Name!,
                             CustomerPhone = order.Customers!.PhoneNumber!,
                             TotalPrice = order.TotalPrice,
                             OrderItems = order.OrderDetails!.Select(od => new OrderItemDto
                             {
                                 ProductName = od.Products!.Name!,
                                 ProductPrice = od.Products.Price,
                                 Quantity = od.Quantity,
                                 TotalPrice = od.Quantity * od.Products.Price
                             }).ToList()
                         }).FirstOrDefaultAsync(cancellationToken);
                return orderDetails!;
            }


        }
    }
}
