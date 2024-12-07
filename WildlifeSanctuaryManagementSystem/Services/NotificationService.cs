using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class NotificationService: INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<IEnumerable<Notification>> GetUserNotifications(int userId)
    {
        return await _notificationRepository.GetNotificationsByUserId(userId);
    }
        public async Task CreateNotification(string type, string message, int userId)
        {
            var notification = new Notification
            {
                Type = type,
                Message = message,
                UserId = userId,
                Timestamp = DateTime.Now
            };

            await _notificationRepository.AddNotification(notification);
           
        }

        public async Task DeleteNotification(int notificationId)
        {
            await _notificationRepository.DeleteNotification(notificationId);
        }

    }
}
