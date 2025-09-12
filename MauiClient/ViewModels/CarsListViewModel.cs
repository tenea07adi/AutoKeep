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

        public ICommand NavigateToNewCarCommand => new Command(() =>
        {
            _ = NavigateToAddNewCar();
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
            _cars = await _carService.GetAsync();

            Cars.Clear();

            foreach (var car in _cars)
            {
                Cars.Add(car);
            }
        }

        private async Task NavigateToAddNewCar()
        {
            await _navigationService.NavigateToDetailsPageAsync<NewCarView>();
        }

        private void PopulateDummyData()
        {
            _cars.Add(new Car()
            {
                FabricationDate = DateTime.Now.AddYears(7),
                Nickname = "BMV S1",
                KmPassed = 204065,
                VehicleRegistrationPlate = "DJ-02-TST"
            });

            _cars.Add(new Car()
            {
                FabricationDate = DateTime.Now.AddYears(5),
                Nickname = "Hyunadi 1",
                KmPassed = 20051,
                VehicleRegistrationPlate = "B-561-NEW"
            });

            _cars.Add(new Car()
            {
                FabricationDate = DateTime.Now.AddYears(2),
                Nickname = "MiniOne",
                KmPassed = 100000,
                VehicleRegistrationPlate = "DJ-22-EXE"
            });
        }
    }
}
