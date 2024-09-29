using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Services;

namespace ToDo.Application.Common.Models;
public class NotificationFactory
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public INotificationService CreateNotificationService(string notificationType)
    {
        switch (notificationType.ToLower())
        {
            case "email":
                return _serviceProvider.GetRequiredService<EmailService>();

            case "telegram":
                return _serviceProvider.GetRequiredService<TelegramService>();

            default:
                throw new ArgumentException("Invalid notification type", nameof(notificationType));
        }
    }
}


