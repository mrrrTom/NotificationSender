using System;

using NotificationSender.Interfaces;
using NotificationSender.Model;

namespace NotificationSender.Services
{
    public class SenderResolver : ISenderResolver
    {
        private IAndroidNotificationService _androidService;
        private IIOSNotificationService _iosService;

        public SenderResolver(IAndroidNotificationService androidService, IIOSNotificationService iosService)
        {
            _androidService = androidService;
            _iosService = iosService;
        }

        public INotificationService Resolve(Notification n)
        {
            switch (n.Type)
            {
                case NotificationType.AndroidNotification:
                    return _androidService;
                case NotificationType.IOSNotification:
                    return _iosService;
                case NotificationType.Undefined:
                    throw new ArgumentException("Notification type wasn`t resolved");
            }
            throw new Exception();
        }
    }
}
