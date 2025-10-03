using Core.Entities.Abstract;
using Core.Entities.Persisted;
using Core.Ports.Driven;
using Core.Ports.Driving;

namespace Core.Services
{
    public class ReminderEntityService<T> : GenericEntityService<T>, IReminderEntityService<T> where T : Reminder, new()
    {
        protected readonly IGenericEntityService<Schedule> _scheduleService;

        public ReminderEntityService(
            IGenericRepo<T> genericRepo,
            IGenericEntityService<Schedule> scheduleRepo) : base(genericRepo)
        {
            _scheduleService = scheduleRepo;
        }

        public override async Task DeleteAsync(int id)
        {
            var schedules = await _scheduleService.GetAsync(c => c.ReminderId == id);

            foreach (var schedule in schedules)
            {
                await _scheduleService.DeleteAsync(schedule.Id);
            }

            await base.DeleteAsync(id);
        }

        public async Task RescheduleAsync(int reminderId, Schedule newSchedule)
        {
            newSchedule.Id = 0;
            newSchedule.ReminderId = reminderId;
            newSchedule.IsActive = true;
            await _scheduleService.AddAsync(newSchedule);

            var lastSchedule = (await _scheduleService.GetAsync(s => s.ReminderId == reminderId && s.IsActive)).FirstOrDefault();

            if (lastSchedule != null)
            {
                lastSchedule.IsActive = false;
                await _scheduleService.UpdateAsync(lastSchedule);
            }
        }
    }
}
