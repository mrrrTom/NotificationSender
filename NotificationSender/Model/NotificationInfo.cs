using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSender.Model
{
    public class NotificationInfo
    {
        public NotificationInfo(Notification notification)
        {
            this.Id = notification.Id;
            this.Status = notification.Status;
        }

        public string Id { get; set; }
        public NotificationStatus Status { get; set; }
    }
}
