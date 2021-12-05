using AutoRepairShop.MetaModel;
using System.Collections.Generic;
using System.Data.OleDb;

namespace AutoRepairShop.Model
{
    public class DbCarBrand : DbInteraction
    {
        #region Public methods
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

        #endregion // Public methods
    }
}
