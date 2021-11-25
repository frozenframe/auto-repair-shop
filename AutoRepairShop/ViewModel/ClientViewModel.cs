using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.ViewModel
{
    public class ClientViewModel : ViewModelBase //wrap client class to implement inpc not in model
    {
        private readonly Client _client;

        public int? Id => _client.Id;
        public string Lastname => _client.Lastname;
        public string Name => _client.Name;
        public string Surname => _client.Surname;
        public string Phone => _client.Phone;
        public string Comment => _client.Comment;

        public ClientViewModel(Client client)
        {
            _client = client;
        }
    }
}
