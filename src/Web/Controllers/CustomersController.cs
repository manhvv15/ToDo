using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Common.Models;
using ToDo.Application.CustomerFeatures.Queries;

namespace ToDo.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomersController( IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<PaginatedList<CustomerOrdersDto>>> GetCustomerWithPagination([FromQuery] GetCustomerWithPagination query)
    {
        return await _mediator.Send(query);
    }
}
