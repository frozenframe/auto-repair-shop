﻿using AutoRepairShop.MetaModel;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            while (reader.Read())
            {
                var work = new WorkMate((int)reader[0], techEventId,new WorkType(reader.GetInt32(4),reader["work_type"].ToString()),DateTime.Parse(reader["work_date"].ToString()),DateTime.Parse(reader["remind_day"].ToString()),reader["comment"].ToString(),false);
                result.Add(work);
            }
            reader.Close();
            return result;
        }
    }
}
