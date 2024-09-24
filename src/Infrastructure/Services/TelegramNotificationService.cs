using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Common.Models;

namespace ToDo.Infrastructure.Services;
public class TelegramNotificationService : INotificationService
{
    private static readonly HttpClient client = new HttpClient();
    //public string _botToken;
    //public string _chatId;
    private readonly AppSettingsOptions _options;
    public TelegramNotificationService(IOptions<AppSettingsOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendMessageAsync(string message)
    {

        var _botToken = _options.BotToken;
        var _chatId = _options.ChatId;
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

