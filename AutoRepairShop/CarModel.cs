using System;

namespace AutoRepairShop
{
    public class CarModel
    {
		public int? Id { get; }
		public int BrandId { get; set; }
		public String BrandName { get; set; }
		public String Model { get; set; }
		
		public CarModel(int? id, int brandId, String brandName, String model)
		{
			this.Id = id;
			this.BrandId = brandId;
			this.BrandName = brandName;
			this.Model = model;
		}

		public CarModel(int brandId, String brandName, String model) :
			this(null, brandId, brandName, model)
		{
		}

	}
}
