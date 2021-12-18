using AutoRepairShop;
using AutoRepairShop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static AutoRepairShop.Utils.Constants;

namespace AutoRepairShop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private RelayCommand openClientDataWindowCommand;
        private RelayCommand openTreeViewWindowCommand;

        //public ObservableCollection<ClientViewModel> ClientsList { get; set; }
        //public Dictionary<int, Client> claientsMap = new Dictionary<int, Client>;
        //private readonly ClientStore _clientStore;
        //private Client _selectedClient;

        //public Client SelectedClient
        //{
        //    get
        //    {
        //        return _selectedClient;
        //    }
        //    set
        //    {
        //        _selectedClient = value;
        //        OnPropertyChanged(nameof(SelectedClient));
        //    }
        //}

        public MainViewModel()
        {
            //_clientStore = new ClientStore();
            //ClientsList = new ObservableCollection<ClientViewModel>();
            //_clientStore.ClientCreated += OnClientCreated;
        }


        //public MainViewModel(ClientStore clientStore)
        //{
        //    //_clientStore = clientStore;
        //    //ClientsList = new ObservableCollection<ClientViewModel>();            
        //    //ClientsList.Add(new ClientViewModel(new Client()
        //    //{
        //    //    Name = "Иван",
        //    //    Surname = "Иванов",
        //    //    Lastname = "Иванович",
        //    //    Phone = "+71231234455",
        //    //    Comment = "Тестовый"
        //    //}));

        //    //_clientStore.ClientCreated += OnClientCreated;

        //    //ClientsList = new ObservableCollection<Client>
        //    //{
        //    //    new Client("Иванов", "Иван", "Иванович", "+71231234455", "Тестовый"),
        //    //    new Client("Иванов", "Иван", "Иванович", "+71231234455", "Тестовый1"),
        //    //    new Client("Иванов", "Иван", "Иванович", "+71231234455", "Тестовый2"),
        //    //    new Client("Иванов", "Иван", "Иванович", "+71231234455", "Тестовый3"),
        //    //};
        //}

        //private void OnClientCreated(Client client)
        //{
        //    ClientsList.Add(new ClientViewModel(client));
        //}

        //public override void Dispose()
        //{
        //    _clientStore.ClientCreated -= OnClientCreated;
        //    base.Dispose();
        //}


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
            new WindowService().ShowWindow(new ClientsViewViewModel());
        }

        public ICommand OpenTreeViewWindowCommand
        {
            get
            {
                if (openTreeViewWindowCommand == null)
                {
                    openTreeViewWindowCommand = new RelayCommand(OpenTreeViewWindow);
                }
                return openTreeViewWindowCommand;
            }
        }


        private void OpenTreeViewWindow(object commandParameter)
        {
            new WindowService().ShowWindow(new WorkTypeTreeViewModel(WorkTypeViewMode.MANAGEMENT), 450, 800, "Список типов работ");

        }


    }
}
