using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NotificationSender.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NotificationStatus
    {
        Undefined,
        DeliverySucceed,
        DeliveryFailed,
        NotificationCreated
    }
}