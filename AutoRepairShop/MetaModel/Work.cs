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
        private int _id;
        private int _techEventId;
        private WorkType _workType;
        private DateTime _workDate;
        private DateTime _remindDay;
        private string _comment;

        public int Id { get => _id; set => _id = value; }
        public int TechEventId { get; set; }
        public WorkType WorkType { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime RemindDay { get; set; }
        public string Comment { get; set; }

        public Work()
        {

        }
        public Work(int techEventId, WorkType workType, DateTime workDate, DateTime remindDay, string comment)
        {
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
