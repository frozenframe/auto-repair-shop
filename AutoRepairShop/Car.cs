using System;

namespace AutoRepairShop
{
    public class Car
    {
		public int? Id { get; }	
		public CarModel CarModel { get; set; }
		public String RegNumber { get; set; }
		public String Comment { get; set; }

		public Car(int? id, CarModel carModel, String regNumber, String comment)
		{
			this.Id = id;
			this.CarModel = carModel;
			this.RegNumber = regNumber;
			this.Comment = comment;
		}

		public Car(CarModel carModel, String regNumber, String comment) :
			this(null, carModel, regNumber, comment)
		{
		}

	}
}
