using AutoRepairShop.Model;
using AutoRepairShop.Stores;
using System.Windows.Data;
using System.Windows.Input;

namespace AutoRepairShop.ViewModel
{
    class ClientDataViewModel : ViewModelBase
    {
        private ClientStore _clientStore;
        private Client _client;
        DbClient dbClient;
        private RelayCommand addClientCommand;

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

        private int? _id;
        public int? Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _lastname;
        public string Lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));

            }

        }

        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string _phone;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
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
                OnPropertyChanged(nameof(Comment));
            }
        }



        public ClientDataViewModel(ClientStore clientStore)
        {
            _clientStore = clientStore;
            //Client = new Client();
            dbClient = new DbClient();            
        }
        public ClientDataViewModel(ClientViewModel client, ClientStore clientStore) : this(clientStore)
        {
            Client = client.Client;
            Id = (int)client.Id;
            Comment = client.Comment;
            Surname = client.Surname;
            Lastname = client.Lastname;
            Name = client.Name;
            Phone = client.Phone;
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
            if (Id is null)
            {
                Client newClient = new Client(Lastname, Name, Surname, Phone, Comment);
                newClient = dbClient.InsertClient(newClient);
                _clientStore.CreateClient(newClient);
            }
            else
            {
                Client updatedClient = new Client(Id, Lastname, Name, Surname, Phone, Comment);                
                dbClient.UpdateClient(updatedClient);
                Client.Surname = Surname;
                Client.Name = Name;
                Client.Lastname = Lastname;
                Client.Phone = Phone;
                Client.Comment = Comment;
            }
        }
    }
}
