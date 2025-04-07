using Core.Entities.Persisted;
using Core.Ports.Driving;
using Core.Services;
using Persistence.Repository.Generic;

namespace MauiClient.Services
{
    public class LocalEntityServices
    {
        private static LocalEntityServices _instance = null;

        public LocalEntityServices()
        {
            CarService = new GenericEntityService<Car>(
                new SqliteGenericRepo<Car>());
        }

        public static LocalEntityServices Instance 
        { 
            get 
            {
                if(_instance == null)
                {
                    _instance = new LocalEntityServices();
                }

                return _instance;
            }
        }

        public IGenericEntityService<Car> CarService { get; private set; }
    }
}
