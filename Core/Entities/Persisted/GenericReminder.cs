using Core.Entities.Abstract;

namespace Core.Entities.Persisted
{
    public class GenericReminder : Reminder
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
