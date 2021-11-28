using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop
{
    public class WorkTypes
    {
        #region Data

        private SortedList<int, WorkType> _allWorkTypes;
        private DbManager _dbManager;
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};";
        private string dbSourceFromConfig = @"C:\\Users\\Wcoat\\source\\repos\\frozenframe\\auto-repair-shop\\CarRepair.accdb";
        private SortedList<int, WorkType> _workTypesTree = new SortedList<int, WorkType>();

        #endregion // Data

        #region Constructors
        public WorkTypes()
        {
            _dbManager = new DbManager(string.Format(connectionString, dbSourceFromConfig));
        }
        #endregion // Constructors

        #region Properties

        public List<WorkType> RootWorkTypes { 
            get
            {
                if (_allWorkTypes == null)
                {
                    _allWorkTypes = _dbManager.getAllWorkTypes();
                }
                //WorkType firstNode = _allWorkTypes.First().Value; // У нашего дерева должен быть 
                foreach (WorkType workType in _allWorkTypes.Values)
                {
                    WorkType parent;
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


        #region Private section

        private bool TryGetValue(WorkType workType, out WorkType parent)
        {
            if (workType == null)
            {
                parent = null;
                return false;
            } else
            {
                _allWorkTypes.
            }

        }
        #endregion // Private section
    }
}
