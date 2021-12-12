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
    public class ClientsViewViewModel : ViewModelBase
    {
        //private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};";
        //private string dbSourceFromConfig = @"C:\Users\FrozenFrame\source\repos\AutoRepairShop\CarRepair.accdb";        
        DbClient dbClient;
        DbClientCars dbClientCars;
        private RelayCommand openClientDataWindowCommand;
        private RelayCommand openClientDataWindowForEditCommand;
        private RelayCommand deleteClientCommand;
        private RelayCommand openCarDataWindowForEditCommand;
        private RelayCommand openCarDataWindowCommand;

        private readonly ClientStore _clientStore;
        private readonly CarStore _carStore;
        private bool _isClientSelected = false;
        private bool _isCarSelected = false;
        public ObservableCollection<ClientViewModel> ClientsList { get; set; }
        private ClientViewModel _selectedClient;
        public ObservableCollection<Car> ClientsCarList { get; set; }
        private Car _selectedCar;


        public bool IsClientSelected
        {
            get
            {
                return _isClientSelected;
            }
            set
            {
                _isClientSelected = value;
                OnPropertyChanged(nameof(IsClientSelected));
            }
        }

        public bool IsCarSelected
        {
            get
            {
                return _isCarSelected;
            }
            set
            {
                _isCarSelected = value;
                OnPropertyChanged(nameof(IsCarSelected));
            }
        }


        public ClientViewModel SelectedClient
        {
            get
            {
                return _selectedClient;
            }
            set
            {
                _selectedClient = value;                             
                OnPropertyChanged(nameof(SelectedClient));
                if(value != null)
                {
                    FillCarsGrid(_selectedClient);                    
                }                
                IsClientSelected = false;
            }
        }


        public Car SelectedCar
        {
            get
            {
                return _selectedCar;
            }
            set
            {
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
                IsCarSelected = true;
            }
        }


        private void FillCarsGrid(ClientViewModel selectedClient)
        {
            ClientsCarList.Clear();
            var cars = dbClientCars.GetClientCars(selectedClient.Id.Value);
            foreach (var row in cars)
            {
                ClientsCarList.Add(new Car(row.Id,row.CLientId, row.CarModel, row.RegNumber, row.Comment));
            }
        }


        public ClientsViewViewModel()
        {
            _clientStore = new ClientStore();
            _carStore = new CarStore();
            dbClient = new DbClient();
            dbClientCars = new DbClientCars();
            var clientsFromDb = dbClient.GetClient();
            ClientsList = new ObservableCollection<ClientViewModel>();
            ClientsCarList = new ObservableCollection<Car>();
            foreach (var row in clientsFromDb)
            {
                var newClient = new ClientViewModel(row);
                ClientsList.Add(newClient);
            }
            _clientStore.ClientCreated += OnClientCreated;
            _carStore.CarAdded += OnCarAdded;
        }

        private void OnCarAdded(Car car)
        {
            ClientsCarList.Add(new Car(car.Id,car.CLientId, car.CarModel, car.RegNumber, car.Comment));
        }

        private void OnClientCreated(Client client)
        {
            ClientsList.Add(new ClientViewModel(client));
        }

        public override void Dispose()
        {
            _clientStore.ClientCreated -= OnClientCreated;
            _carStore.CarAdded -= OnCarAdded;
            base.Dispose();
        }


        public ICommand OpenClientDataWindowCommand
        {
            get
            {
                if (openClientDataWindowCommand == null)
                {
                    openClientDataWindowCommand = new RelayCommand(OpenClientDataWindow);
                }
                return openClientDataWindowCommand;
            }
        }

        private void OpenClientDataWindow(object commandParameter)
        {
            new WindowService().ShowWindow(new ClientDataViewModel(_clientStore));
        }


        public ICommand OpenClientDataWindowForEditCommand
        {
            get
            {
                if (openClientDataWindowForEditCommand == null)
                {
                    openClientDataWindowForEditCommand = new RelayCommand(OpenClientDataWindowForEdit,CheckClientSelection);
                }
                return openClientDataWindowForEditCommand;
            }
        }

        private void OpenClientDataWindowForEdit(object commandParameter)
        {           
            new WindowService().ShowWindow(new ClientDataViewModel(SelectedClient,_clientStore));
        }
                

        public ICommand DeleteClientCommand
        {
            get
            {
                if (deleteClientCommand == null)
                {
                    deleteClientCommand = new RelayCommand(DeleteClient, CheckClientSelection);
                }

                return deleteClientCommand;
            }
        }


        private void DeleteClient(object commandParameter)
        {
            foreach (var car in ClientsCarList)
            {
                dbClientCars.DeleteClientCar(car);
            }
            ClientsCarList.Clear();
            dbClient.DeleteClient(_selectedClient.Client);
            ClientsList.Remove(_selectedClient);
        }
        

        public ICommand OpenCarDataWindowCommand
        {
            get
            {
                if (openCarDataWindowCommand == null)
                {
                    openCarDataWindowCommand = new RelayCommand(OpenCarDataWindow,CheckClientSelection);
                    
                }
                return openCarDataWindowCommand;
            }
        }

        private void OpenCarDataWindow(object commandParameter)
        {
            
            new WindowService().ShowWindow(new CarDataViewModel(_carStore,SelectedClient));
        }

        private bool CheckClientSelection(object param)
        {
            return SelectedClient is null ? false : true;
        }

        private bool CheckCarSelection(object param)
        {
            return SelectedCar is null? false : true;
            //return IsCarSelected;
        }



        public ICommand OpenCarDataWindowForEditCommand
        {
            get
            {
                if (openCarDataWindowForEditCommand == null)
                {
                    openCarDataWindowForEditCommand = new RelayCommand(OpenCarDataWindowForEdit,CheckCarSelection);
                }

                return openCarDataWindowForEditCommand;
            }
        }

        private void OpenCarDataWindowForEdit(object commandParameter)
        {
            new WindowService().ShowWindow(new CarDataViewModel(SelectedCar, _carStore, SelectedClient));
        }

        private RelayCommand deleteCarCommand;

        public ICommand DeleteCarCommand
        {
            get
            {
                if (deleteCarCommand == null)
                {
                    deleteCarCommand = new RelayCommand(DeleteCar,CheckCarSelection);
                }

                return deleteCarCommand;
            }
        }

        private void DeleteCar(object commandParameter)
        {
            dbClientCars.DeleteClientCar(SelectedCar);
            ClientsCarList.Remove(SelectedCar);
        }
    }
}
