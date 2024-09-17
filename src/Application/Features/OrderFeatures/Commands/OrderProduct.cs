using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Application.Features.OrderFeatures.Commands;
public class OrderProduct
{
    public Guid OrderId { get; set; }
    public Order? Orders { get; set; }

    public Guid ProductId { get; set; }
    public Product? Products { get; set; }

    public int QuantityPurchased { get; set; }
}
