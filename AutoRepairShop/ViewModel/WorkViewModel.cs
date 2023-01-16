using AutoRepairShop;
using AutoRepairShop.MetaModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoRepairShop.ViewModel
{
    public class WorkViewModel : ViewModelBase
    {
        public ObservableCollection<WorkMate> Works { get; set; }
        private WorkMate _selectedWork;
        private DateTime? _workDate;
        private DateTime? _remindDate;
        private string _comment;

        public DateTime? RemindDate
        {
            get
            {
                return _remindDate;
            }
            set
            {
                _remindDate = value;
                SelectedWork.WasEdited = true;
                OnPropertyChanged(nameof(RemindDate));
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
                SelectedWork.WasEdited = true;
                OnPropertyChanged(nameof(WorkDate));
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
                SelectedWork.WasEdited = true;
                OnPropertyChanged(nameof(Comment));
            }
        }


        public WorkMate SelectedWork
        {
            get
            {
                return _selectedWork;
            }
            set
            {
                _selectedWork = value;
                WorkDate = value.WorkDate;
                RemindDate = value.RemindDay;
                Comment = value.Comment;
                OnPropertyChanged(nameof(SelectedWork));
            }
        }


        public WorkViewModel(ObservableCollection<WorkMate> works)
        {
            Works = works;
        }

        private RelayCommand _saveWorksChangesCommand;

        public ICommand SaveWorksChangesCommand
        {
            get
            {
                if (_saveWorksChangesCommand == null)
                {
                    _saveWorksChangesCommand = new RelayCommand(SaveWorksChanges);
                }

                return _saveWorksChangesCommand;
            }
        }

        private void SaveWorksChanges(object commandParameter)
        {

        }
    }
}
