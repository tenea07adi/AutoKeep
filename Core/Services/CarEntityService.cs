using Core.Entities.Persisted;
using Core.Ports.Driven;
using Core.Ports.Driving;

namespace Core.Services
{
    public class CarEntityService : GenericEntityService<Car>, ICarEntityService
    {
        private readonly IReminderEntityService<GenericReminder> _genericReminderRepo;

        public CarEntityService(
            IGenericRepo<Car> carRepo,
            IReminderEntityService<GenericReminder> genericReminderRepo) : base(carRepo)
        {
            _genericReminderRepo = genericReminderRepo;
        }

        public override async Task DeleteAsync(int id)
        {
            var reminders = await _genericReminderRepo.GetAsync(c => c.VehicleId == id);

            foreach (var reminder in reminders)
            {
                await _genericReminderRepo.DeleteAsync(reminder.Id);
            }

            await base.DeleteAsync(id);
        }
    }
}
