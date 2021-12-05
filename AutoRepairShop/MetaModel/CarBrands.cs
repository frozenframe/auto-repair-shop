using System.Collections.Generic;

namespace AutoRepairShop.MetaModel
{
    // Этот класс должен быть подписан на событие обновления таблицы Car_Brands и в этом случае он должен затирать и перечитывать 
    // мапу carBrands из базы.
    // Эта мапа зачитывается в при старте программы и используется в качестве источника для ComboBox "марки авто"
    public class CarBrands
    {
        private Dictionary<int, CarBrand> carBrands;

        public CarBrands(Dictionary<int, CarBrand> carBrands)
        {
            if (this.carBrands == null || this.carBrands.Count == 0)
            {
                this.carBrands = carBrands;
            }
        }

        public Dictionary<int, CarBrand> getCarBrands()
        {
            return carBrands;
        }
    }
}
