using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using NotificationSender.Interfaces;
using NotificationSender.Model;

namespace NotificationSender.Services
{
    public class AndroidNotificationService : IAndroidNotificationService
    {
        private const int _minimumDelay = 500;
        private const int _maximumDelay = 2000;
        private static int _counter = 1;
        private readonly ILogger<AndroidNotificationService> _logger;

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

        public AndroidNotificationService(ILogger<AndroidNotificationService> logger)
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
            string deviceToken;
            if (string.IsNullOrWhiteSpace(deviceToken = n.DeviceToken) || deviceToken.Length >= 50)
            {
                errorMessage += $"device token string is null, or more or equals than 50 - {deviceToken}\n";
                return false;
            }

            string message;
            if (string.IsNullOrWhiteSpace(message = n.Message) || message.Length >= 2000)
            {
                errorMessage += $"message string is null, or more or equals than 2000 - {message}\n";
                return false;
            }

            string title;
            if (string.IsNullOrWhiteSpace(title = n.Title) || title.Length >= 255)
            {
                errorMessage += $"title string is null, or more or equals than 225 - {title}\n";
                return false;
            }

            string condition;
            if (!string.IsNullOrWhiteSpace(condition = n.Condition) && condition.Length >= 2000)
            {
                errorMessage += $"condition string is or more or equals than 2000 - {condition}\n";
                return false;
            }

            return true;
        }
    }
}
