using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop
{
    public class WorkType
    {
        public int Id { get; }
        public int? ParentId { get; }
        public string WorkTypeName { get; }

        public WorkType (int id, int parentId, String workTypeName)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.WorkTypeName = workTypeName;
        }
    }
}
