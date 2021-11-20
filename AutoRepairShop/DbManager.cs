using System;

namespace AutoRepairShop
{
    public class DbManager
    {
        private OleDbConnection connection;


        public DbManager(string connectionString)
        {
            connection = new OleDbConnection(connectionString);
            OpenConnection();
        }

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

        public void addClient(Client client) // HashMap<String, String> parameters
        {
            object[] args = { client.Lastname, client.Name, client.Surname, client.Phone, client.Comment };
            String sqlQuery = String.Format(SqlQueries.addClient, args);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error is appearred during inserting into Client table: " + e.Message);
            }
        }

        public List<Client> getClients(Client client)
        {
            OleDbCommand command = new OleDbCommand(SqlQueries.getClients, connection);
            OleDbDataReader reader = command.ExecuteReader();

            List<Client> result = new List<Client>();
            while (reader.Read())
            {
                Client client = new Client(reader[0].ToInt32(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
                result.Items.Add(client);
            }

            reader.Close();

            return result;
        }
    }
}
