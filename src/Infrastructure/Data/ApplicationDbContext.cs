using System.Reflection;
using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ToDo.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Product>().HasData(
           new Product
           {
               Id = Guid.NewGuid(),
               Name = "Product 11",
               Detail = "Detail of Product 11",
               Price = 19.99,
               Quantity = 10
           },
           new Product
           {
               Id = Guid.NewGuid(),
               Name = "Product 12",
               Detail = "Detail of Product 12",
               Price = 29.99,
               Quantity = 5
           },
           new Product
           {
               Id = Guid.NewGuid(),
               Name = "Product 13",
               Detail = "Detail of Product 13",
               Price = 15.50,
               Quantity = 20
           },
           new Product
           {
               Id = Guid.NewGuid(),
               Name = "Product 14",
               Detail = "Detail of Product 14",
               Price = 45.00,
               Quantity = 7
           },
           new Product
           {
               Id = Guid.NewGuid(),
               Name = "Product 15",
               Detail = "Detail of Product 15",
               Price = 9.99,
               Quantity = 30
           }
       );
        builder.Entity<Customer>().HasData(
            new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Manh",
                Address = "Nam Dinh",
                PhoneNumber = "0353271947"
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Tri",
                Address = "Ha Noi",
                PhoneNumber = "0353171947"
            }
        );
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
