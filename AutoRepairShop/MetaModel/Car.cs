using System;

namespace AutoRepairShop.MetaModel
{
    public class Car
    {
		public int? Id { get; }
		public int CLientId { get; set; }
		public CarModel CarModel { get; set; }
		public String RegNumber { get; set; }
		public String Comment { get; set; }

		public Car(int? id, int cLientId, CarModel carModel, String regNumber, String comment)
		{
			this.Id = id;
			this.CLientId = cLientId;
			this.CarModel = carModel;
			this.RegNumber = regNumber;
			this.Comment = comment;
		}

		public Car(int cLientId, CarModel carModel, String regNumber, String comment) :
			this(null, cLientId, carModel, regNumber, comment)
		{
		}

	}
}
