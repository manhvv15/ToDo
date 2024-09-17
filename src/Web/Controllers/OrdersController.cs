using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Common.Models;
using ToDo.Application.Features.CustomerFeatures.Queries;
using ToDo.Application.Features.OrderFeatures.Commands;
using ToDo.Application.Features.OrderFeatures.Queries;
using ToDo.Domain.Entities;
using static ToDo.Application.Features.OrderFeatures.Queries.GetOrderWithPagination;

namespace ToDo.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    public OrdersController(IConfiguration configuration, IMediator mediator)
    {
        _configuration = configuration;
        _mediator = mediator;
    }
    //[HttpGet]
    //public async Task<ActionResult<PaginatedOrderResponse>> GetOrderWithPagination(
    //    [FromQuery] GetOrderWithPaginationQuery query,
    //    CancellationToken cancellationToken)
    //{
    //    return await _mediator.Send(query,cancellationToken);
    //}
    [HttpPost]
    public async Task<IActionResult> CreateOrders([FromBody] CreateOrderCommand command,CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest("Order data is not valid.");
        }

        await _mediator.Send(command, cancellationToken);

        return Ok("Customer buy product successful.");
    }   
}
