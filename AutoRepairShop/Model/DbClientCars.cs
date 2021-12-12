using AutoRepairShop.MetaModel;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace AutoRepairShop.Model
{
    public class DbClientCars : DbManager
    {
        #region Public methods
        public Car AddClientCar(Client client, Car car)
        {
            object[] insertArgs = { (int)client.Id, car.CarModel.Id, car.RegNumber, car.Comment };
            string insertQuery = string.Format(SqlQueries.addClientCar, insertArgs);
            try
            {
                int id = InsertRecordIntoDb(insertQuery) ?? 0;
                if (id != 0)
                {
                    return new Car(id, (int)client.Id, car.CarModel, car.RegNumber, car.Comment);
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(
                    string.Format("An error is appearred during adding car ({0}/{1}) for client {2} {3}. Error: {4}",
                    car.CarModel.CarBrand.BrandName,
                    car.CarModel.Model,
                    client.Lastname,
                    client.Name,
                    e.Message)
                    );

            }
            return null;
        }

        public void UpdateClientCar(Car car)
        {
            //"update Client_Cars set car_model_id='{0}', reg_number='{1}', comment='{2}' where id={3}";
            object[] args = { car.CarModel.Id, car.RegNumber, car.Comment, car.Id };
            string sqlQuery = string.Format(SqlQueries.updateClientCar, args);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error is appearred during updating ClientCar table: " + e.Message);
            }
        }

        public List<Car> GetClientCars(int clientId)
        {
            string selectQuery = string.Format(SqlQueries.getAllClientCars, (int)clientId);
            OleDbCommand command = new OleDbCommand(selectQuery, Connection);
            OleDbDataReader reader = command.ExecuteReader();
            List<Car> result = new List<Car>();
            while (reader.Read())
            {
                Car car = new Car((int)reader[0], clientId, new CarModel((int)reader[4], new CarBrand((int)reader[7], reader[8].ToString()), reader[6].ToString()), reader[2].ToString(), reader[3].ToString());
                result.Add(car);
            }
            return result;
        }


        public void DeleteClientCar(Car car)
        {
            string sqlQuery = string.Format(SqlQueries.deleteClientCar, car.Id);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error is appearred during deleting from Client table: " + e.Message);
            }
        }
        #endregion Public methods
    }
}
