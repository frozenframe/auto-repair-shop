using AutoRepairShop.MetaModel;
using System;

namespace AutoRepairShop.Model
{
    public class DbClientCars : DbManager
    {
        #region Public methods
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

        #endregion Public methods
    }
}
