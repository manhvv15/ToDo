﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Common.Interfaces;
public interface INotificationService
{
    Task SendNotification(string recipient, string subject, string message);
}
