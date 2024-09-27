using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Common.Models;
public class AppSettingsOptions
{
    public double MinProductPrice { get; set; }
    public int topProductPrice { get; set; }
    public string? BotToken { get; set; }
    public string? ChatId { get; set; }

}
