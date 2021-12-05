﻿namespace AutoRepairShop.MetaModel
{// Здесь не будет Nullable Id и Setters, так как марки и модели машин мы всегда берем из базы и не даем менять другим способом. Только через базу.
    public class CarBrand
    {
		#region Properties
		// Здесь не будет Nullable Id так как марки и модели машин мы всегда знаем точно на момент старта программы.
		public int Id { get; }
        public string BrandName { get; }

		#endregion Properties

		public CarBrand(int id, string brandName)
		{
			this.Id = id;
			this.BrandName = brandName;
		}
	}
}
