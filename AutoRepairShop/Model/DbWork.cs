using AutoRepairShop.MetaModel;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace AutoRepairShop.Model
{
    public class DbWork : DbManager
    {
        public List<WorkMate> GetTechEventWorks(int techEventId)
        {
            return GetTechEventWorks(SqlQueries.getAllTechEventWorks,techEventId);
        }

        private List<WorkMate> GetTechEventWorks(string sqlQuery, int techEventId)
        {
            string selectQuery = string.Format(SqlQueries.getAllTechEventWorks,techEventId);
            OleDbCommand command = new OleDbCommand(selectQuery, Connection);
            OleDbDataReader reader = command.ExecuteReader();

            List<WorkMate> result = new List<WorkMate>();
            if(!reader.HasRows)
            {
                reader.Close();
                return result;
            }
            while (reader.Read())
            {
                var workType = new WorkType(
                    reader.GetInt32(4),
                    reader[5] is DBNull ? null : (int?)reader[5],
                    reader["work_type"].ToString());

                var work = new WorkMate(
                    (int)reader[0],
                    techEventId,
                    workType,
                    DateTime.TryParse(reader["work_date"].ToString(), out DateTime workDate) ? workDate : (DateTime?)null,
                    DateTime.TryParse(reader["remind_day"].ToString(), out DateTime remindDate) ? remindDate : (DateTime?)null,
                    reader["comment"].ToString(),
                    false);
                result.Add(work);
            }
            reader.Close();
            return result;
        }

        public void InsertWorks(List<WorkMate> works,int techEventId)
        {
            foreach(var work in works)
            {
                InsertWork(work, techEventId);
            }
        }
        

        private WorkMate InsertWork(WorkMate work, int techEventId)
        {            
            string insertQuery = string.Format(SqlQueries.insertWork, 
                                                techEventId,
                                                work.WorkType.Id,
                                                work.WorkDate.Value.ToShortDateString(),
                                                work.RemindDay.HasValue ? work.RemindDay.Value.ToShortDateString() : "null",
                                                work.Comment);
            try
            {
                int id = InsertRecordIntoDb(insertQuery) ?? 0;
                if (id != 0)
                {
                    work.Id = id;
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(
                    string.Format("An error is appearred during inserting work {0} {1} {2} {3}. Error: {4}",
                    work.TechEventId,
                    work.WorkType,
                    work.WorkDate,
                    work.RemindDay,
                    e.Message)
                    );
            }
            return null;
        }


        public void UpdateWorks(List<WorkMate> works)
        {
            foreach(var work in works)
            {
                UpdateWorks(work);
            }
        }

        private void UpdateWorks(WorkMate work)
        {
            string sqlQuery = string.Format(SqlQueries.updateWork, work.WorkDate, work.RemindDay, work.Comment, work.Id);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Log.Error(string.Format("An error is appearred during updating Work table: {0}", e.Message));
            }
        }
    }
}