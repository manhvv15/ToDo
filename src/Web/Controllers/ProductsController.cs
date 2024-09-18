using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Common.Models;
using ToDo.Application.Features.ProductFeatures.Command;
using ToDo.Application.Features.ProductFeatures.Queries;
using ToDo.Domain.Entities;

namespace ToDo.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    //1 thu vien giup giam thieu su phu thuoc giua controller va cac service khac
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetProductsWithPagination([FromQuery] GetProductWithPaginationQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    public async Task<Product> GetProductsById([FromQuery] GetProductByIdQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("productsByPrice")]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetProductsByPriceWithPagination([FromQuery] GetProductsByPriceWithPaginationQuery query, CancellationToken cancellationToken)
    {
        return await _mediator.Send(query);
    }
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (command == null)
            {
                return BadRequest("Product data is not valid.");
            }
            var product = await _mediator.Send(command);

            return Ok(new {messase = "Product created successfully.", Product = product});
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
        }

    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductCommand command)
    {
        try
        {
            if (id != command.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            var updatedProductId = await _mediator.Send(command);
            return Ok($"Product with ID {updatedProductId} updated successfully.");
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            ////if (command == null)
            ////{
            ////    return BadRequest("Product data is not valid.");
            ////}
            //if (id != command.Id)
            //{
            //    return BadRequest("Product ID mismatch.");
            //}

            ////var deleteProductId = await _mediator.Send(command, cancellationToken);
            await _mediator.Send(new DeleteProductCommand { Id = id });
            return Ok($"Product deleted successfully.");
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }
}
