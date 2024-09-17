using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Common.Models;
using ToDo.Application.Features.CustomerFeatures.Queries;
using ToDo.Domain.Entities;

namespace ToDo.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    public CustomersController(IConfiguration configuration, IMediator mediator)
    {
        _configuration = configuration;
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<PaginatedList<Customer>>> GetCustomerWithPagination([FromBody] GetCustomerWithPagination query)
    {
        return await _mediator.Send(query);
    }
}
