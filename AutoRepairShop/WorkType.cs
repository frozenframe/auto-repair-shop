using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop
{
    public class WorkType
    {
        #region Data
        public int Id { get; }
        public int? ParentId { get; }
        public string WorkTypeName { get; }

        private Collection<WorkType> _children;

        #endregion // Data

        #region Constructor
        public WorkType (int id, int parentId, String workTypeName)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.WorkTypeName = workTypeName;
        }

        #endregion // Constructor

        #region Properties

        public Collection<WorkType> Children
        {
            get { return _children; }
            //set { _children = value; }
        }

        #endregion // Properties
    }
}
