using AutoRepairShop.Utils;
using AutoRepairShop.ViewModel;
using System;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace AutoRepairShop.Model
{
    public class DbInteraction
    {
        #region Fields

        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};";

        private string dbPathString = "";
        protected OleDbConnection Connection { get; }

        #endregion Fields

        public DbInteraction()
        {
            
            //// Логики вычитывания из базы здесь быть не должно. Пусть за это будут отвечать дочерние от него классы.
            //// Позже развяжем их.            
            SettingsManager settingsManager = SettingsManager.getSettingsManager();
            if (!settingsManager.Settings.TryGetValue(Constants.CONNECTION_STRING, out dbPathString))
            {
                findDatabase("В файле конфигурации не указан путь до базы данных");
            }
            settingsManager.Settings.TryGetValue(Constants.CONNECTION_STRING, out dbPathString);
            if (!File.Exists(dbPathString))
            {
                findDatabase("Файл базы данных не существует по указанному пути: " + dbPathString);
            }

            Connection = new OleDbConnection(string.Format(connectionString, dbPathString));
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

        private void findDatabase(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            new WindowService().ShowWindow(new SettingsViewModel(), 450, 820, "Общие настройки", true);
        }

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
