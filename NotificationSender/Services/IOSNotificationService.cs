using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using NotificationSender.Interfaces;
using NotificationSender.Model;

namespace NotificationSender.Services
{
    public class IOSNotificationService : IIOSNotificationService
    {
        private const int _minimumDelay = 500;
        private const int _maximumDelay = 2000;
        private static int _counter = 0;
        private readonly ILogger<IOSNotificationService> _logger;

        private static bool _succeeded
        {
            get
            {
                if (_counter == 5)
                {
                    _counter = 1;
                    return false;
                }
                _counter++;
                return true;
            }
        }

        public IOSNotificationService(ILogger<IOSNotificationService> logger)
        {
            _logger = logger;
        }

        public bool Send(Notification n)
        {
            var rnd = new Random();
            var delayTime = rnd.Next(_minimumDelay, _maximumDelay);
            _ = Task.Delay(delayTime);
            _logger.LogInformation(typeof(IOSNotificationService).Name);
            return _succeeded;
        }

        public bool ValidateInput(Notification n, out string errorMessage)
        {
            errorMessage = "Validation errors found: ";
            string pushToken;
            if (string.IsNullOrWhiteSpace(pushToken = n.PushToken) || pushToken.Length >= 50)
            {
                errorMessage += $"push token string is null, or more or equals than 50 - {pushToken}\n";
                return false;
            }

            string alert;
            if (string.IsNullOrWhiteSpace(alert = n.Alert) || alert.Length >= 2000)
            {
                errorMessage += $"alert string is null, or more or equals than 2000 - {pushToken}\n";
                return false;
            }

            return true;
        }
    }
}
