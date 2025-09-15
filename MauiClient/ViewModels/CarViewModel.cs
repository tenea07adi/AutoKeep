using AsyncAwaitBestPractices;
using Core.Entities.Persisted;
using Core.Ports.Driving;
using MauiClient.ViewModels.Abstract;

namespace MauiClient.ViewModels
{
    public class CarViewModel : BaseViewModel
    {
        private readonly IGenericEntityService<Car> _carService;
        public Car Car { get; set; }

        public CarViewModel(IGenericEntityService<Car> carService)
        {
            _carService = carService;
        }

        public override void OnNavigatedToWithParams(IDictionary<string, object> query)
        {
            base.OnNavigatedToWithParams(query);

            var carId = (int)query["id"];

            async Task Awaitable()
            {
                await LoadCarDetails(carId);
            }

            Awaitable().SafeFireAndForget();
        }

        private async Task LoadCarDetails(int carId)
        {
            Car = await _carService.GetAsync(carId) ?? new();
            OnPropertyChanged(nameof(Car));
        }
    }
}
