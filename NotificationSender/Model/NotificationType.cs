using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NotificationSender.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NotificationType
    {
        Undefined,
        AndroidNotification,
        IOSNotification
    }
}