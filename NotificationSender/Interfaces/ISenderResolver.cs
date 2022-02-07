using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NotificationSender.Model;
using NotificationSender.Services;

namespace NotificationSender.Interfaces
{
    public interface ISenderResolver
    {
        public INotificationService Resolve(Notification n);
    }
}
