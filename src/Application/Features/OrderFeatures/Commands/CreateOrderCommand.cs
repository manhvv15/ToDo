using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Features.ProductFeatures.Queries;
using ToDo.Domain.Entities;

namespace ToDo.Application.Features.OrderFeatures.Commands;
public class CreateOrderCommand : IRequest
{
    public Guid CustomerId { get; set; }
    public List<ProductOrderDto> Products { get; set; } = new List<ProductOrderDto>();
    public class ProductOrderDto
    {
        public Guid ProductId { get; set; }
        //public string? ProductName { get; set; }
        public int QuantityPurchased { get; set; }
    }
}
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    public CreateOrderCommandHandler(IApplicationDbContext context, IMapper mapper, INotificationService notificationService)
    {
        _context = context;
        _mapper = mapper;
        _notificationService = notificationService;
    }
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            var order = new Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.UtcNow,
                OrderDetails = new List<OrderDetail>(),
                TotalPrice = 0,
            };
            var productIds = request.Products.Select(p => p.ProductId).ToList();
            var productList = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            // todo foreach productlist check k ton tai nem ra luon 

            var invalidProducts = request.Products
                .Where(productRequest =>
                    !productList.Any(p => p.Id == productRequest.ProductId) ||
                    productList.First(p => p.Id == productRequest.ProductId).Quantity < productRequest.QuantityPurchased)
                .ToList();
            if (invalidProducts.Any())
            {
                var errorMessages = invalidProducts.Select(ip =>
                    $"Not enough stock for product (ID: {ip.ProductId}");
                throw new InvalidOperationException(string.Join("; ", errorMessages));
            }
            var validProducts = request.Products.Where(productRequest =>
               productList.Any(p => p.Id == productRequest.ProductId &&
                                    p.Quantity >= productRequest.QuantityPurchased)).ToList();
            order.OrderDetails = validProducts.Select(productRequest =>
            {
                var product = productList.First(p => p.Id == productRequest.ProductId);
                product.Quantity -= productRequest.QuantityPurchased;
                order.TotalPrice += product.Price * product.Quantity;
                return new OrderDetail
                {
                    ProductId = product.Id,
                    OrderId = order.Id,
                    Name = product.Name,
                    Quantity = productRequest.QuantityPurchased,
                    Price = product.Price,
                    OrderDate = DateTime.UtcNow
                };
            }).ToList();

            //order.TotalPrice = order.OrderDetails.Sum(od => od.Price * od.Quantity) ?? 0;

            _context.Orders.Add(order);
            string message = $"New order created! Order ID: {order.Id}, Total: {order.TotalPrice}";

            await _notificationService.SendMessageAsync(message);
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        throw new NotImplementedException();
    }
}

