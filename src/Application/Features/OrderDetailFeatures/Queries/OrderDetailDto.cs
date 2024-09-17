using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Application.Features.OrderDetailFeatures.Queries;
public class OrderDetailDto
{
    public Guid OrderDetailId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
}
