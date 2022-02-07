using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSender.Model
{
    public class IOSNotification : BaseNotification
    {
        public string PushToken { get; set; }
        public string Alert { get; set; }
        public int Priority { get; set; }
        public bool IsBackground { get; set; }
    }
}
