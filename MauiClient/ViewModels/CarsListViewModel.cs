using Core.Entities.Persisted;

namespace MauiClient.ViewModels
{
    public class CarsListViewModel
    {
        public List<Car> Cars = new List<Car>();

        public CarsListViewModel()
        {
            LoadCars();
        }

        private void LoadCars()
        {
            Cars.Add(new Car()
            {
                FabricationDate = DateOnly.Parse(DateTime.Now.AddYears(7).ToString()),
                Nickname = "BMV S1",
                KmPassed = 204065,
                VehicleRegistrationPlate = "DJ-02-TST"
            });

            Cars.Add(new Car()
            {
                FabricationDate = DateOnly.Parse(DateTime.Now.AddYears(5).ToString()),
                Nickname = "Hyunadi 1",
                KmPassed = 20051,
                VehicleRegistrationPlate = "B-561-NEW"
            });

            Cars.Add(new Car()
            {
                FabricationDate = DateOnly.Parse(DateTime.Now.AddYears(2).ToString()),
                Nickname = "MiniOne",
                KmPassed = 100000,
                VehicleRegistrationPlate = "DJ-22-EXE"
            });
        }
    }
}
