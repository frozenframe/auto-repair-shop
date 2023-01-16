using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.MetaModel
{
    public class Work : INotifyPropertyChanged
    {
        private int? _id;
        private int? _techEventId;
        private WorkType _workType;
        private DateTime? _workDate;
        private DateTime? _remindDay;
        private string _comment;

        public int? Id { get => _id; set => _id = value; }
        public int? TechEventId { get; set; }

        public WorkType WorkType
        {
            get 
            {
                return _workType;
            }
            set
            {
                _workType = value;
                OnPropertyChanged(nameof(WorkType));
            }
        }


        public DateTime? WorkDate
        {
            get
            {
                return _workDate;
            }
            set
            {
                _workDate = value;
                OnPropertyChanged(nameof(WorkDate));
            }
        }


        public DateTime? RemindDay
        {
            get
            {
                return _remindDay;
            }
            set
            {
                _remindDay = value;
                OnPropertyChanged(nameof(RemindDay));
            }
        }


        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public Work()
        {

        }

        public Work(int? id, int? techEventId, WorkType workType, DateTime workDate, DateTime remindDay, string comment)
        {
            Id = id;
            TechEventId = techEventId;
            WorkType = workType;
            WorkDate = workDate;
            RemindDay = remindDay;
            Comment = comment;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
