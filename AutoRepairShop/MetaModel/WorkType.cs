using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.MetaModel
{
    public class WorkType
    {
        #region Data
        private int _id;
        private int? _parentId;
        private string _workTypeName;

        private Collection<WorkType> _children = new Collection<WorkType>();

        #endregion // Data

        #region Constructor
        public WorkType (int id, int? parentId, String workTypeName)
        {
            this._id = id;
            this._parentId = parentId;
            this._workTypeName = workTypeName;
        }

        #endregion // Constructor

        #region Properties
        public int Id {
            get { return _id; }
        }
        public int? ParentId {
            get { return this._parentId; }
        }
        public string WorkTypeName
        {
            get { return this._workTypeName; }
        }

        public Collection<WorkType> Children
        {
            get { return _children; }
            //set { _children = value; }
        }

        #endregion // Properties
    }
}
