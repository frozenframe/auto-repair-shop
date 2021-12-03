using System;

namespace AutoRepairShop
{
    public class CarModel
	{// Здесь не будет Nullable Id и Setters, так как марки и модели машин мы всегда берем из базы и не даем менять другим способом. Только через базу.
		public int Id { get; }
		public CarBrand CarBrand { get; }
		public string Model { get; }
		
		public CarModel(int id, CarBrand carBrand, string model)
		{
			this.Id = id;
			this.CarBrand = carBrand;
			this.Model = model;
		}
	}
}
