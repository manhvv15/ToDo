using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Mappings;
using ToDo.Domain.Entities;

namespace ToDo.Application.Features.ProductFeatures.Queries;
public class ProductDto : IMapFrom<Product>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Detail { get; set; }
    public double Price { get; set; }


}
