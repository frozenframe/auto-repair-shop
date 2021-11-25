using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Stores
{
    public class ClientStore
    {
        public event Action<Client> ClientCreated;
        public void CreateClient(Client client)
        {
            ClientCreated?.Invoke(client);
        }
    }
}
