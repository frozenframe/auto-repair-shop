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

        public void DeleteTechEvent(TechEvent techEvent)
        {
            string sqlQuery = string.Format(SqlQueries.deleteTechEvent, techEvent.Id);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Log.Error(string.Format("An error is appearred during deleting from Client table: {0}", e.Message));
            }
        }

        public TechEvent InsertTechEvent(TechEvent techEvent)
        {
            object[] insertArgs = { techEvent.Car.Id, techEvent.EventStartDate.Value.ToShortDateString(), techEvent.EventEndDate ?? null };
            string insertQuery = string.Format(SqlQueries.insertTechEvent,
                                                            techEvent.Car.Id,
                                                            techEvent.EventStartDate.Value.ToShortDateString(),
                                                            techEvent.EventEndDate.HasValue ? techEvent.EventEndDate.Value.ToShortDateString() : "null");

            try
            {
                int id = InsertRecordIntoDb(insertQuery) ?? 0;
                if (id != 0)
                {
                    return new TechEvent(id, techEvent.Car, techEvent.EventStartDate, techEvent.EventEndDate);
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(
                    string.Format("An error is appearred during inserting TechEvent {0} {1} {2}. Error: {3}",
                    techEvent.Car.Id,
                    techEvent.EventStartDate,
                    techEvent.EventEndDate,
                    e.Message)
                    );
            }
            return null;
        }

        public void UpdateTechEvent(TechEvent techEvent)
        {
            object[] args = { techEvent.Car.Id,techEvent.EventStartDate,techEvent.EventEndDate,techEvent.Id};
            string sqlQuery = string.Format(SqlQueries.updateTechEvent, args);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Log.Error(string.Format("An error is appearred during updating TechEvent table: {0}", e.Message));
            }
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
                    new Car(reader.GetInt32(3),reader.GetInt32(4),new CarModel(reader.GetInt32(7),
                            new CarBrand(reader.GetInt32(8),reader["brand_name"].ToString()),reader.GetString(9)),reader.GetString(5),reader.GetString(6)),
                    DateTime.TryParse(reader["event_start_date"].ToString(), out DateTime startDate) ? startDate : (DateTime?)null,
                    DateTime.TryParse(reader["event_end_date"].ToString(), out DateTime endDate) ? endDate : (DateTime?)null));
            }
            reader.Close();
            return result;
        }
    }
}
