using AutoRepairShop.MetaModel;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AutoRepairShop.ViewModel
{
    public class WorkViewModel : ViewModelBase
    {
        public ObservableCollection<WorkMate> Works { get; set; }
        private WorkMate _selectedWork;

        public WorkMate SelectedWork
        {
            get
            {
                return _selectedWork;
            }
            set
            {
                _selectedWork = value;
                OnPropertyChanged(nameof(SelectedWork));
            }
        }


        public WorkViewModel(ObservableCollection<WorkMate> works)
        {
            Works = works;
            foreach (var work in Works)
            {
                work.PropertyChanged += ItemPropertyChanged;
            }
        }


        public override void Dispose()
        {
            foreach (var work in Works)
            {
                work.PropertyChanged -= ItemPropertyChanged;
            }
            base.Dispose();
        }


        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ((WorkMate)sender).WasEdited = true;
        }
    }
}
