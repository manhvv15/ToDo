using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Entities;
public class OrderDetail
{
    
    public Guid OrderDetailId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
    public virtual Order? Orders{ get; set; }
    public virtual Product? Products { get; set; }
}
