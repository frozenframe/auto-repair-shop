using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Model
{
    public class DbInteraction
    {
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};";
        private string dbSourceFromConfig = @"C:\Users\FrozenFrame\source\repos\AutoRepairShop\CarRepair.accdb";
        private DbManager dbManager { get; set; }

        public DbInteraction()
        {
            dbManager = new DbManager(string.Format(connectionString, dbSourceFromConfig));
        }
        public List<Client> GetClient()
        {
            return dbManager.getClients();
        }
        public void DeleteClient(Client client)
        {
            dbManager.deleteClient(client);
        }

        public void AddClient(Client client)
        {
            dbManager.addClient(client);
        }

        public void UpdateClient(Client client)
        {
            dbManager.updateClient(client);
        }
    }
}
