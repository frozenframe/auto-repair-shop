using System;

namespace AutoRepairShop.Stores
{
    public class ClientStore
    {
        public event Action<Client> ClientCreated;
        public event Action<Client> ClientSelected;
        public void CreateClient(Client client)
        {
            ClientCreated?.Invoke(client);
        }

        public void SelectClient(Client client)
        {
            ClientSelected?.Invoke(client);
        }
    }
}
