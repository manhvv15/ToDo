﻿using System;
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
    public CreateOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
            foreach (var productDto in request.Products)
            {
                var product = await _context.Products.FindAsync(productDto.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {productDto.ProductId} not found.");
                }
                if (product.Quantity < productDto.QuantityPurchased)
                {
                    throw new InvalidOperationException($"Not enough stock for product {product.Name}.");
                }
                product.Quantity -= productDto.QuantityPurchased;
                var orderDetail = new OrderDetail
                {
                    ProductId = product.Id,
                    OrderId = order.OrderId,
                    Name = product.Name,
                    Quantity = productDto.QuantityPurchased,
                    Price = product.Price,
                };
                order.OrderDetails.Add(orderDetail);

                order.TotalPrice += product.Price * productDto.QuantityPurchased;
            }
            _context.Orders.Add(order);
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
