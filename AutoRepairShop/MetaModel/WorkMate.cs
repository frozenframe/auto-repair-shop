using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.MetaModel
{
    public class WorkMate : Work
    {
        private bool _wasEdited;

        public bool WasEdited
        {
            get
            {
                return _wasEdited;
            }
            set
            {
                _wasEdited = value;
                OnPropertyChanged(nameof(WasEdited));
            }
        }

        public WorkMate(int? id, int? techEventId, WorkType workType, DateTime? workDate, DateTime? remindDay, string comment,bool wasEdited)
        {
            Id = id;
            TechEventId = techEventId;
            WorkType = workType;
            WorkDate = workDate;
            RemindDay = remindDay;
            Comment = comment;
            WasEdited = wasEdited;
        }
    }
}
