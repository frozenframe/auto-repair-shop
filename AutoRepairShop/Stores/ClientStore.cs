using System;

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
