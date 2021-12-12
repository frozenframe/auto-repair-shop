using System;
using System.Data.OleDb;

namespace AutoRepairShop.Model
{
    public class DbManager : DbInteraction
    {

        #region Protected methods
        protected int? InsertRecordIntoDb(string insertQuery)
        {
            int? newRecodeId = null;
            OleDbTransaction transaction = null;
            try
            {
                transaction = Connection.BeginTransaction();
                OleDbCommand insertCommand = new OleDbCommand(insertQuery, Connection);
                insertCommand.Transaction = transaction;
                int rows = insertCommand.ExecuteNonQuery();
                if (rows == 1)
                {
                    newRecodeId = GetIdInsertedRecord(transaction);
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

        protected int GetIdInsertedRecord(OleDbTransaction transaction)
        {
            OleDbCommand selectCommand = new OleDbCommand("SELECT @@IDENTITY", Connection);
            selectCommand.Transaction = transaction;
            return (int)selectCommand.ExecuteScalar();
        }

        #endregion Protected methods

    }
}
