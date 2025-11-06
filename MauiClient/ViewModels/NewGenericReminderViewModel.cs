using Core.Entities.Persisted;
using Core.Ports.Driving;
using MauiClient.Adapters.Navigation;
using MauiClient.ViewModels.Abstract;
using System.Text;

namespace MauiClient.ViewModels
{
    public class NewGenericReminderViewModel : BaseViewModel
    {
        protected readonly INavigationService _navigationService;
        protected readonly IGenericEntityService<GenericReminder> _genericReminderService;
        protected readonly IGenericEntityService<Schedule> _scheduleService;

        public GenericReminder GenericReminder 
        {
            get => _genericReminder; 
            set
            {
                _genericReminder = value;
                OnPropertyChanged(nameof(GenericReminder));
            }
        }
        private GenericReminder _genericReminder = new();

        public Schedule Schedule 
        { 
            get => _schedule;
            set
            {
                _schedule = value;
                OnPropertyChanged(nameof(Schedule));
            }
        }
        private Schedule _schedule = new();

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

        public Command CreateReminderCommand => new Command(() =>
        {
            _ = CreateReminderAsync();
        });

        public NewGenericReminderViewModel(
            INavigationService navigationService,
            IGenericEntityService<GenericReminder> genericReminderService,
            IGenericEntityService<Schedule> scheduleService)
        {
            _navigationService = navigationService;
            _genericReminderService = genericReminderService;
            _scheduleService = scheduleService;
        }

        public override void OnNavigatedToWithParams(IDictionary<string, object> query)
        {
            if (!query.ContainsKey("id"))
            {
                _navigationService.NavigateBackAsync();
            }

            base.OnNavigatedToWithParams(query);

            InitDefaultData();

            var vehicleId = (int)query["id"];

            GenericReminder.VehicleId = vehicleId;
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async Task CreateReminderAsync()
        {
            if(!IsFormValid())
            {
                return;
            }

            await _genericReminderService.AddAsync(GenericReminder);

            Schedule.IsActive = true;
            Schedule.ReminderId = GenericReminder.Id;
            await _scheduleService.AddAsync(Schedule);

            _navigationService.NavigateBackNative();
        }

        private bool IsFormValid()
        {
            var err = new StringBuilder();

            if (String.IsNullOrEmpty(GenericReminder.Name))
            {
                err.AppendLine("- Name is required.");
            }

            if (Schedule.LastScheduledDate.Date > DateTime.Now.Date)
            {
                err.AppendLine("- Last scheduled date needs to be in past.");
            }

            if (Schedule.ScheduledDate.Date <= DateTime.Now.Date)
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

        private void InitDefaultData()
        {
            GenericReminder = new GenericReminder()
            {
                Name = "",
                Description = ""
            };

            Schedule = new Schedule()
            {
                SendNotifications = true,
                NotificationsIntervalInDays = 3,
                NotificationsStartBeforeInDays = 14,
                LastScheduledDate = DateTime.Now,
                ScheduledDate = DateTime.Now
            };
        }
    }
}
