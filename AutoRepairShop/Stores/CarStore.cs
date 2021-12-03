using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
