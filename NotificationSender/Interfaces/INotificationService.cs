using NotificationSender.Model;

namespace NotificationSender.Interfaces
{
    public interface INotificationService
    {
        public bool Send(Notification n);
        public bool ValidateInput(Notification n, out string errorMessage);
    }
}