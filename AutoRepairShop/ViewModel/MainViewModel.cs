using AutoRepairShop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoRepairShop.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }        

        private RelayCommand openAddWorkWindowCommand;

        public ICommand OpenAddWorkWindowCommand
        {
            get
            {
                if (openAddWorkWindowCommand == null)
                {
                    openAddWorkWindowCommand = new RelayCommand(OpenAddWorkWindow);
                }

                return openAddWorkWindowCommand;
            }
        }

        private void OpenAddWorkWindow(object commandParameter)
        {
            new WindowService().ShowWindow(new AddWorkViewModel());
        }
    }
}
