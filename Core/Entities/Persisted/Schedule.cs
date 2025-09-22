using Core.Entities.Abstract;

namespace Core.Entities.Persisted
{
    public class Schedule : BaseEntity
    {
        public int ReminderId { get; set; }
        public DateTime LastScheduledDate { get; set; }
        public DateTime ScheduledDate { get; set; }
        public bool SendNotifications { get; set; }
        public int NotificationsStartBeforeInDays { get; set; }
        public int NotificationsIntervalInDays { get; set; }
        public bool IsActive { get; set; }
    }
}
