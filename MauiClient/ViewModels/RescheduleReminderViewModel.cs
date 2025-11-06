using Core.Entities.Persisted;
using Core.Ports.Driving;
using MauiClient.Adapters.Navigation;
using MauiClient.Adapters.Popup;
using MauiClient.ViewModels.Abstract;
using System.Text;
using System.Text.Json;

namespace MauiClient.ViewModels
{
    public class RescheduleReminderViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPopupNotificationsService _popupService;

        private readonly IGenericEntityService<Schedule> _scheduleService;
        private readonly IReminderEntityService<GenericReminder> _genericReminderService;

        public int ReminderId 
        { 
            get => _reminderId; 
            set
            {
                _reminderId = value;
                OnPropertyChanged(nameof(ReminderId));
            }
        }
        private int _reminderId;

        public Schedule NewSchedule
        {
            get => _newSchedule;
            set
            {
                _newSchedule = value;
                OnPropertyChanged(nameof(NewSchedule));
            }
        }
        private Schedule _newSchedule = new();

        private Schedule _lastSchedule = new();

        public string ValidationErrorMessage
        {
            get => _validationErrorMessage;
            set
            {
                _validationErrorMessage = value;
                OnPropertyChanged(nameof(ValidationErrorMessage));
            }
        }

        private string _validationErrorMessage = string.Empty;

        public Command SubmitCommand => new Command(() =>
        {
            _ = CreateNewScheduleAsync();
        });

        public RescheduleReminderViewModel(
            INavigationService navigationService,
            IPopupNotificationsService popupService,
            IGenericEntityService<Schedule> scheduleService,
            IReminderEntityService<GenericReminder> genericReminderService)
        {
            _navigationService = navigationService;
            _popupService = popupService;
            _scheduleService = scheduleService;
            _genericReminderService = genericReminderService;
        }

        public override void OnNavigatedToWithParams(IDictionary<string, object> query)
        {
            base.OnNavigatedToWithParams(query);

            if (!query.ContainsKey("ReminderId"))
            {
                _navigationService.NavigateBackNative();
                return;
            }

            ReminderId = (int)query["ReminderId"];

            LoadLastSchedule().Wait();
        }

        private async Task CreateNewScheduleAsync()
        {
            if (!IsFormValid())
            {
                return;
            }

            try
            {
                await _genericReminderService.RescheduleAsync(ReminderId, NewSchedule);
            }
            catch (Exception ex)
            {
                await _popupService.ShowPopupAsync("Operation Failed!", "An unexpected error stopped the process.", "Ok");
                return;
            }

            _navigationService.NavigateBackNative();
        }

        private async Task LoadLastSchedule()
        {
            var lastSchedule = (await _scheduleService.GetAsync(s => s.ReminderId == ReminderId && s.IsActive)).FirstOrDefault();
            var newSchedule = GetDefaultSchedule();

            if (lastSchedule != null)
            {
                _lastSchedule = lastSchedule;

                newSchedule = JsonSerializer.Deserialize<Schedule>(JsonSerializer.Serialize(_lastSchedule)) ?? GetDefaultSchedule();
                newSchedule.Id = 0;
                newSchedule.IsActive = true;
                newSchedule.LastScheduledDate = lastSchedule.ScheduledDate;
                newSchedule.ScheduledDate = lastSchedule.ScheduledDate.AddDays((lastSchedule.ScheduledDate - lastSchedule.LastScheduledDate).TotalDays);
            }

            NewSchedule = newSchedule;
        }

        private Schedule GetDefaultSchedule()
        {
            return new Schedule()
            {
                SendNotifications = true,
                NotificationsIntervalInDays = 3,
                NotificationsStartBeforeInDays = 14,
                LastScheduledDate = DateTime.Now,
                ScheduledDate = DateTime.Now
            };
        }

        private bool IsFormValid()
        {
            var err = new StringBuilder();

            if (NewSchedule.LastScheduledDate.Date > DateTime.Now.Date)
            {
                err.AppendLine("- Last scheduled date needs to be in past.");
            }

            if (NewSchedule.ScheduledDate.Date <= DateTime.Now.Date)
            {
                err.AppendLine("- Scheduled date needs to be in future.");
            }

            if (err.Length == 0)
            {
                ValidationErrorMessage = string.Empty;
                return true;
            }
            else
            {
                ValidationErrorMessage = err.ToString();
                return false;
            }
        }
    }
}
