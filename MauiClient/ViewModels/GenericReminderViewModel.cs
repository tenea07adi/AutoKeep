using AsyncAwaitBestPractices;
using Core.Entities.Persisted;
using Core.Ports.Driving;
using MauiClient.Adapters.Navigation;
using MauiClient.Adapters.Popup;
using MauiClient.UI.Pages;
using MauiClient.ViewModels.Abstract;
using System.Collections.ObjectModel;

namespace MauiClient.ViewModels
{
    public class GenericReminderViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPopupNotificationsService _popupService;
        private IReminderEntityService<GenericReminder> _reminderService;
        private IGenericEntityService<Schedule> _scheduleService;

        public bool DisplayHistory
        {
            get => _displayHistory;
            set
            {
                _displayHistory = value;
                OnPropertyChanged(nameof(DisplayHistory));
            }
        }
        private bool _displayHistory = false;

        public bool HaveHistory
        {
            get => OldSchedules.Count > 0;
        }

        private int _reminderId;

        public GenericReminder Reminder 
        { 
            get => _reminder;
            set
            {
                _reminder = value;
                OnPropertyChanged(nameof(Reminder));
            } 
        }
        private GenericReminder _reminder = new();


        public Schedule? ActiveSchedule 
        { 
            get => _activeSchedule;
            set
            {
                _activeSchedule = value;
                OnPropertyChanged(nameof(ActiveSchedule));
            } 
        }
        private Schedule? _activeSchedule = new();

        public ObservableCollection<Schedule> OldSchedules 
        { 
            get => _oldSchedules;
            set
            {
                _oldSchedules = value;
                OnPropertyChanged(nameof(OldSchedules));
            } 
        }
        private ObservableCollection<Schedule> _oldSchedules = new();

        public Command<bool> ChangeDisplayHistoryCommand => new Command<bool>(displayHistory =>
        {
            ChangeDisplayHistory(displayHistory);
        });

        public Command NavigateToRescheduleCommand => new Command(() =>
        {
            _ = NavigateToReschedule();
        });

        public Command DeleteReminderCommand => new Command(() =>
        {
            _ = DeleteReminder();
        });

        public GenericReminderViewModel(
            INavigationService navigationService,
            IPopupNotificationsService popupService,
            IReminderEntityService<GenericReminder> reminderService,
            IGenericEntityService<Schedule> scheduleService)
        {
            _navigationService = navigationService;
            _popupService = popupService;
            _reminderService = reminderService;
            _scheduleService = scheduleService;
        }

        public override void OnNavigatedToWithParams(IDictionary<string, object> query)
        {
            if(query == null || !query.ContainsKey("id"))
            {
                return;
            }

            _reminderId = (int)query["id"];

            async Task Awaitable()
            {
                await LoadReminder();
                await LoadSchedules();
            }

            Awaitable().Wait();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            async Task Awaitable()
            {
                await LoadReminder();
                await LoadSchedules();
            }

            Awaitable().SafeFireAndForget();
        }

        private async Task NavigateToReschedule()
        {
            var parameters = new Dictionary<string, object>
            {
                { "ReminderId", _reminderId }
            };

            await _navigationService.NavigateToAsync(nameof(RescheduleReminderView), parameters);
        }

        private async Task LoadReminder()
        {
            var reminder = await _reminderService.GetAsync(_reminderId);

            if (reminder == null) 
            {
                _navigationService.NavigateBackNative();
                return;
            }

            Reminder = reminder;
        }

        private async Task LoadSchedules()
        {
            var oldSchedules = await _scheduleService.GetAsync(c => c.ReminderId == _reminderId && c.IsActive == false);
            var activeSchedule = await _scheduleService.GetAsync(c => c.ReminderId == _reminderId && c.IsActive == true);

            OldSchedules = new ObservableCollection<Schedule>(oldSchedules);
            ActiveSchedule = activeSchedule.FirstOrDefault();
        }

        private async Task DeleteReminder()
        {
            if(Reminder.Id == 0)
            {
                return;
            }

            if(await _popupService.ShowQuestionPopupAsync("Delete Reminder", "Do you want to delete this reminder? Its schedules will be deleted automatically.", "Yes", "No") == false)
            {
                return;
            }

            try
            {
                await _reminderService.DeleteAsync(Reminder.Id);
            }
            catch (Exception ex)
            {
                await _popupService.ShowPopupAsync("Operation Failed!", "An unexpected error stopped the process.", "Ok");
                return;
            }


            _navigationService.NavigateBackNative();
        }

        private void ChangeDisplayHistory(bool displayHistory)
        {
            DisplayHistory = displayHistory;
        }
    }
}
