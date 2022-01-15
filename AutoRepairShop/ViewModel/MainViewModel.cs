using System.Windows.Input;
using static AutoRepairShop.Utils.Constants;

namespace AutoRepairShop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private RelayCommand openClientDataWindowCommand;
        private RelayCommand openTreeViewWindowCommand;

        public MainViewModel()
        {
        }

        public ICommand OpenClientDataWindowCommand 
        {
            get
            {
                if (openClientDataWindowCommand == null)
                {
                    openClientDataWindowCommand = new RelayCommand(OpenClientDataWindow);
                }
                return openClientDataWindowCommand;
            }
        }

        private void OpenClientDataWindow(object commandParameter)
        {            
            new WindowService().ShowWindow(new ClientsViewViewModel());
        }

        public ICommand OpenTreeViewWindowCommand
        {
            get
            {
                if (openTreeViewWindowCommand == null)
                {
                    openTreeViewWindowCommand = new RelayCommand(OpenTreeViewWindow);
                }
                return openTreeViewWindowCommand;
            }
        }

        private void OpenTreeViewWindow(object commandParameter)
        {
            new WindowService().ShowWindow(new WorkTypeTreeViewModel(WorkTypeViewMode.MANAGEMENT), 450, 800, "Список типов работ", SHOW_MODAL);

        }


    }
}
