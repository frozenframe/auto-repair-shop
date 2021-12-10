using System;
using System.Data.OleDb;

namespace AutoRepairShop.Model
{
    public class DbManager : DbInteraction
    {

        #region Protected methods
        protected int? insertRecordIntoDb(String insertQuery)
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

        protected int getIdInsertedRecord(OleDbTransaction transaction)
        {
            OleDbCommand selectCommand = new OleDbCommand("SELECT @@IDENTITY", connection);
            selectCommand.Transaction = transaction;
            return (int)selectCommand.ExecuteScalar();
        }

        #endregion Protected methods

    }
}
