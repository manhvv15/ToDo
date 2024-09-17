using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.Features.OrderFeatures.Queries;
public class GetOrderWithPagination
{
    public class GetOrderWithPaginationQuery : IRequest<PaginatedOrderResponse>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class PaginatedOrderResponse
    {
        public IEnumerable<OrderDto>? Orders { get; set; }
        public int TotalCount { get; set; }
    }

    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public IEnumerable<ProductDto>? Products { get; set; }
    }

    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
    public class GetOrderWithPaginationQueryHandler : IRequestHandler<GetOrderWithPaginationQuery, PaginatedOrderResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetOrderWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<PaginatedOrderResponse> Handle(GetOrderWithPaginationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public async Task<PaginatedOrderResponse> Handle(GetOrderWithPaginationQuery request, CancellationToken cancellationToken)
        //{
        //    var query = _context.Orders
        //        .Include(o => o.Products)
        //        .AsQueryable();

        //    var totalCount = await query.CountAsync(cancellationToken);

        //    var orders = await query
        //        .OrderBy(o => o.OrderDate)  
        //        .Skip((request.PageNumber - 1) * request.PageSize)
        //        .Take(request.PageSize)
        //        .Select(o => new OrderDto
        //        {
        //            OrderId = o.OrderId,
        //            CustomerId = o.CustomerId,
        //            OrderDate = o.OrderDate,
        //            TotalPrice = o.TotalPrice,
        //            Products = o.Products != null ? o.Products.Select(p => new ProductDto
        //            {
        //                ProductId = p.Id,
        //                Name = p.Name,
        //                Quantity = p.Quantity, 
        //                Price = p.Price
        //            }).ToList() : new List<ProductDto>()
        //        })
        //        .ToListAsync(cancellationToken);

        //    return new PaginatedOrderResponse
        //    {
        //        Orders = orders,
        //        TotalCount = totalCount
        //    };
        //}
    }
}

