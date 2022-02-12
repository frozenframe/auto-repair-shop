using AutoRepairShop.Model;
using AutoRepairShop.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace AutoRepairShop.ViewModel
{
    public class ClientsViewViewModel : ViewModelBase
    {
        private DbClient dbClient;
        private DbClientCars dbClientCars;

        private RelayCommand openClientDataWindowCommand;
        private RelayCommand openClientDataWindowForEditCommand;
        private RelayCommand deleteClientCommand;
        private RelayCommand openCarDataWindowForEditCommand;
        private RelayCommand openCarDataWindowCommand;

        private readonly ClientStore _clientStore;
        private readonly CarStore _carStore;        
        public ObservableCollection<ClientViewModel> ClientsList { get; set; }
        private ClientViewModel _selectedClient;
        public ObservableCollection<Car> ClientsCarList { get; set; }
        private Car _selectedCar;
        public ICollectionView ClientListView { get; private set; }
        private string _surnameFilterText;


        public string SurnameFilterText
        {
            get
            {
                return _surnameFilterText;
            }
            set
            {
                _surnameFilterText = value;
                OnPropertyChanged(nameof(SurnameFilterText));                
                ClientListView.Refresh();
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
            


            ClientListView = CollectionViewSource.GetDefaultView(ClientsList);
            ClientListView.Filter = SurnameFilter;
        }


        public bool SurnameFilter(object obj)
        {
            if (!string.IsNullOrWhiteSpace(SurnameFilterText))
            {
                var client = obj as ClientViewModel;
                return client.Surname.Contains(SurnameFilterText);
            }
            return true;
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
            new WindowService().ShowWindow(new ClientDataViewModel(_clientStore), 450, 800, "Добавление клиента");
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
            new WindowService().ShowWindow(new ClientDataViewModel(SelectedClient, _clientStore), 450, 800, "Редактирование клиента");
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
            var confirmation = MessageBox.Show(
                    "Выбранная машина будет удалена из списка автомобилей выбранного клиента?",
                    "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );

            if (confirmation == DialogResult.Yes)
            {
                foreach (var car in ClientsCarList)
                {
                    dbClientCars.DeleteClientCar(car);
                }
                ClientsCarList.Clear();
                dbClient.DeleteClient(_selectedClient.Client);
                ClientsList.Remove(_selectedClient);
            }
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

            new WindowService().ShowWindow(new CarDataViewModel(_carStore, SelectedClient), 450, 800, "Добавление автомобиля клиента");
        }

        private bool CheckClientSelection(object param)
        {
            return SelectedClient is null ? false : true;
        }

        private bool CheckCarSelection(object param)
        {
            return SelectedCar is null? false : true;            
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

            new WindowService().ShowWindow(new CarDataViewModel(SelectedCar, _carStore, SelectedClient), 450, 800, "Редактирование автомобиля клиента");
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
            var confirmation = MessageBox.Show(
                    "Выбранная машина будет удалена из списка автомобилей выбранного клиента?",
                    "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );

            if (confirmation == DialogResult.Yes)
            {
                dbClientCars.DeleteClientCar(SelectedCar);
                ClientsCarList.Remove(SelectedCar);
            }
        }
    }
}
