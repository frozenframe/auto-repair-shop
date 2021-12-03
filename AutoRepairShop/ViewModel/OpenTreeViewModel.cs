using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoRepairShop.ViewModel
{
    public class OpenTreeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public OpenTreeViewModel()
        {
           
        }
        
    }
}