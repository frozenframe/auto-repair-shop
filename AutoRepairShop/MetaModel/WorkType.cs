using System;
using System.Collections.ObjectModel;

namespace AutoRepairShop.MetaModel
{
    public class WorkType
    {
        #region Properties
        public int Id { get; }
        public int? ParentId { get; }
        public string WorkTypeName { get; }

        public Collection<WorkType> Children { get; } = new Collection<WorkType>();

        #endregion Properties

        #region Constructor
        public WorkType (int id, int? parentId, String workTypeName)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.WorkTypeName = workTypeName;
        }

        #endregion Constructor


    }
}
