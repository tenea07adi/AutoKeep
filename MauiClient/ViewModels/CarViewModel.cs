using Core.Entities.DTOs;
using Core.Entities.Persisted;
using Core.Ports.Driving;
using MauiClient.Adapters.Navigation;
using MauiClient.Adapters.Popup;
using MauiClient.UI.Pages;
using MauiClient.ViewModels.Abstract;
using System.Collections.ObjectModel;
using static MauiClient.Constants.Constants;

namespace MauiClient.ViewModels
{
    public class CarViewModel : BaseViewModel
    {
        private readonly ICarEntityService _carService;
        private readonly IGenericEntityService<GenericReminder> _genericReminderService;
        private readonly IGenericEntityService<Schedule> _scheduleService;
        private readonly INavigationService _navigationService;
        private readonly IPopupNotificationsService _popupService;

        public Car Car { get; set; }

        public ObservableCollection<ReminderPreviewDTO> Reminders { get; set; }
        public ReminderPreviewDTO? SelectedReminder { get; set; }

        public Command AddNewGenericReminderCommand => new Command(() =>
        {
            _ = NavigateToNewGenericReminder();
        });

        public Command DeleteCarCommand => new Command(() =>
        {
            _ = DeleteCar();
        });

        public Command<ReminderPreviewDTO> NavigateToReminderDetailsCommand => new Command<ReminderPreviewDTO>((ReminderPreviewDTO reminder) =>
        {
            _ = NavigateToReminderDetails(reminder);
        });

        public CarViewModel(
            INavigationService navigationService,
            ICarEntityService carService,
            IGenericEntityService<GenericReminder> genericReminderService,
            IGenericEntityService<Schedule> scheduleService,
            IPopupNotificationsService popupService)
        {
            _navigationService = navigationService;
            _carService = carService;
            _genericReminderService = genericReminderService;
            _scheduleService = scheduleService;
            _popupService = popupService;
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

        private async Task NavigateToReminderDetails(ReminderPreviewDTO reminder)
        {
            var navParams = new Dictionary<string, object>
            {
                { "id", reminder.Id }
            };

            await _navigationService.NavigateToDetailsPageAsync<GenericReminderView>(navParams);
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

                var daysRemaining = (schedule.ScheduledDate.Date - DateTime.Now.Date).TotalDays;
                var totalDays = (schedule.ScheduledDate.Date - schedule.LastScheduledDate.Date).TotalDays;
                var progress = daysRemaining <= 0 ? 1 : (1 - Math.Round(daysRemaining / totalDays, 2));

                var warningPoint = schedule.SendNotifications
                    ? schedule.NotificationsStartBeforeInDays
                    : (totalDays * SchedulesConstants.ScheduleWarningPointPercent > SchedulesConstants.ScheduleWarningPointMinValue
                        ? totalDays * SchedulesConstants.ScheduleWarningPointPercent 
                        : SchedulesConstants.ScheduleWarningPointMinValue);

                var alertColor = SchedulesConstants.GoodScheduleColor;

                if(daysRemaining <= 0)
                {
                    alertColor = SchedulesConstants.ExpiredScheduleColor;
                }
                else if (daysRemaining <= warningPoint)
                {
                    alertColor = SchedulesConstants.WarningScheduleColor;
                }

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
    
        private async Task DeleteCar()
        {
            if (Car == null || Car.Id == 0)
            {
                return;
            }

            if (await _popupService.ShowQuestionPopupAsync("Delete Car", "Do you want to delete this car? Its reminders will be deleted automatically.", "Yes", "No") == false)
            {
                return;
            }

            try
            {
                await _carService.DeleteAsync(Car.Id);
            }
            catch (Exception ex)
            {
                await _popupService.ShowPopupAsync("Operation Failed!", "An unexpected error stopped the process.", "Ok");
                return;
            }

            _navigationService.NavigateBackNative();
        }
    }
}
