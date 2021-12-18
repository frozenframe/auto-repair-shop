using AutoRepairShop.MetaModel;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;

namespace AutoRepairShop.Model
{
    public class DbWorkType : DbManager
    {
        #region Fields

        private SortedList<int, WorkType> _allWorkTypes;

        #endregion Fields

        #region Properties

        public List<WorkType> RootWorkTypes
        {
            get
            {
                if (_allWorkTypes == null)
                {
                    _allWorkTypes = GetAllWorkTypes();
                }
                foreach (WorkType workType in _allWorkTypes.Values)
                {
                    WorkType parent;
                    // Пробегаемся по списку и проверяем каждый узел.
                    // Если узел не корневой, то найдем его парента и добавим этот узел ему в дочерние.
                    if (workType.ParentId.HasValue && _allWorkTypes.TryGetValue(workType.ParentId.Value, out parent))
                    {
                        parent.Children.Add(workType);
                    }
                    //WorkType currentNode = workType; // new TreeNode(workType.WorkTypeName);
                    //WorkType parent;
                    //if (workType.ParentId == null)
                    //{
                    //    //_workTypesTree.Add(workType.Id, workType); // Добавление корневого узла в список, отражащий наше импровизированное дерево.
                    //}
                    //else if (_allWorkTypes.TryGetValue(workType.ParentId.Value, out parent))
                    //{
                    //    parent.Children.Add(workType);
                    //}
                    ////    //nodes.Add(cat_id, node);
                }
                List<WorkType> rootNodes = _allWorkTypes.Where(kvPair => kvPair.Value.ParentId == null).Select(kvPair => kvPair.Value).ToList();
                return rootNodes;
            }
        }

        #endregion Properties

        #region Public methods

        public void DeleteWorkType(WorkType workType)
        {
            string sqlQuery = string.Format(SqlQueries.deleteWorkType, workType.Id);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Log.Error(string.Format("An error is appearred during deleting from WorkType table: {0}", e.Message));
            }
        }

        public WorkType AddWorkType(WorkType workType)
        {
            object[] insertArgs = { workType.ParentId, workType.WorkTypeName };
            string insertQuery = string.Format(SqlQueries.addWorkType, insertArgs);

            try
            {
                int id = InsertRecordIntoDb(insertQuery) ?? 0;
                if (id != 0)
                {
                    return new WorkType(id, workType.ParentId, workType.WorkTypeName);
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(
                    string.Format("An error is appearred during adding work type parentId/Name: {0}/{1}. Error: {2}",
                    workType.ParentId,
                    workType.WorkTypeName,
                    e.Message)
                    );
            }
            return null;
        }

        public void UpdateWorkType(WorkType workType)
        {
            object[] args = { workType.WorkTypeName, workType.Id };
            string sqlQuery = string.Format(SqlQueries.updateWorkType, args);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Log.Error(string.Format("An error is appearred during updating WorkType table: {0}", e.Message));
            }
        }
        public void ChangeWorkTypeParent(int workTypeId, int workTypeNewParentId)
        {
            object[] args = { workTypeNewParentId, workTypeId };
            string sqlQuery = string.Format(SqlQueries.changeWorkTypeParent, args);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Log.Error(string.Format("An error is appearred during updating WorkType table: {0}", e.Message));
            }
        }

        public WorkType getWorkType(int id)
        {
            WorkType workType = null;

            string sqlQuery = string.Format(SqlQueries.getWorkType, id);
            try
            {
                OleDbCommand command = new OleDbCommand(sqlQuery, Connection);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    workType = new WorkType((int)reader[0], (int)reader[1], reader[2].ToString());
                }
                reader.Close();

                return workType;
            }
            catch (Exception e)
            {
                Logger.Log.Error(string.Format("An error is appearred during getting record from WorkType table: {0}", e.Message));
                return null;
            }
        }
        #endregion Public methods

        #region Private methods
        private SortedList<int,WorkType> GetAllWorkTypes()
        {
            OleDbCommand command = new OleDbCommand(SqlQueries.getAllWorkTypes, Connection);
            SortedList<int, WorkType> result = new SortedList<int, WorkType>();

            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    WorkType workType;
                    if (reader.IsDBNull(1))
                    {
                        workType = new WorkType((int)reader[0], null, reader[2].ToString());
                    }
                    else
                    {
                        workType = new WorkType((int)reader[0], (int)reader[1], reader[2].ToString());
                    }
                    result.Add((int)reader[0], workType); //!List может быть и обычным! на этом этапе не нужен порядок записей. Позже упорядочим
                }
            }
            return result;
        }
        #endregion Private methods
    }
}
