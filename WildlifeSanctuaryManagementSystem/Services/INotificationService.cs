using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetUserNotifications(int userId);
        Task CreateNotification(string type, string message, int userId);

        Task DeleteNotification(int notificationId);
    }
}
