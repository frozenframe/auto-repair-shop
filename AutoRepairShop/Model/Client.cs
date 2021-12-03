using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoRepairShop
{
    public class Client : INotifyPropertyChanged
    {
        public int? Id { get; }
        private string _lastname;
        public string Lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value; OnPropertyChanged(nameof(Lastname));
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
        
        public Client()
        {

        }

        public Client(int? id, string lastname, string name, string surname, string phone, string comment)
        {
            Id = id;
            Lastname = lastname;
            Name = name;
            Surname = surname;
            Phone = phone;
            Comment = comment;
        }

        public Client(string lastname, string name, string surname, string phone, string comment) :
            this(null, lastname, name, surname, phone, comment)
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}