using AutoRepairShop.Model;
using AutoRepairShop.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutoRepairShop.ViewModel
{
    public class CarDataViewModel : ViewModelBase
    {
        private RelayCommand addCarToClientCommand;

        private CarStore _carstore;
        private Car _car;
        private CarBrand _selectedCarBrand;
        private CarModel _selectedCarModel;
        private ClientViewModel _client;
        DbCarModel dbCarModel;
        DbCarBrand dbCarBrand;
        DbClientCars dbClientCars;        
        public ObservableCollection<CarBrand> CarBrands { get; set; }
        public ObservableCollection<CarModel> CarModels { get; set; }
        
        #region Properties
        public ClientViewModel Client
        {
            get
            {
                return _client;
            }
            set
            {
                _client = value;
                OnPropertyChanged(nameof(Client));
            }
        }

        public CarBrand SelectedCarBrand
        {
            get
            {
                return _selectedCarBrand;
            }
            set
            {
                //вопрос о том что carbrand должен быть выше carmodel
                if (_selectedCarBrand == value) return;
                _selectedCarBrand = value;
                //if(Car.CarModel != null)
                //{
                //    Car.CarModel.CarBrand = value;
                //}                
                FillCarModelComboBox(_selectedCarBrand);
                OnPropertyChanged(nameof(SelectedCarBrand));
            }
        }

        public CarModel SelectedCarModel
        {
            get
            {
                return _selectedCarModel;
            }
            set
            {
                _selectedCarModel = value;
                //Car.CarModel = value;
                OnPropertyChanged(nameof(SelectedCarModel));
            }
        }

        public Car Car
        {
            get
            {
                return _car;                
            }
            set
            {
                _car = value;
                OnPropertyChanged(nameof(Car));
            }
        }
        private string _regNumber;
        public string RegNumber
        {
            get
            {
                return _regNumber;
            }
            set
            {
                _regNumber = value;
            }
        }

        private string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }

        #endregion Properties

        #region Constructors
        public CarDataViewModel(CarStore carStore, ClientViewModel client)
        {
            _carstore = carStore;
            //dbInteraction = new DbInteraction();
            dbCarModel = new DbCarModel();
            dbCarBrand = new DbCarBrand();
            dbClientCars = new DbClientCars();
            Car = new Car();
            CarBrands = new ObservableCollection<CarBrand>();
            foreach (var row in dbCarBrand.GetCarBrands())
            {
                CarBrands.Add(row.Value);
            }
            CarModels = new ObservableCollection<CarModel>();
            Client = client;
        }

        public CarDataViewModel(Car car, CarStore carStore,ClientViewModel client) : this(carStore, client)
        {
            Car = car;
            SelectedCarBrand = Car.CarModel.CarBrand;
            SelectedCarModel = car.CarModel;
            RegNumber = car.RegNumber;
            Comment = car.Comment;
        }
        #endregion Constructors

        //------------------------------- Предположительно ненужно---------------------------
        //private RelayCommand brandSelectionChangedCommand;

        //public ICommand BrandSelectionChangedCommand
        //{
        //    get
        //    {
        //        if (brandSelectionChangedCommand == null)
        //        {
        //            brandSelectionChangedCommand = new RelayCommand(BrandSelectionChanged);
        //        }

        //        return brandSelectionChangedCommand;
        //    }
        //}

        //private void BrandSelectionChanged(object commandParameter)
        //{
        //   var carModelWithSelectedBrand = dbInteraction.GetModels(SelectedCarBrand);
        //    CarModels.Add(new CarModel(1, new CarBrand(1, ""), "f911"));
        //    CarModels.Add(new CarModel(1, new CarBrand(2, ""), "gg"));
        //    
        //}


        private void FillCarModelComboBox( CarBrand selectedCarBrand)
        {
            CarModels.Clear();
            var carModelWithSelectedBrand = dbCarModel.GetCarModels(selectedCarBrand);
            foreach(var row in carModelWithSelectedBrand)
            {
                CarModels.Add(row);
            }
        }


        

        public ICommand AddCarToClientCommand
        {
            get
            {
                if (addCarToClientCommand == null)
                {
                    addCarToClientCommand = new RelayCommand(AddCarToClient);
                }
                return addCarToClientCommand;
            }
        }

        private void AddCarToClient(object commandParameter)
        {
            if(Car.Id is null)
            {
                var newCar = new Car((int)Client.Id, SelectedCarModel, RegNumber, Comment);                
                newCar = dbClientCars.AddClientCar(new Client(Client.Id, Client.Lastname, Client.Name, Client.Surname, Client.Phone, Client.Comment), newCar);
                _carstore.AddCar(newCar);
            }
            else
            {
                Car.CarModel = SelectedCarModel;
                Car.Comment = Comment;
                Car.RegNumber = RegNumber;
                dbClientCars.UpdateClientCar(Car);
                //_carstore.UpdateCar(Car);
            }
        }
    }
}
