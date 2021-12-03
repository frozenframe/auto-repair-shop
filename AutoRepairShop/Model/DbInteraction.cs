using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace AutoRepairShop.Model
{
    public class DbInteraction
    {
        #region Deprecated
        protected DbManager dbManager { get; }
        #endregion //Deprecated

        #region Fields

        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};";
        // Надо либо уже запилить нормальный конфиг файл, либо разобраться как указать для базы текущую директорию!
        //Кто первый - тот молодец!
        //private string dbSourceFromConfig = @"C:\Users\FrozenFrame\source\repos\AutoRepairShop\CarRepair.accdb";
        private string dbSourceFromConfig = "C:\\Users\\Wcoat\\source\\repos\\frozenframe\\auto-repair-shop\\CarRepair.accdb";

        protected OleDbConnection connection { get; }

        #endregion // Fields

        public DbInteraction()
        {   // Класс DbManager становится по сути нам не нужен. Это класс будет создавать и хранить соединение до БД.
            // Логики вычитывания из базы здесь быть не должно. Пусть за это будут отвечать дочерние от него классы.
            // Позже развяжем их.
            dbManager = new DbManager(string.Format(connectionString, dbSourceFromConfig));
            connection = new OleDbConnection(string.Format(connectionString, dbSourceFromConfig));
            OpenConnection();

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

        #region Private section

        private void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                // TODO: Нужно отлавливать только специфичный exception
                catch (Exception ex)
                {
                    Logger.Log.Error("An error was occured during openning connection to DB: " + ex.Message);
                    throw;
                }
            }
        }

        #endregion // Private section
    }
}
