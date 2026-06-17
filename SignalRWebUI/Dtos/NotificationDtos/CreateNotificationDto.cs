namespace SignalRWebUI.Dtos.NotificationDtos
{
    public class CreateNotificationDto
    {
        public string NotificationType { get; set; }
        public string NotificationIcon { get; set; }
        public string Description { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool Status { get; set; }
    }
}
