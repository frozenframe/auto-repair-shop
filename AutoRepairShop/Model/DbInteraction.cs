using System;
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
            //dbManager = new DbManager(string.Format(connectionString, dbSourceFromConfig));
            connection = new OleDbConnection(string.Format(connectionString, dbSourceFromConfig));
            OpenConnection();

        }

        #region Public methods
        public void CloseConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    connection.Close();
                }
                //TODO: Все тоже самое, что и для Connection.Open();
                catch (Exception ex)
                {
                    Logger.Log.Error("An error was occured during processing query: " + ex.Message);
                    throw;
                }
            }
        }

        #endregion Public methods

        #region Private methods

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

        #endregion Private methods
    }
}
