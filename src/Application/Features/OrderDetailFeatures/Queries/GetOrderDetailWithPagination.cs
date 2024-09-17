﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;

namespace ToDo.Application.Features.OrderDetailFeatures.Queries;
public class GetOrderDetailWithPagination: IRequest<PaginatedList<OrderDetailDto>>
{
    public Guid CustomerId { get; set; } 
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetOrderDetailWithPaginationHanlder : IRequestHandler<GetOrderDetailWithPagination, PaginatedList<OrderDetailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public GetOrderDetailWithPaginationHanlder(IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }
    public async Task<PaginatedList<OrderDetailDto>> Handle(GetOrderDetailWithPagination request, CancellationToken cancellationToken)
    {
        var query = _context.OrderDetails.Where(od => od.Orders != null && od.Orders.CustomerId == request.CustomerId).AsQueryable();

        var orderDetailDto = query.OrderBy(p => p.Name)
                               .ProjectTo<OrderDetailDto>(_mapper.ConfigurationProvider);

        return await PaginatedList<OrderDetailDto>.CreateAsync(orderDetailDto, request.PageNumber, request.PageSize);
    }
}
