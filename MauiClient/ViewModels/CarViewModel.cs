using Core.Entities.DTOs;
using Core.Entities.Persisted;
using Core.Ports.Driving;
using MauiClient.Adapters.Navigation;
using MauiClient.UI.Pages;
using MauiClient.ViewModels.Abstract;
using System.Collections.ObjectModel;

namespace MauiClient.ViewModels
{
    public class CarViewModel : BaseViewModel
    {
        private readonly IGenericEntityService<Car> _carService;
        private readonly IGenericEntityService<GenericReminder> _genericReminderService;
        private readonly IGenericEntityService<Schedule> _scheduleService;
        private readonly INavigationService _navigationService;

        public Car Car { get; set; }

        public ObservableCollection<ReminderPreviewDTO> Reminders { get; set; }
        public ReminderPreviewDTO? SelectedReminder { get; set; }

        public Command AddNewGenericReminderCommand => new Command(() =>
        {
            _ = NavigateToNewGenericReminder();
        });

        public CarViewModel(
            INavigationService navigationService,
            IGenericEntityService<Car> carService,
            IGenericEntityService<GenericReminder> genericReminderService,
            IGenericEntityService<Schedule> scheduleService)
        {
            _navigationService = navigationService;
            _carService = carService;
            _genericReminderService = genericReminderService;
            _scheduleService = scheduleService;
        }

        public override void OnNavigatedToWithParams(IDictionary<string, object> query)
        {
            base.OnNavigatedToWithParams(query);

            var carId = (int)query["id"];

            async Task Awaitable()
            {
                await LoadCarDetails(carId);
            }

            Awaitable().Wait();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if( Car == null || Car.Id == 0)
            {
                return;
            }

            async Task Awaitable()
            {
                await LoadReminders(Car.Id);
            }

            Awaitable().Wait();
        }

        private async Task LoadCarDetails(int carId)
        {
            Car = await _carService.GetAsync(carId) ?? new();
            OnPropertyChanged(nameof(Car));
        }

        private async Task LoadReminders(int carId)
        {
            var reminders = await _genericReminderService.GetAsync(c => c.VehicleId == Car.Id);
            Reminders = new ObservableCollection<ReminderPreviewDTO>(await GetRemindersPreviewAsync(reminders));
            OnPropertyChanged(nameof(Reminders));
        }

        private async Task NavigateToNewGenericReminder()
        {
            var navParams = new Dictionary<string, object>
            {
                { "id", Car.Id }
            };

            await _navigationService.NavigateToDetailsPageAsync<NewGenericReminderView>(navParams);
        }

        private async Task<List<ReminderPreviewDTO>> GetRemindersPreviewAsync(List<GenericReminder> genericReminders)
        {
            var reminders = new List<ReminderPreviewDTO>();

            int[] remindersIds = genericReminders.Select(x => x.Id).ToArray();

            var schedules = await _scheduleService.GetAsync(c => c.IsActive == true && remindersIds.Contains(c.ReminderId));

            foreach (var reminder in genericReminders)
            {
                var schedule = schedules.FirstOrDefault(x => x.ReminderId == reminder.Id);

                if (schedule == null)
                {
                    continue;
                }
                var daysRemaining = (schedule.ScheduledDate - DateTime.Now).TotalDays;
                var totalDays = (schedule.ScheduledDate - schedule.LastScheduledDate).TotalDays;
                var progress = 1 - Math.Round(daysRemaining / totalDays, 2);

                var alertColor = daysRemaining switch
                {
                    <= 0 => "#E90017",
                    <= 14 => "#FFB73E",
                    _ => "#2FD300"
                };

                reminders.Add(new ReminderPreviewDTO
                {
                    Id = reminder.Id,
                    ReminderType = nameof(GenericReminder),
                    Name = reminder.Name,
                    DaysRemaining = (int)daysRemaining,
                    Progress = progress,
                    AlertColorHEX = alertColor,
                    SendNotifications = schedule.SendNotifications,
                    Icon = "notification.png"
                });
            }
            return reminders;
        }
    }
}
