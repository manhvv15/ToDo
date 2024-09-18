using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Entities;
public class Product : BaseAuditableEntity
{
   // public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Detail { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
}
