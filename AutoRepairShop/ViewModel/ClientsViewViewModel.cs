﻿using AutoRepairShop;
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
        DbInteraction dbInteraction;
        private RelayCommand openClientDataWindowCommand;
        private RelayCommand openClientDataWindowForEditCommand;
        private RelayCommand deleteClientCommand;
        private RelayCommand openCarDataWindowForEditCommand;
        private RelayCommand openCarDataWindowCommand;

        private readonly ClientStore _clientStore;
        private readonly CarStore _carStore;
        private bool _canInteractWithCarFunctions = false;
        public ObservableCollection<ClientViewModel> ClientsList { get; set; }
        private ClientViewModel _selectedClient;
        public ObservableCollection<Car> ClientsCarList { get; set; }
        private Car _selectedCar;


        public bool CanInteractWithCarFunctions
        {
            get
            {
                return _canInteractWithCarFunctions;
            }
            set
            {
                _canInteractWithCarFunctions = value;
                OnPropertyChanged(nameof(CanInteractWithCarFunctions));
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
                FillCarsGrid(_selectedClient);
                CanInteractWithCarFunctions = true;
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
            var cars = dbInteraction.GetClientCars(selectedClient.Id.Value);
            foreach (var row in cars)
            {
                ClientsCarList.Add(new Car(row.CLientId, row.CarModel, row.RegNumber, row.Comment));
            }
        }


        public ClientsViewViewModel()
        {
            _clientStore = new ClientStore();
            _carStore = new CarStore();
            dbInteraction = new DbInteraction();
            var clientsFromDb = dbInteraction.GetClient();            
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
            ClientsCarList.Add(new Car(car.CLientId, car.CarModel, car.RegNumber, car.Comment));
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
                    openClientDataWindowForEditCommand = new RelayCommand(OpenClientDataWindowForEdit);
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
                    deleteClientCommand = new RelayCommand(DeleteClient);
                }

                return deleteClientCommand;
            }
        }

        private void DeleteClient(object commandParameter)
        {
            dbInteraction.DeleteClient(_selectedClient.Client);            
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
            return CanInteractWithCarFunctions;
        }



        public ICommand OpenCarDataWindowForEditCommand
        {
            get
            {
                if (openCarDataWindowForEditCommand == null)
                {
                    openCarDataWindowForEditCommand = new RelayCommand(OpenCarDataWindowForEdit,CheckClientSelection);
                }

                return openCarDataWindowForEditCommand;
            }
        }

        private void OpenCarDataWindowForEdit(object commandParameter)
        {
            new WindowService().ShowWindow(new CarDataViewModel(_selectedCar, _carStore, SelectedClient));
        }
    }
}
