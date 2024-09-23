using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;
using static ToDo.Application.Features.OrderDetailFeatures.Queries.GetOrderDetails;

namespace ToDo.Application.Features.OrderDetailFeatures.Queries;
public class GetOrderDetails : IRequest<List<OrderDetailsDto>>
{
    public class OrderDetailsDto
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetails, List<OrderDetailsDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetOrderDetailsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrderDetailsDto>> Handle(GetOrderDetails request, CancellationToken cancellationToken)
        {
            var result = await (from order in _context.Orders
                                join customer in _context.Customers on order.CustomerId equals customer.Id
                                join orderDetail in _context.OrderDetails on order.Id equals orderDetail.OrderId
                                join product in _context.Products on orderDetail.ProductId equals product.Id
                                select new OrderDetailsDto
                                {
                                    OrderId = order.Id,
                                    OrderDate = order.OrderDate,
                                    CustomerName = customer.Name,
                                    CustomerPhone = customer.PhoneNumber,
                                    ProductName = product.Name,
                                    ProductPrice = product.Price,
                                    Quantity = orderDetail.Quantity,
                                    TotalPrice = order.TotalPrice
                                }).ToListAsync(cancellationToken);

            return result;
        }
    }
}
