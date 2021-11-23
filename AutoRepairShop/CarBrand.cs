using System;

namespace AutoRepairShop
{// Здесь не будет Nullable Id и Setters, так как марки и модели машин мы всегда берем из базы и не даем менять другим способом. Только через базу.
	public class CarBrand
    {// Здесь не будет Nullable Id так как марки и модели машин мы всегда знаем точно на момент старта программы.
        public int Id { get; }
        public String BrandName { get; }

		public CarBrand(int id, String brandName)
		{
			this.Id = id;
			this.BrandName = brandName;
		}
	}
}
