using AutoRepairShop;
using AutoRepairShop.MetaModel;
using AutoRepairShop.Model;
using AutoRepairShop.Stores;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static AutoRepairShop.Utils.Constants;

namespace AutoRepairShop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private RelayCommand _openClientDataWindowCommand;
        private RelayCommand _openSettingsWindowCommand;
        private RelayCommand _openTreeViewWindowCommand;
        private RelayCommand _openTechEventWindowCommand;
        private RelayCommand _openEditTechEventWindowCommand;

        private readonly TechEventStore _techEventStore;

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
                    _selectedTechEvent.Works = FillTechEventWorks((int)_selectedTechEvent.Id);
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
            _techEventStore = new TechEventStore();
            _techEventStore.TechEventAdded += OnTechEventAdded;
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

        private void OnTechEventAdded(TechEvent techEvent)
        {
            TechEvents.Add(techEvent);
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
                if (_openClientDataWindowCommand == null)
                {
                    _openClientDataWindowCommand = new RelayCommand(OpenClientDataWindow);
                }
                return _openClientDataWindowCommand;
            }
        }

        private void OpenClientDataWindow(object commandParameter)
        {
            new WindowService().ShowWindow(new ClientsViewViewModel(), 450, 1000, "Клиенты", true);
        }

        public ICommand OpenTreeViewWindowCommand
        {
            get
            {
                if (_openTreeViewWindowCommand == null)
                {
                    _openTreeViewWindowCommand = new RelayCommand(OpenTreeViewWindow);
                }
                return _openTreeViewWindowCommand;
            }
        }

        private void OpenTreeViewWindow(object commandParameter)
        {
            new WindowService().ShowWindow(new WorkTypeTreeViewModel(WorkTypeViewMode.MANAGEMENT), 450, 800, "Список типов работ", SHOW_MODAL);

        }

        public ICommand OpenSettingsWindowCommand
        {
            get
            {
                if (_openSettingsWindowCommand == null)
                {
                    _openSettingsWindowCommand = new RelayCommand(OpenSettingsWindow);
                }
                return _openSettingsWindowCommand;
            }
        }
        private void OpenSettingsWindow(object commandParameter)
        {
            new WindowService().ShowWindow(new SettingsViewModel(), 524, 814, "Общие настройки", true);
        }

        

        public ICommand OpenTechEventWindowCommand
        {
            get
            {
                if (_openTechEventWindowCommand == null)
                {
                    _openTechEventWindowCommand = new RelayCommand(OpenTechEventWindow);
                }
                return _openTechEventWindowCommand;
            }
        }

        private void OpenTechEventWindow(object commandParameter)
        {
            new WindowService().ShowWindow(new TechEventViewModel(_techEventStore), 600, 900, "Добавление ремонта", true);
        }

        public ICommand OpenEditTechEventWindowCommand
        {
            get
            {
                if (_openEditTechEventWindowCommand == null)
                {
                    _openEditTechEventWindowCommand = new RelayCommand(OpenEditTechEventWindow);
                }
                return _openEditTechEventWindowCommand;
            }
        }

        private void OpenEditTechEventWindow(object commandParameter)
        {
            if(SelectedTechEvent  != null)
            {
                new WindowService().ShowWindow(new TechEventViewModel(_techEventStore, SelectedTechEvent), 600, 900, "Добавление ремонта", true);
            }            
        }
        #endregion

        
    }
}
