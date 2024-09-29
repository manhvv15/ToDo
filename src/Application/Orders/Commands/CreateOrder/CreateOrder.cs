using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;
using ToDo.Application.Features.ProductFeatures.Queries;
using ToDo.Domain.Entities;

namespace ToDo.Application.Features.OrderFeatures.Commands;
public class CreateOrder : IRequest
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
public class CreateOrderCommandHandler : IRequestHandler<CreateOrder>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly NotificationFactory _notificationFactory;
    public CreateOrderCommandHandler(IApplicationDbContext context, IMapper mapper, NotificationFactory notificationFactory)
    {
        _context = context;
        _mapper = mapper;
        _notificationFactory = notificationFactory;
    }
    public async Task Handle(CreateOrder request, CancellationToken cancellationToken)
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

            var invalidProductIds = productIds.Except(productList.Select(p => p.Id)).ToList();
            if (invalidProductIds.Any())
            {
                throw new Exception($"Invalid product IDs in the request: {string.Join(", ", invalidProductIds)}");
            }
            var invalidProducts = new List<string>();
            foreach(var product in productList)
            {
                var productRequest = request.Products.FirstOrDefault(pr => pr.ProductId == product.Id);

                if (productRequest == null)
                {
                    invalidProducts.Add($"Product (ID: {product.Id}) does not exist in the request.");
                }
                else if (product.Quantity < productRequest.QuantityPurchased)
                {
                    invalidProducts.Add($"Not enough stock for product (ID: {productRequest.ProductId}).");
                }
                else
                {
                    product.Quantity -= productRequest.QuantityPurchased;
                    order.TotalPrice += product.Price * productRequest.QuantityPurchased;

                    order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = product.Id,
                        OrderId = order.Id,
                        Name = product.Name,
                        Quantity = productRequest.QuantityPurchased,
                        Price = product.Price,
                        OrderDate = DateTime.UtcNow
                    });
                }
            }

         

            if (invalidProducts.Any())
            {
                throw new InvalidOperationException(string.Join("; ", invalidProducts));
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);
            var message = $"New order created! Order ID: {order.Id}, Total: {order.TotalPrice}";
            var notificationService = _notificationFactory.CreateNotificationService("email");
            var emailCutomer = "manhvv15@gmail.com";
            await notificationService.SendNotification(emailCutomer, "Order Created", message);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}

