using StackExchange.Redis;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;
using ToDo.Application.Common.Services;
using ToDo.Infrastructure.Data;
using ToDo.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();
builder.Services.AddControllers();
builder.Services.Configure<AppSettingsOptions>(builder.Configuration.GetSection("ProductsSettings"));
builder.Services.Configure<AppSettingsOptions>(builder.Configuration.GetSection("TelegramSettings"));
var redisConnectionString = builder.Configuration["Redis:ConnectionString"];
if (string.IsNullOrEmpty(redisConnectionString))
{
    throw new ArgumentNullException(nameof(redisConnectionString), "Redis connection string is missing in the configuration.");
}
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisConnectionString);
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
//builder.Services.AddScoped<ITelegramService, TelegramService>();
builder.Services.AddScoped<ICacheService, RedisCacheService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<TelegramService>();
builder.Services.AddScoped<NotificationFactory>();
//builder.Services.AddTransient<CreateOrderCommandHandler>();
//builder.Services.AddScoped<IEmailService, EmailService>();

//var redis = ConnectionMultiplexer.Connect(" 10.110.12.49");
//builder.Services.AddScoped(s => redis.GetDatabase());
var app = builder.Build();

if (app.Environment.IsDevelopment())    
{
    await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.UseExceptionHandler(options => { });

app.Map("/", () => Results.Redirect("/api"));

app.MapEndpoints();

app.Run();

public partial class Program { }
