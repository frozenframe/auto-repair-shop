using AutoRepairShop;
using AutoRepairShop.MetaModel;
using AutoRepairShop.Model;
using AutoRepairShop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static AutoRepairShop.Utils.Constants;

namespace AutoRepairShop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private RelayCommand openClientDataWindowCommand;
        private RelayCommand openTreeViewWindowCommand;

        DbWork dbWork;
        DbTectEvent dbTectEvent;
        DbWorkType dbWorkType;
        public ObservableCollection<TechEvent> TechEvents { get; set; }
        public ObservableCollection<Work> WorksOfSelectedTechEvent { get; set; }
        private TechEvent _selectedTechEvent;


        public TechEvent SelectedTechEvent
        {
            get
            {
                return _selectedTechEvent;
            }
            set
            {
                _selectedTechEvent = value;
                if(_selectedTechEvent.Works is null)
                {
                    _selectedTechEvent.Works = FillTechEventWorks(_selectedTechEvent.Id);
                }
                
                OnPropertyChanged(nameof(SelectedTechEvent));
            }
        }
            

        private ObservableCollection<Work> FillTechEventWorks(int techEventId)
        {
            WorksOfSelectedTechEvent = new ObservableCollection<Work>(dbWork.GetTechEventWorks(techEventId));
            

            
            return WorksOfSelectedTechEvent;
        }

        public MainViewModel()
        {
            dbWork = new DbWork();
            dbTectEvent = new DbTectEvent();
            dbWorkType = new DbWorkType();
            var techEventsFromDb = dbTectEvent.GetTechEvents();            
            TechEvents = new ObservableCollection<TechEvent>(techEventsFromDb);
            //var newWork = new TechEvent();
            //TechEvents.Add(newWork);

            //_clientStore = new ClientStore();
            //ClientsList = new ObservableCollection<ClientViewModel>();
            //_clientStore.ClientCreated += OnClientCreated;
        }



        //public override void Dispose()
        //{
        //    _clientStore.ClientCreated -= OnClientCreated;
        //    base.Dispose();
        //}

        #region command
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
            new WindowService().ShowWindow(new ClientsViewViewModel(), 450, 1000, "Клиенты");
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
            new WindowService().ShowWindow(new WorkTypeTreeViewModel(WorkTypeViewMode.MANAGEMENT), 450, 800, "Список типов работ");

        }
        #endregion
    }
}
