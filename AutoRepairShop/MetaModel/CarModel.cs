using System;

namespace AutoRepairShop.MetaModel
{
    public class CarModel
	{
		// Здесь не будет Nullable Id и Setters, так как марки и модели машин мы всегда берем из базы и не даем менять другим способом. Только через базу.
		#region Properties
		public int Id { get; }
		public CarBrand CarBrand { get; }
		public String Model { get; }

		#endregion Properties

		public CarModel(int id, CarBrand carBrand, String model)
		{
			this.Id = id;
			this.CarBrand = carBrand;
			this.Model = model;
		}
	}
}
