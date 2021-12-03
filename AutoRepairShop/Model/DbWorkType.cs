using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;

namespace AutoRepairShop.Model
{
    public class DbWorkType : DbInteraction
    {
        #region Fields

        private SortedList<int, WorkType> _allWorkTypes;

        #endregion // Fields

        #region Properties

        public List<WorkType> RootWorkTypes
        {
            get
            {
                if (_allWorkTypes == null)
                {
                    _allWorkTypes = getAllWorkTypes();
                }
                //WorkType firstNode = _allWorkTypes.First().Value; // У нашего дерева должен быть 
                foreach (WorkType workType in _allWorkTypes.Values)
                {
                    WorkType parent;
                    // Пробегаемся по списку и проверяем каждый узел.
                    // Если узел не корневой, то найдем его парента и добавим этот узел ему в дочерние.
                    if (workType.ParentId != null && _allWorkTypes.TryGetValue(workType.ParentId.Value, out parent))
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

        #endregion // Properties

        #region Public methods
        #endregion // Public methods

        #region Private methods
        private SortedList<int,WorkType> getAllWorkTypes()
        {
            OleDbCommand command = new OleDbCommand(SqlQueries.getAllWorkTypes, connection);
            SortedList<int, WorkType> result = new SortedList<int, WorkType>();

            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    WorkType workType = new WorkType((int)reader[0], (int)reader[1], reader[2].ToString());
                    result.Add((int)reader[0], workType); //!List может быть и обычным! на этом этапе не нужен порядок записей. Позже упорядочим
                }
            }
            return result;
        }
        #endregion // Private methods
    }
}
