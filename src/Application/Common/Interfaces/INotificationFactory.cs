using ToDo.Application.Common.Models;

namespace ToDo.Application.Common.Interfaces;
public interface INotificationFactory
{
    INotificationService CreateNotificationService(NotificationType notificationType);
}

