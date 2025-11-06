using Core.Entities.Abstract;
using Core.Entities.Persisted;

namespace Core.Ports.Driving
{
    public interface IReminderEntityService<T> : IGenericEntityService<T> where T : Reminder, new()
    {
        public Task RescheduleAsync(int reminderId, Schedule newSchedule);
    }
}
