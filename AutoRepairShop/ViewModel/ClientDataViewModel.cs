using AutoRepairShop.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoRepairShop.ViewModel
{
    class ClientDataViewModel : ViewModelBase
    {
        private ClientStore _clientStore;
        private Client _client;
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
        

        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};";
        private string dbSourceFromConfig = @"C:\Users\FrozenFrame\source\repos\AutoRepairShop\CarRepair.accdb";

        private RelayCommand addClientCommand;


        

        public ClientDataViewModel(ClientStore clientStore)
        {
            _clientStore = clientStore;
        }
        public ClientDataViewModel(ClientViewModel client, ClientStore clientStore) : this(clientStore)
        {
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
                    addClientCommand = new RelayCommand(addClient);
                }
                return addClientCommand;
            }
        }

        private void addClient(object commandParameter)
        {
            DbManager dbManager = new DbManager(string.Format(connectionString, dbSourceFromConfig));
            Client newClient = new Client(Surname, Name, Lastname, Phone, Comment);
            dbManager.addClient(newClient);
            _clientStore.CreateClient(newClient);
            //List<Client> clients = dbManager.getClients();
        }
    }
}
