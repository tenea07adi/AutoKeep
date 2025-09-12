using Core.Entities.Persisted;
using Core.Ports.Driving;
using MauiClient.Adapters.Navigation;
using MauiClient.ViewModels.Abstract;
using System.Windows.Input;

namespace MauiClient.ViewModels
{
    public class NewCarViewModel : BaseViewModel
    {
        protected readonly INavigationService _navigationService;

        protected readonly IGenericEntityService<Car> _carService;

        public Car Car { get; set; } = new();

        public ICommand AddNewCar => new Command(() =>
        {
            AddCar();
        });

        public NewCarViewModel(INavigationService navigationService, IGenericEntityService<Car> carService)
        {
            _navigationService = navigationService;

            _carService = carService;
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            Car = new Car()
            {
                FabricationDate = DateTime.Now.AddMonths(-1)
            };
        }

        private void AddCar()
        {
            if (!IsFormValid())
            {
                return;
            }

            _carService.AddAsync(Car);

            _navigationService.NavigateBackAsync();
        }

        private bool IsFormValid()
        {
            if (Car == null)
            {
                return false;
            }

            if (Car.Nickname == null || Car.Nickname == string.Empty)
            {
                return false;
            }

            if (Car.VehicleRegistrationPlate == null || Car.VehicleRegistrationPlate == string.Empty)
            {
                return false;
            }

            return true;
        }
    }
}
