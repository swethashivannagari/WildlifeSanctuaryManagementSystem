namespace WildlifeSanctuaryManagementSystem.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public int UserId { get; set; }
    }
}
