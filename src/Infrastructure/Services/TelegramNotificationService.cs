using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Infrastructure.Services;
public class TelegramNotificationService : INotificationService
{
    private static readonly HttpClient client = new HttpClient();
    private readonly string _botToken;
    private readonly string _chatId;

    public TelegramNotificationService(string botToken, string chatId)
    {
        _botToken = botToken;
        _chatId = chatId;
    }

    public async Task SendMessageAsync(string message)
    {
        var url = $"https://api.telegram.org/bot{_botToken}/sendMessage?chat_id={_chatId}&text={Uri.EscapeDataString(message)}";
        HttpResponseMessage response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Message sent successfully.");
        }
        else
        {
            Console.WriteLine("Failed to send message.");
        }
    }
}

