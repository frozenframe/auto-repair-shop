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
        private RelayCommand openClientDataWindowCommand;
        private RelayCommand openClientDataWindowForEditCommand;
        
        private readonly ClientStore _clientStore;
        public ObservableCollection<ClientViewModel> ClientsList { get; set; }
        private ClientViewModel _selectedClient;

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
            }
        }
        public ClientsViewViewModel()
        {
            _clientStore = new ClientStore();
            ClientsList = new ObservableCollection<ClientViewModel>();
            ClientsList.Add(new ClientViewModel(new Client()
            {
                Name = "Иван",
                Surname = "Иванов",
                Lastname = "Иванович",
                Phone = "+71231234455",
                Comment = "Тестовый"
            }));
            _clientStore.ClientCreated += OnClientCreated;
        }

        private void OnClientCreated(Client client)
        {
            ClientsList.Add(new ClientViewModel(client));
        }

        public override void Dispose()
        {
            _clientStore.ClientCreated -= OnClientCreated;
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

        
    }


}
