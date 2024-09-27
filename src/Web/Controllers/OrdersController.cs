using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Common.Models;
using ToDo.Application.Features.OrderFeatures.Commands;
using ToDo.Application.Features.OrderFeatures.Queries;

namespace ToDo.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrdersController( IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<PaginatedList<OrderDto>>> GetOrderWithPaginations([FromQuery] GetOrderWithPaginationQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrders([FromBody] CreateOrder command,CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest("Order data is not valid.");
        }

        await _mediator.Send(command, cancellationToken);

        return Ok("Customer buy product successful.");
    }   
}
