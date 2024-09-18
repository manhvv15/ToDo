using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Entities;
public class Order : BaseAuditableEntity
{
    //public Guid OrderId { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public double TotalPrice { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    public virtual Customer? Customers { get; set; }
}
