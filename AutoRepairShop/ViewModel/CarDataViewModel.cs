using AutoRepairShop;
using AutoRepairShop.Model;
using AutoRepairShop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoRepairShop.ViewModel
{
    public class CarDataViewModel : ViewModelBase
    {
        private CarStore _carstore;
        private Car _car;
        private CarBrand _selectedCarBrand;
        private CarModel _selectedCarModel;
        private ClientViewModel _client;
        //private readonly string _clientName;
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
        DbInteraction dbInteraction;

        public ObservableCollection<CarBrand> CarBrands { get; set; }
        public ObservableCollection<CarModel> CarModels { get; set; }

        public CarBrand SelectedCarBrand
        {
            get
            {
                return _selectedCarBrand;
            }
            set
            {
                _selectedCarBrand = value;
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

        public CarDataViewModel(CarStore carStore, ClientViewModel client)
        {
            _carstore = carStore;
            dbInteraction = new DbInteraction();
            Car = new Car();
            CarBrands = new ObservableCollection<CarBrand>();
            foreach (var row in dbInteraction.GetBrands())
            {
                CarBrands.Add(row);
            }
            CarModels = new ObservableCollection<CarModel>();
            Client = client;
        }

        public CarDataViewModel(Car car, CarStore carStore,ClientViewModel client) : this(carStore,client)
        {
            Car = car;            
        }


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
        //    //TODO GET ALL car models for selected brand
        //}


        private void FillCarModelComboBox( CarBrand selectedCarBrand)
        {
            CarModels.Clear();
            var carModelWithSelectedBrand = dbInteraction.GetModels(SelectedCarBrand);
            foreach(var row in carModelWithSelectedBrand)
            {
                CarModels.Add(row);
            }
        }

        private RelayCommand addCarToClientCommand;

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
                var newCar = new Car(1, SelectedCarModel, "A999AE", "norm");
                _carstore.AddCar(newCar);
                //TODO добавление в базу записи о новой машине клиента
            }
            else
            {
                //TODO изменение выбранной машины клиента
            }
        }
    }
}
