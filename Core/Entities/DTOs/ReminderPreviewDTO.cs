using System.Drawing;

namespace Core.Entities.DTOs
{
    public class ReminderPreviewDTO
    {
        public int Id { get; set; }
        public string ReminderType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int DaysRemaining { get; set; }
        public bool SendNotifications { get; set; }
        public double Progress { get; set; }
        public string AlertColorHEX { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}
