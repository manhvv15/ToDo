using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;
using ToDo.Domain.Entities;

namespace ToDo.Application.CustomerFeatures.Queries;
public class GetCustomerWithPagination : IRequest<PaginatedList<Customer>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetCustomerWithPaginationHanlder : IRequestHandler<GetCustomerWithPagination, PaginatedList<Customer>>
{
    private readonly IApplicationDbContext _context;
    public GetCustomerWithPaginationHanlder(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<Customer>> Handle(GetCustomerWithPagination request, CancellationToken cancellationToken)
    {
        var query = _context.Customers.AsQueryable();
        var customers = await PaginatedList<Customer>.CreateAsync(query, request.PageNumber, request.PageSize);
        return customers;
    }
}
