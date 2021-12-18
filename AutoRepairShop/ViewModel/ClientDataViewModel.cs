using AutoRepairShop.Model;
using AutoRepairShop.Stores;
using System.Windows.Input;

namespace AutoRepairShop.ViewModel
{
    class ClientDataViewModel : ViewModelBase
    {
        private ClientStore _clientStore;
        private Client _client;
        DbClient dbClient;
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
        //private string _lastname;
        //public string Lastname
        //{
        //    get
        //    {
        //        return _lastname;
        //    }
        //    set
        //    {
        //        _lastname = value;
        //        OnPropertyChanged(nameof(Lastname));
        //    }
        //}

        //private string _name;
        //public string Name
        //{
        //    get
        //    {
        //        return _name;
        //    }
        //    set
        //    {
        //        _name = value;
        //        OnPropertyChanged(nameof(Name));
                    
        //    }

        //}

        //private string _surname;
        //public string Surname 
        //{
        //    get 
        //    {
        //        return _surname;
        //    }
        //    set 
        //    {
        //        _surname = value;
        //        OnPropertyChanged(nameof(Surname));
        //    }
        //}

        //private string _phone;
        //public string Phone
        //{
        //    get
        //    {
        //        return _phone;
        //    }
        //    set
        //    {
        //        _phone = value;
        //        OnPropertyChanged(nameof(Phone));
        //    }
        //}

        //private string _comment;
        //public string Comment
        //{
        //    get
        //    {
        //        return _comment;
        //    }
        //    set
        //    {
        //        _comment = value;
        //        OnPropertyChanged(nameof(Comment));
        //    }
        //}
        

        //private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};";
        //private string dbSourceFromConfig = @"C:\Users\FrozenFrame\source\repos\AutoRepairShop\CarRepair.accdb";

        private RelayCommand addClientCommand;


        

        public ClientDataViewModel(ClientStore clientStore)
        {
            _clientStore = clientStore;
            Client = new Client();
            dbClient = new DbClient();            
        }
        public ClientDataViewModel(ClientViewModel client, ClientStore clientStore) : this(clientStore)
        {
            _client = client.Client;
            //Comment = client.Comment;
            //Surname = client.Surname;
            //Lastname = client.Lastname;
            //Name = client.Name;
            //Phone = client.Phone;
        }

        public ICommand AddClientCommand
        {
            get
            {
                if (addClientCommand == null)
                {
                    addClientCommand = new RelayCommand(AddOrUpdateClient);
                }
                return addClientCommand;
            }
        }

        private void AddOrUpdateClient(object commandParameter)
        {
            if(_client.Id is null)
            {                
                Client newClient = new Client(_client.Lastname, _client.Name, _client.Surname, _client.Phone, _client.Comment);
                newClient = dbClient.AddClient(newClient);                
                _clientStore.CreateClient(newClient);
            }
            else
            {                
                Client newClient = new Client(_client.Id,_client.Lastname, _client.Name, _client.Surname, _client.Phone, _client.Comment);
                dbClient.UpdateClient(newClient);
            }
            
            //List<Client> clients = dbManager.getClients();
        }
    }
}
