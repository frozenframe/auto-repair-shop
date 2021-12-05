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
            return dbManager.GetClients();
        }
        public void DeleteClient(Client client)
        {
            dbManager.DeleteClient(client);
        }

        public void AddClient(Client client)
        {
            dbManager.AddClient(client);
        }

        public void UpdateClient(Client client)
        {
            dbManager.UpdateClient(client);
        }

        public List<Car> GetClientCars(int clientId)
        {
            return dbManager.GetClientCars(clientId);
            
        }

        public List<CarBrand> GetBrands()
        {
            var carBrands = new List<CarBrand>();
            foreach(var row in dbManager.GetCarBrands())
            {
                carBrands.Add(row.Value);
            }
            return carBrands;
        }

        public List<CarModel> GetModels(CarBrand carBrand)
        {
           return dbManager.GetCarModels(carBrand);
        }

        public Car AddClientCar(Client client,Car car)
        {
           return dbManager.AddClientCar(client, car);
            //dbManager.;
        }
    }
}
