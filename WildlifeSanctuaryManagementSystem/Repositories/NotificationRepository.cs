using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly SanctuaryDbContext _context;

        public NotificationRepository(SanctuaryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserId(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.Timestamp)
                .ToListAsync();
        }

        public async Task<Notification> GetNotificationById(int id)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.NotificationId == id);
            if (notification == null)
            {
                throw new Exception($"Notification with ID {id} does not exist.");
            }
            return notification;
        }

        public async Task AddNotification(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotification(int notificationId)
        {
            var notification = await GetNotificationById(notificationId);
            if (notification == null)
            {
                throw new Exception($"Notification with ID {notificationId} does not exist.");
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }
    }
}
