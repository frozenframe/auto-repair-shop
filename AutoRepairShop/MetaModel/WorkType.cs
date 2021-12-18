using System;
using System.Collections.ObjectModel;

namespace AutoRepairShop.MetaModel
{
    public class WorkType
    {
        #region Properties
        public int? Id { get; }
        public int? ParentId { get; set; } // Давать возможность устанавливать эти значения нарпяммую плохая идея. Временный костыль
        public string WorkTypeName { get; }

        public Collection<WorkType> Children { get; } = new Collection<WorkType>();

        #endregion Properties

        #region Constructor
        public WorkType (int? id, int? parentId, String workTypeName)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.WorkTypeName = workTypeName;
        }

        public WorkType(int? parentId, String workTypeName) : this(null, parentId, workTypeName)
        {
        }
        #endregion Constructor


    }
}
