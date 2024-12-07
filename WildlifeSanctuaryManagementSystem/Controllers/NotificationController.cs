using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildlifeSanctuaryManagementSystem.Services;

namespace WildlifeSanctuaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMedicalRecordService _medicalRecordService;

        public NotificationController(INotificationService notificationService, IMedicalRecordService medicalRecordService)
        {
            _notificationService = notificationService;
            _medicalRecordService = medicalRecordService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotifications(int userId)
        {
            var notifications = await _notificationService.GetUserNotifications(userId);
            return Ok(notifications);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNotification(NotificationDto notificationDto)
        {
            await _notificationService.CreateNotification(
                notificationDto.Type,
                notificationDto.Message,
                notificationDto.UserId);
            return Ok("Notification created successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteNotification(int id)
        {
            await _notificationService.DeleteNotification(id);
            return NoContent();
        }
    }

    public class NotificationDto
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }
}
