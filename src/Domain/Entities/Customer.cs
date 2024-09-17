using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Entities;
public class Customer 
{
    public Guid Id { get; set; }
    public string? Name { get; set; }   
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }

}
