using AutoRepairShop.MetaModel;
using AutoRepairShop.Model;
using AutoRepairShop.Stores;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using static AutoRepairShop.Utils.Constants;

namespace AutoRepairShop.ViewModel
{
    /// <summary>
    /// ViewModel для окна добавления/обновления типа работ.
    /// </summary>
    public class WorkTypeAddUpdateViewModel : INotifyPropertyChanged
    {
        #region Fields
        private DbWorkType _dbWorkType;

        private WorkTypeStore _workTypeStore;
        private WorkType _parentWorkType;
        private WorkType _currentWorkType;

        private string _newWorkTypeName;
        private CrudMode _crudMode;

        private RelayCommand addOrUpdateWorkTypeCommand;

        #endregion Fields

        #region Constructors

        public WorkTypeAddUpdateViewModel(WorkTypeTreeItemViewModel workTypeViewModel, WorkTypeStore workTypeStore, CrudMode crudMode)
        {
            _workTypeStore = workTypeStore;
            _dbWorkType = new DbWorkType();
            _crudMode = crudMode;

            if (crudMode == CrudMode.ADD)
            {
                _parentWorkType = workTypeViewModel.WorkType;
            } 
            else if (crudMode == CrudMode.UPDATE)
            {
                _currentWorkType = workTypeViewModel.WorkType;
                _parentWorkType = _dbWorkType.getWorkType((int)_currentWorkType.ParentId);
            }
        }
        #endregion Constructors


        #region Properties
        public WorkType WorkType
        {
            get
            {
                return _currentWorkType;
            }
            set
            {
                _currentWorkType = value;
                OnPropertyChanged(nameof(Client));
            }
        }
        public string CurrentWorkTypeName
        {
            get
            {
                return _crudMode == CrudMode.UPDATE ? _currentWorkType.WorkTypeName : string.Empty;
            }
        }
        public bool ShowCurrentRecordInfo
        {
            get
            {
                return _crudMode == CrudMode.UPDATE;
            }
        }

        public string NewWorkTypeName
        {
            get
            {
                return _newWorkTypeName;
            }
            set
            {
                if (value != _newWorkTypeName)
                {
                    _newWorkTypeName = value;
                    OnPropertyChanged("NewWorkTypeName");
                }
            }
        }
        public string CurrentWorkTypeGroupName
        {
            get
            {
                return _parentWorkType.WorkTypeName;
            }
        }
        #endregion Properties

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ICommand AddOrUpdateCommand
        {
            get
            {
                if (addOrUpdateWorkTypeCommand == null)
                {
                    addOrUpdateWorkTypeCommand = new RelayCommand(AddOrUpdateWorkType);
                }
                return addOrUpdateWorkTypeCommand;
            }
        }

        private void AddOrUpdateWorkType(object commandParameter)
        {
            WorkType workType;
            WorkType result;
            if (_crudMode == CrudMode.ADD)
            {
                workType = new WorkType(_parentWorkType.Id, NewWorkTypeName);
                result = _dbWorkType.AddWorkType(workType);
                if (result == null)
                {
                    throw new Exception("Не удалось добавить новый тип работ. Детали ошибки можно найти в логе.");
                }
                _workTypeStore.CreateWorkType(result);
            }
            else if (_crudMode == CrudMode.UPDATE)
            {// Что будет, если сразу обновим здесь это поле? Возможное расхождение с базой, если update не пройдет?
                //_currentWorkType.WorkTypeName = NewWorkTypeName;
                workType = new WorkType(_currentWorkType.Id, _currentWorkType.ParentId, NewWorkTypeName);
                _dbWorkType.UpdateWorkType(workType);
                _workTypeStore.UpdateWorkType(workType);
            }
        }

    }
}
