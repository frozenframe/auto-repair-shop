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

        ///--------------------- Client Part --------------------------------
        public Client addClient(Client client)
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


        ///--------------------- Client Cars Part --------------------------------

        public Car addClientCar(Client client, Car car)
        {
            object[] insertArgs = { (int)client.Id, car.CarModel.Id, car.RegNumber, car.Comment };
            String insertQuery = String.Format(SqlQueries.addClientCar, insertArgs);
            try
            {
                int id = insertRecordIntoDb(insertQuery) ?? 0;
                if (id != 0)
                {
                    return new Car(id, (int)client.Id, car.CarModel, car.RegNumber, car.Comment);
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(
                    String.Format("An error is appearred during adding car ({0}/{1}) for client {2} {3}. Error: {4}",
                    car.CarModel.CarBrand.BrandName,
                    car.CarModel.Model,
                    client.Lastname,
                    client.Name,
                    e.Message)
                    );

            }
            return null;
        }

 
        ///--------------------- Car Brands Part --------------------------------
        public Dictionary<int, CarBrand> getCarBrands()
        {
            OleDbCommand command = new OleDbCommand(SqlQueries.getAllCarBrands, connection);
            OleDbDataReader reader = command.ExecuteReader();

            Dictionary<int, CarBrand> result = new Dictionary<int, CarBrand>();
            while (reader.Read())
            {
                CarBrand carBrand = new CarBrand((int)reader[0], reader[1].ToString());
                result.Add((int)carBrand.Id, carBrand);
            }
            reader.Close();

            return result;
        }


        ///--------------------- Car Models Part --------------------------------
        public List<CarModel> getCarModels(CarBrand carBrand)
        {
            String selectQuery = String.Format(SqlQueries.getAllCarModelsByBrandId, (int)carBrand.Id);
            OleDbCommand command = new OleDbCommand(selectQuery, connection);
            List<CarModel> result = new List<CarModel>();

            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    CarModel carModel = new CarModel((int)reader[0], carBrand, reader[2].ToString());
                    result.Add(carModel);
                }
            }

            return result;
        }

        public SortedList<int, WorkType> getAllWorkTypes()
        {
            OleDbCommand command = new OleDbCommand(SqlQueries.getAllWorkTypes, connection);
            SortedList<int, WorkType> result = new SortedList<int, WorkType>();

            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    WorkType workType = new WorkType((int)reader[0], (int)reader[1], reader[2].ToString());
                    result.Add((int)reader[0], workType); //!List может быть и обычным! на этом этапе не нужен порядок записей. Позже упорядочим
                }
            }
            return result;
        }

        ///---------------------------------------- PRIVATE METHODS SECTION -------------------------
        private int? insertRecordIntoDb(String insertQuery)
        {
            int? newRecodeId = null;
            OleDbTransaction transaction = null;
            try
            {
                transaction = connection.BeginTransaction();
                OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection);
                insertCommand.Transaction = transaction;
                int rows = insertCommand.ExecuteNonQuery();
                if (rows == 1)
                {
                    newRecodeId = getIdInsertedRecord(transaction);
                }
                else
                {
                    throw new Exception("Insert command returns that no records were inserted.");
                }
                transaction.Commit();
                return newRecodeId;
            }
            catch (Exception e)
            {
                Logger.Log.Error("An error is appearred during inserting record: " + e.Message);
                try
                {
                    // Attempt to roll back the transaction.
                    transaction.Rollback();
                }
                catch
                {
                    // Do nothing here; transaction is not active.
                }
                throw e;
            }
        }

        private int getIdInsertedRecord(OleDbTransaction transaction)
        {
            OleDbCommand selectCommand = new OleDbCommand("SELECT @@IDENTITY", connection);
            selectCommand.Transaction = transaction;
            return (int)selectCommand.ExecuteScalar();
        }


    }
}
