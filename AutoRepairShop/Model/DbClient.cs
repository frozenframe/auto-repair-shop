﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace AutoRepairShop.Model
{
    public class DbClient : DbManager
    {
        #region Public methods

        public List<Client> GetClient()
        {
            return getClients(SqlQueries.getAllClients);
        }

        public void DeleteClient(Client client)
        {
            String sqlQuery = String.Format(SqlQueries.deleteClient, client.Id);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Log.Error(String.Format("An error is appearred during deleting from Client table: {0}", e.Message));
            }
        }

        public Client AddClient(Client client)
        {
            object[] insertArgs = { client.Lastname, client.Name, client.Surname, client.Phone, client.Comment };
            String insertQuery = String.Format(SqlQueries.addClient, insertArgs);

            try
            {
                int id = insertRecordIntoDb(insertQuery) ?? 0;
                if (id != 0)
                {
                    return new Client(id, client.Lastname, client.Name, client.Surname, client.Phone, client.Comment);
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(
                    String.Format("An error is appearred during adding client {0} {1} {2} {3}. Error: {4}",
                    client.Lastname,
                    client.Name,
                    client.Surname,
                    client.Phone,
                    e.Message)
                    );
            }
            return null;
        }

        public void UpdateClient(Client client)
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
                Logger.Log.Error(String.Format("An error is appearred during updating Client table: {0}", e.Message));
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

        #endregion Public methods

        #region Private methods

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

        #endregion Private methods
    }
}
