using ToDo.Application.Common.Interfaces;
using ToDo.Application.Features.OrderFeatures.Queries;
using static ToDo.Application.Features.OrderDetailFeatures.Queries.GetOrderDetails;

namespace ToDo.Application.Features.OrderDetailFeatures.Queries
{
    public class GetOrderDetails : IRequest<OrderDetailsDto>
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

        public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetails, OrderDetailsDto>
        {
            private readonly IApplicationDbContext _context;

            public GetOrderDetailsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<OrderDetailsDto> Handle(GetOrderDetails request, CancellationToken cancellationToken)
            {
                var orderDetails = await (from order in _context.Orders
                                          join customer in _context.Customers on order.CustomerId equals customer.Id
                                          where order.Id == request.OrderId
                                          select new OrderDetailsDto
                                          {
                                              OrderId = order.Id,
                                              OrderDate = order.OrderDate,
                                              CustomerName = customer.Name!,
                                              CustomerPhone = customer.PhoneNumber!,
                                              TotalPrice = order.TotalPrice
                                          }).FirstOrDefaultAsync(cancellationToken);

                if (orderDetails == null)
                {
                    throw new Exception("Order Id is not valid");
                }

                var orderItems = await (from orderDetail in _context.OrderDetails
                                        join product in _context.Products on orderDetail.ProductId equals product.Id
                                        where orderDetail.OrderId == request.OrderId
                                        select new OrderItemDto
                                        {
                                            ProductName = product.Name!,
                                            ProductPrice = product.Price,
                                            Quantity = orderDetail.Quantity,
                                            TotalPrice = orderDetail.Quantity * product.Price
                                        }).ToListAsync(cancellationToken);

                orderDetails.OrderItems = orderItems;
                return orderDetails;
            }
        }
    }
}
