using AutoRepairShop;
using AutoRepairShop.MetaModel;
using AutoRepairShop.Model;
using AutoRepairShop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoRepairShop.ViewModel
{
    public class TechEventViewModel : ViewModelBase
    {

        private RelayCommand _openAddWorkWindowCommand;
        private RelayCommand _openClientDataWindowCommand;
        private RelayCommand _saveTechEventChangesCommand;
        DbTectEvent dbTechEvent;
        DbClient dbClient;

        private TechEventStore _techEventStore;
        private readonly ClientStore _clientStore;
        private readonly CarStore _carStore;

        private Client _client;
        private Car _clientCar;
        
        private string _fullname;
        private string _carModelAndBrand;
        private string _carNumber;
        private string _clientPhoneNumber;
        private DateTime? _techEventEndDate;
        private DateTime? _techEventStartDate;
        private string _remind;
        public TechEvent _techEvent;
        public TechEvent TechEvent 
        {   
            get
            {
                return _techEvent;
            }

            set
            {
                _techEvent = value;
                OnPropertyChanged(nameof(TechEvent));
            }
        }

        public string Remind
        {
            get
            {
                return _remind;
            }
            set
            {
                _remind = value;
                OnPropertyChanged(nameof(Remind));
            }
        }

        public DateTime? TechEventEndDate
        {
            get
            {
                return _techEventEndDate;
            }
            set
            {
                _techEventEndDate = value;
                OnPropertyChanged(nameof(TechEventEndDate));
            }
        }

        public DateTime? TechEventStartDate
        {
            get
            {
                return _techEventStartDate;
            }
            set
            {
                _techEventStartDate = value;
                OnPropertyChanged(nameof(TechEventStartDate));
            }
        }
        public string Fullname
        {
            get
            {
                return _fullname;
            }
            set
            {
                _fullname = value;
                OnPropertyChanged(nameof(Fullname));                
            }
        }

        public string CarModelAndBrand
        {
            get
            {
                return _carModelAndBrand;
            }
            set
            {
                _carModelAndBrand = value;
                OnPropertyChanged(nameof(CarModelAndBrand));
            }
        }

        public string CarNumber
        {
            get
            {
                return _carNumber ;
            }
            set
            {
                _carNumber = value;
                OnPropertyChanged(nameof(CarNumber));
            }
        }

        public string ClientPhoneNumber
        {
            get
            {
                return _clientPhoneNumber;
            }
            set
            {
                _clientPhoneNumber = value;                
                OnPropertyChanged(nameof(ClientPhoneNumber));
            }
        }






        public Client Client
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


        public Car ClientCar
        {
            get
            {
                return _clientCar;
            }
            set
            {
                _clientCar = value;
                OnPropertyChanged(nameof(ClientCar));
            }
        }

        public TechEventViewModel(TechEventStore techEventStore)
        {
            _techEventStore = techEventStore;
            _clientStore = new ClientStore();
            _carStore = new CarStore();
            _clientStore.ClientSelected += OnClientSelected;
            _carStore.CarSelected += OnCarSelected;
            Client = new Client();
            ClientCar = new Car();
            dbTechEvent = new DbTectEvent();
            
        }

        public TechEventViewModel(TechEventStore techEventStore, TechEvent techEvent) : this(techEventStore)
        {
            dbClient = new DbClient();
            Client = dbClient.GetClientById(techEvent.Car.CLientId);
            Fullname = $@"{Client.Surname} {Client.Name} {Client.Lastname}";
            ClientPhoneNumber = Client.Phone;
            TechEvent = techEvent;
            ClientCar = techEvent.Car;
            CarModelAndBrand = $@"{techEvent.Car.CarModel.CarBrand.BrandName} {techEvent.Car.CarModel.Model}";
            CarNumber = techEvent.Car.RegNumber;
            TechEventStartDate = techEvent.EventStartDate;
            TechEventEndDate = techEvent.EventEndDate;
        }

        public override void Dispose()
        {
            _clientStore.ClientSelected -= OnClientSelected;
            _carStore.CarSelected -= OnCarSelected;
            base.Dispose();
        }

        private void OnCarSelected(Car car)
        {
            ClientCar = car;
            CarNumber = car.RegNumber;
            CarModelAndBrand = $@"{car.CarModel.CarBrand.BrandName} {car.CarModel.Model}";
        }

        private void OnClientSelected(Client client)
        {
            Client = client;
            Fullname = $@"{client.Surname} {client.Name} {client.Lastname}" ;
            ClientPhoneNumber = client.Phone;
                     
        }

        public ICommand OpenAddWorkWindowCommand
        {
            get
            {
                if (_openAddWorkWindowCommand == null)
                {
                    _openAddWorkWindowCommand = new RelayCommand(OpenAddWorkWindow);
                }

                return _openAddWorkWindowCommand;
            }
        }

        private void OpenAddWorkWindow(object commandParameter)
        {
        }

        public ICommand OpenClientDataWindowCommand
        {
            get
            {
                if (_openClientDataWindowCommand == null)
                {
                    _openClientDataWindowCommand = new RelayCommand(OpenClientDataWindow);
                }
                return _openClientDataWindowCommand;
            }
        }

        private void OpenClientDataWindow(object commandParameter)
        {
            new WindowService().ShowWindow(new ClientsViewViewModel(_clientStore,_carStore), 450, 1000, "Клиенты", true);
        }


        public ICommand SaveTechEventChangesCommand
        {
            get
            {
                if (_saveTechEventChangesCommand == null)
                {
                    _saveTechEventChangesCommand = new RelayCommand(SaveTechEventChanges);
                }

                return _saveTechEventChangesCommand;
            }
        }

        private void SaveTechEventChanges(object commandParameter)
        {
            if(TechEvent.Id is null)//insert
            {
                var insertedTechEvent = dbTechEvent.InsertTechEvent(new TechEvent(ClientCar, TechEventStartDate, TechEventEndDate));
                _techEventStore.AddTechEvent(insertedTechEvent);
            }
            else//update
            {
                TechEvent.Car.Id = ClientCar.Id;
                TechEvent.EventStartDate = TechEventStartDate;
                TechEvent.EventEndDate = TechEventEndDate;
                dbTechEvent.UpdateTechEvent(TechEvent);
            }
            
        }
    }
}
