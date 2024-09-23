using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Common.Models;
using ToDo.Application.Features.OrderDetailFeatures.Queries;
using ToDo.Application.Features.ProductFeatures.Queries;
using static ToDo.Application.Features.OrderDetailFeatures.Queries.GetOrderDetails;

namespace ToDo.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderDetailsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public OrderDetailsController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<OrderDetailDto>>> GetOrderDetailWithPagination([FromQuery] GetOrderDetailWithPagination query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("GetOrderDetailByJoinTables")]
    public async Task<ActionResult<List<OrderDetailsDto>>> GetOrderDetailByJoinTables([FromQuery] GetOrderDetails query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
