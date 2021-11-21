using System;
using System.Collections.Generic;
using System.Data.OleDb;

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

        public Client addClient(Client client) // HashMap<String, String> parameters
        {
            object[] insertArgs = { client.Lastname, client.Name, client.Surname, client.Phone, client.Comment };
            String insertQuery = String.Format(SqlQueries.addClient, insertArgs);
            try
            {
                OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection);
                insertCommand.ExecuteNonQuery();
                return getClientByFioPhone(client);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error is appearred during inserting into Client table: " + e.Message);
                return null;
            }
        }
        public Client getClientByFioPhone(Client client)
        {
            object[] selectArgs = { client.Lastname, client.Name, client.Surname, client.Phone };
            String selectQuery = String.Format(SqlQueries.getClientByFioPhone, selectArgs);

            return getClients(selectQuery).ToArray()[0];
        }

        public List<Client> getClients()
        {
            return getClients(SqlQueries.getAllClients);
        }

        private List<Client> getClients(String sqlQuery)
        {
            OleDbCommand command = new OleDbCommand(sqlQuery, connection);
            OleDbDataReader reader = command.ExecuteReader();

            List<Client> result = new List<Client>();
            while (reader.Read())
            {
                Client client = new Client((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
                result.Add(client);
            }
            reader.Close();

            return result;
        }

        public void updateClient(Client client)
        {
            object[] args = { client.Lastname, client.Name, client.Surname, client.Phone, client.Comment, client.Id };
            String sqlQuery = String.Format(SqlQueries.updateClient, args);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error is appearred during updating Client table: " + e.Message);
            }
        }

        public void deleteClient(Client client)
        {
            String sqlQuery = String.Format(SqlQueries.deleteClient, client.Id);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error is appearred during deleting from Client table: " + e.Message);
            }
        }
    }
}
