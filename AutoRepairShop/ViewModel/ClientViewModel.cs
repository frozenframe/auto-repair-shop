using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.ViewModel
{
    public class ClientViewModel : ViewModelBase //wrap client class to implement inpc not in model
    {
        private Client _client;
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



        public int? Id => _client.Id;
        public string Lastname => _client.Lastname;
        public string Name => _client.Name;
        public string Surname => _client.Surname;
        public string Phone => _client.Phone;
        public string Comment => _client.Comment;


        //public int? Id { get; }
        //private string _lastname;
        //public string Lastname
        //{
        //    get
        //    {
        //        return _lastname;
        //    }
        //    set
        //    {
        //        _lastname = value; OnPropertyChanged(nameof(Lastname));
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

        public ClientViewModel(Client client)
        {
            _client = client;
        }
        
    }
}
