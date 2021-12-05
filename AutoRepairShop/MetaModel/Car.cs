namespace AutoRepairShop.MetaModel
{
    public class Car
    {
		#region Properties
		public int? Id { get; }
		public int CLientId { get; set; }
		public CarModel CarModel { get; set; }
		public string RegNumber { get; set; }
		public string Comment { get; set; }

		#endregion Properties

		#region Constructors
		public Car(int? id, int cLientId, CarModel carModel, string regNumber, string comment)
		{
			this.Id = id;
			this.CLientId = cLientId;
			this.CarModel = carModel;
			this.RegNumber = regNumber;
			this.Comment = comment;
		}

		public Car(int cLientId, CarModel carModel, string regNumber, string comment) :
			this(null, cLientId, carModel, regNumber, comment)
		{
		}

		#endregion Constructors
	}
}
