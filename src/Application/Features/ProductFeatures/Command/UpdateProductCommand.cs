﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.Features.ProductFeatures.Command;
public class UpdateProductCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Detail { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
{

    private readonly IApplicationDbContext _context;
    //  private readonly IValidator<Product> _validator;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        //_validator = validator;
    }
    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Id);
        if (product == null)
        {
            throw new ApplicationException($"Product with Id {request.Id} not found");
        }

        product.Name = request.Name;
        product.Detail = request.Detail;
        product.Price = request.Price;
        product.Quantity = request.Quantity;
        //var validationResult = await _validator.ValidateAsync(product, cancellationToken);
        //if (!validationResult.IsValid)
        //{
        //    throw new ValidationException(validationResult.Errors);
        //}
        await _context.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}