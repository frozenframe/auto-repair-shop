using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoRepairShop
{
    public class Car : INotifyPropertyChanged
    {
        private CarModel _carModel;
        private string _regNumber;
        private string _comment;
        public int? Id { get; }
        public int CLientId { get; set; }
        public CarModel CarModel
        {
            get
            {
                return _carModel;
            }
            set
            {
                _carModel = value;
                OnPropertyChanged(nameof(CarModel));
            }
        }
        public string RegNumber
        {
            get
            {
                return _regNumber;
            }
            set
            {
                _regNumber = value;
                OnPropertyChanged(nameof(RegNumber));
            }
        }
        public string Comment 
        {
            get
            {
                return _comment;
            }

            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public Car()
        {

        }
		public Car(int? id, int cLientId, CarModel carModel, string regNumber, string comment)
		{
			Id = id;
			CLientId = cLientId;
			CarModel = carModel;
			RegNumber = regNumber;
			Comment = comment;
		}

		public Car(int cLientId, CarModel carModel, string regNumber, string comment) :
			this(null, cLientId, carModel, regNumber, comment)
		{
		}


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

	}
}
