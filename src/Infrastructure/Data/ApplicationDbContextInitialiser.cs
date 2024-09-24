using System.Runtime.InteropServices;
using ToDo.Domain.Constants;
using ToDo.Domain.Entities;
using ToDo.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ToDo.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }
        if (!_context.Products.Any())
        {
            _context.Products.AddRange(new[]
            {
            new Product
            {
                Name = "Product 11",
                Detail = "Detail of Product 11",
                Price = 19.99,
                Quantity = 10
            },
            new Product
            {
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
                Name = "Product 14",
                Detail = "Detail of Product 14",
                Price = 45.00,
                Quantity = 7
            },
            new Product
            {
                Name = "Product 15",
                Detail = "Detail of Product 15",
                Price = 9.99,
                Quantity = 30
            }
        });

            await _context.SaveChangesAsync();
        }

        // Seed Customers
        if (!_context.Customers.Any())
        {
            _context.Customers.AddRange(new[]
            {
            new Customer
            {
                Name = "Vu",
                Address = "Nam Dinh",
                PhoneNumber = "0353271947"
            },
            new Customer
            {
                Name = "Tri",
                Address = "Ha Noi",
                PhoneNumber = "0353171947"
            }
        });

            await _context.SaveChangesAsync();
        }
    }
}

