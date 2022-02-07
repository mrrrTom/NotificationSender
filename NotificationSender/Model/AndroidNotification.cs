using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSender.Model
{
    public class AndroidNotification : BaseNotification
    {
        public string DeviceToken { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Condition { get; set; }
    }
}
