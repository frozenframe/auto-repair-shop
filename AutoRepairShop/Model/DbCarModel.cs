using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace AutoRepairShop.Model
{
    public class DbCarModel : DbInteraction
    {
        #region Public methods
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

        #endregion // Public methods
    }
}
