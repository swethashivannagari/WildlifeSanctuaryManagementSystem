using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotificationsByUserId(int userId);
        Task AddNotification(Notification notification);
        Task DeleteNotification(int notificationId);


    }
}
