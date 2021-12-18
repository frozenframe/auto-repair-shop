using System;

namespace AutoRepairShop.Stores
{
    public class CarStore
    {
        public event Action<Car> CarAdded;
        public void AddCar(Car car)
        {
            CarAdded?.Invoke(car);
        }
    }
}
