using Microsoft.Extensions.Options;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;

namespace ToDo.Application.Common.Services;
public class TelegramService : INotificationService
{
    private static readonly HttpClient client = new HttpClient();
    private readonly AppSettingsOptions _options;

    public TelegramService(IOptions<AppSettingsOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendNotification(string recipient, string subject, string message)
    {
        var _botToken = _options.BotToken;
        var _chatId = _options.ChatId;
        var url = $"https://api.telegram.org/bot{_botToken}/sendMessage?chat_id={_chatId}&text={Uri.EscapeDataString(message)}";
        HttpResponseMessage response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Telegram message sent successfully.");
        }
        else
        {
            Console.WriteLine("Failed to send Telegram message.");
        }
    }

}

