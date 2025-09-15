using Core.Entities.Persisted;
using Core.Ports.Driving;
using MauiClient.Adapters.Navigation;
using MauiClient.UI.Pages;
using MauiClient.ViewModels.Abstract;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiClient.ViewModels
{
    public class CarsListViewModel : BaseViewModel
    {
        protected readonly INavigationService _navigationService;

        protected readonly IGenericEntityService<Car> _carService;

        private List<Car> _cars = new List<Car>();

        public ObservableCollection<Car> Cars { get; set; } = new();

        public Car? SelectedCar
        {
            get
            {
                return _selectedCar;
            }
            set
            {
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        private Car? _selectedCar = null;

        public ICommand NavigateToNewCarCommand => new Command(() =>
        {
            _ = NavigateToAddNewCar();
        });

        public ICommand NavigateToCarDetailsCommand => new Command<Car>((Car car) =>
        {
            _ = NavigateToCarDetails(car);
        });

        public CarsListViewModel(INavigationService navigationService, IGenericEntityService<Car> carService)
        {
            _navigationService = navigationService;

            _carService = carService;
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            _ = LoadCars();
        }

        public override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            new Task(async () => await LoadCars()).RunSynchronously();
        }

        private async Task LoadCars()
        {
            SelectedCar = null;

            Cars.Clear();

            _cars = await _carService.GetAsync();

            foreach (var car in _cars)
            {
                Cars.Add(car);
            }
        }

        private async Task NavigateToAddNewCar()
        {
            await _navigationService.NavigateToDetailsPageAsync<NewCarView>();
        }

        private async Task NavigateToCarDetails(Car car)
        {
            var navParams = new Dictionary<string, object>
            {
                { "id", car.Id }
            };

            await _navigationService.NavigateToDetailsPageAsync<CarView>(navParams);
        }
    }
}
