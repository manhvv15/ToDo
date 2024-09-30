using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;

namespace ToDo.Infrastructure.Services;
public class NotificationFactory : INotificationFactory
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public INotificationService CreateNotificationService(NotificationType notificationType)
    {
        switch (notificationType)
        {
            case NotificationType.Email:
                return _serviceProvider.GetRequiredService<EmailService>();

            case NotificationType.Telegram:
                return _serviceProvider.GetRequiredService<TelegramService>();

            default:
                throw new ArgumentException("Invalid notification type", nameof(notificationType));
        }
    }
}

