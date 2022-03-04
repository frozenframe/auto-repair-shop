using System;

namespace AutoRepairShop.Stores
{
    public class CarStore
    {
        public event Action<Car> CarAdded;
        public event Action<Car> CarSelected;
        
        public void AddCar(Car car)
        {
            CarAdded?.Invoke(car);
        }


        public void SelectCar(Car car)
        {
            CarSelected?.Invoke(car);
        }


    }
}
