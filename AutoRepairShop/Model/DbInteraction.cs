using System;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

namespace AutoRepairShop.Model
{
    public class DbInteraction
    {
        #region Fields

        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};";
        // Надо либо уже запилить нормальный конфиг файл, либо разобраться как указать для базы текущую директорию!
        //Кто первый - тот молодец!

        private string dbSourceFromConfig; // = @"C:\Users\FrozenFrame\source\repos\AutoRepairShop\CarRepair.accdb";
        //private string dbSourceFromConfig = "C:\\Users\\Wcoat\\source\\repos\\frozenframe\\auto-repair-shop\\CarRepair.accdb";

        protected OleDbConnection Connection { get; }

        #endregion // Fields

        public DbInteraction()
        {
            // Логики вычитывания из базы здесь быть не должно. Пусть за это будут отвечать дочерние от него классы.
            // Позже развяжем их.            
            dbSourceFromConfig = File.Exists(ConfigurationManager.AppSettings["SConnectionString"]) ? Properties.Settings.Default["UConnectionString"].ToString() : Properties.Settings.Default["SConnectionString"].ToString();
            
            Connection = new OleDbConnection(string.Format(connectionString, dbSourceFromConfig));
            OpenConnection();

        }

        #region Public methods
        public void CloseConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    Connection.Close();
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
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    Connection.Open();
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
