using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Features.OrderFeatures.Queries;
public class OrderItemDto
{
    public string ProductName { get; set; } = string.Empty;
    public double ProductPrice { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
}
