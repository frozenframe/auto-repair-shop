using AutoRepairShop.MetaModel;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Model
{
    public class DbTectEvent : DbManager
    {
        public List<TechEvent> GetTechEvents()
        {
            return GetTechEvent(SqlQueries.getTechEvent);
        }

        private List<TechEvent> GetTechEvent(string sqlQuery)
        {            
            OleDbCommand command = new OleDbCommand(sqlQuery, Connection);
            OleDbDataReader reader = command.ExecuteReader();

            List<TechEvent> result = new List<TechEvent>();
            while (reader.Read())
            {
                //"select id, client_car_id, event_start_date, event_end_date from Tech_Event";
                result.Add(new TechEvent((int)reader[0], 
                    new Car(reader.GetInt32(4),new CarModel(reader.GetInt32(7),
                            new CarBrand(reader.GetInt32(8),reader["brand_name"].ToString()),reader.GetString(9)),reader.GetString(5),reader.GetString(6)),
                    DateTime.Parse(reader["event_start_date"].ToString()), DateTime.Parse(reader["event_end_date"].ToString())));
            }
            reader.Close();
            return result;            
        }
    }
}
