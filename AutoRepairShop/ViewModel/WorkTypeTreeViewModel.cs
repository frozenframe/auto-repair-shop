using AutoRepairShop.Model;
using AutoRepairShop.Stores;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using static AutoRepairShop.Utils.Constants;
using System.Windows.Forms;
using AutoRepairShop.MetaModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoRepairShop.Utils;

namespace AutoRepairShop.ViewModel
{
    /// <summary>
    /// ViewModel для окна иерархического дерева полного списка видов работ. С кнопками добавления/обновления/удаления/смены группы.
    /// Эта ViewModel может использоваться и просто как пользовательский элемент для выбора нужного вида работ из полного списка.
    /// При создании экземпляра этого класса WorkTypeViewMode.SELECT по кнопке Ok будет возвращен выбранный в дереве вид работ.
    /// </summary>
    public class WorkTypeTreeViewModel //: INotifyPropertyChanged
    {
        #region Fields
        
        private DbWorkType _dbWorkType;

        private ObservableCollection<WorkTypeTreeItemViewModel> _rootNodes;
        private WorkTypeTreeItemViewModel _rootWorkType;

        private RelayCommand openTreeViewWindowCommand;

        private RelayCommand _addCommand;
        private RelayCommand _updateCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _changeParentCommand;
        private RelayCommand _okCommand;

        private readonly WorkTypeStore _workTypeStore;
        private string _buttonVisibility;
        private readonly WorkTypeViewMode _workTypeViewMode;

        private WorkType _selectedWorkType;


        #endregion Fields

        #region Constructors

        public WorkTypeTreeViewModel(WorkTypeStore workTypeStore, WorkTypeViewMode workTypeViewMode)
        {
            _workTypeViewMode = workTypeViewMode;
            _workTypeStore = workTypeStore;
            setDefaultValues();
        }

        public WorkTypeTreeViewModel(WorkTypeViewMode workTypeViewMode)
        {
            _workTypeViewMode = workTypeViewMode;

            _workTypeStore = new WorkTypeStore();
            if (workTypeViewMode == WorkTypeViewMode.MANAGEMENT)
            {
                _workTypeStore.WorkTypeChangedParent += OnSelectNewParent;
                _workTypeStore.WorkTypeCreated += OnCreateNewWorkType;
                _workTypeStore.WorkTypeUpdated += OnUpdateWorkType;
            }
            setDefaultValues();

        }
        private void setDefaultValues()
        {
            _dbWorkType = new DbWorkType();
            _buttonVisibility = _workTypeViewMode == WorkTypeViewMode.MANAGEMENT ? "Visible" : "Hidden";

            // По-хорошему нужно иметь возможность обрабатывать случай, когда есть несколько корневых узлов у дерева иерархии
            //Но пока зашиваемся, что он жестко один!
            _rootWorkType = new WorkTypeTreeItemViewModel(_dbWorkType.RootWorkTypes.First());
            _rootNodes = new ObservableCollection<WorkTypeTreeItemViewModel>(
                new WorkTypeTreeItemViewModel[]
                {
                    _rootWorkType
                });
        }

        #endregion Constructors

        public void Dispose()
        {
            _workTypeStore.WorkTypeChangedParent -= OnSelectNewParent;
            _workTypeStore.WorkTypeCreated -= OnCreateNewWorkType;
            _workTypeStore.WorkTypeUpdated -= OnUpdateWorkType;
        }

        #region Properties

        #region RootNodes

        /// <summary>
        /// Returns a read-only collection containing the first person 
        /// in the family tree, to which the TreeView can bind.
        /// </summary>
        public ObservableCollection<WorkTypeTreeItemViewModel> RootNodes => _rootNodes;

        #endregion // RootNodes

        public bool ButtonIsEnabled
        {
            get
            {
                return SelectedItem != null;
            }
        }

        public string ButtonVisibility
        {
            get
            {
                return _buttonVisibility;
            }
        }
        #endregion Properties

        public WorkTypeTreeItemViewModel SelectedItem
        {
            get {
                WorkTypeTreeItemViewModel selectedItem = null;
                foreach (var node in _rootNodes)
                {
                    selectedItem = getSelectedItem(node);
                    if (selectedItem != null)
                    {
                        break;
                    }
                }
                if (selectedItem != null)
                {
                    _selectedWorkType = selectedItem.WorkType;
                }
                return selectedItem;
            }
        }

        private WorkTypeTreeItemViewModel getSelectedItem(WorkTypeTreeItemViewModel treeItem)
        {
            if (treeItem.IsSelected)
            {
                return treeItem;
            }
            else
            {
                WorkTypeTreeItemViewModel selectedItem = null;
                foreach (var child in treeItem.Children)
                {
                    selectedItem = getSelectedItem(child);
                    if (selectedItem != null)
                    {
                        break;
                    }
                }
                return selectedItem;
            }
        }
        
        #region Commands

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
            new WindowService().ShowWindow(new WorkTypeTreeItemViewModel(_dbWorkType.RootWorkTypes.First()));
        }

        /// <summary>
        /// Returns the command used to add work type to tree.
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                      _addCommand = new RelayCommand(
                        execute: (commandParameter) => OpenAddWorkTypeWindow(commandParameter),
                        canExecute: (obj) => SelectedItem != null
                    );
                }
                return _addCommand;
            }
        }

        private void OpenAddWorkTypeWindow(object commandParameter)
        {
            new WindowService().ShowWindow(new WorkTypeAddUpdateViewModel(SelectedItem, _workTypeStore, CrudMode.ADD), 210, 400, commandParameter.ToString(), SHOW_MODAL);
        }

        /// <summary>
        /// Returns the command used to update work type name of the tree node.
        /// </summary>
        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                {
                    _updateCommand = new RelayCommand(
                         execute: (commandParameter) => OpenUpdateWorkTypeWindow(commandParameter),
                         canExecute: (obj) => SelectedItem != null
                     );
                }
                return _updateCommand;
            }
        }
        private void OpenUpdateWorkTypeWindow(object commandParameter)
        {
            new WindowService().ShowWindow(new WorkTypeAddUpdateViewModel(SelectedItem, _workTypeStore, CrudMode.UPDATE), 210, 400, commandParameter.ToString(), SHOW_MODAL);
        }

        /// <summary>
        /// Returns the command used to delete selected work type.
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(
                         execute: (commandParameter) => DeleteWorkType(commandParameter),
                         canExecute: (obj) => SelectedItem != null
                     );
                }
                return _deleteCommand;
            }
        }
        private void DeleteWorkType(object commandParameter)
        {
            if (_selectedWorkType != null)
            {
                var confirmation = MessageBox.Show(
                    "Будет удален выбранный узел и все его дочерние. Выполнить удаление?",
                    "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );

                if (confirmation == DialogResult.Yes)
                {
                    if(false/*_selectedWorkType is already used in some activity*/){
                        MessageBox.Show(
                            "Выбранный тип работ не может быть удален. Он задействован в произведенных работах.",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                        return;
                    }
                    _dbWorkType.DeleteWorkType(SelectedItem.WorkType);
                }
            }
        }

        /// <summary>
        /// Returns the command used to change parent of the selected work type by selecting another parent node from the tree.
        /// </summary>
        public ICommand ChangeParentCommand
        {
            get
            {
                if (_changeParentCommand == null)
                {
                    _changeParentCommand = new RelayCommand(
                         execute: (commandParameter) => ChangeParentCommandWindow(commandParameter),
                         canExecute: (obj) => SelectedItem != null
                     );
                }
                return _changeParentCommand;
            }
        }


        private void ChangeParentCommandWindow(object commandParameter)
        {
            WorkTypeStore workTypeStore1 = new WorkTypeStore();
            workTypeStore1.WorkTypeChangedParent += OnSelectNewParent;
            string title = string.Format("Выберите новую группу для '{0}'", _selectedWorkType.WorkTypeName);
            new WindowService().ShowWindow(new WorkTypeTreeViewModel(workTypeStore1, WorkTypeViewMode.CHANGE_PARENT), 450, 800, title, SHOW_MODAL);

        }

        /// <summary>
        /// Returns the command used to delete selected work type.
        /// </summary>
        public ICommand OkCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new RelayCommand(
                         execute: (commandParameter) => OkButtonClicked(commandParameter),
                         canExecute: (obj) => SelectedItem != null
                     );
                }
                return _okCommand;
            }
        }
        private void OkButtonClicked(object commandParameter)
        {
            if (WorkTypeViewMode.CHANGE_PARENT == _workTypeViewMode)
            {
                _workTypeStore.SelectNewParentWorkType(_selectedWorkType);
            }
            else if (WorkTypeViewMode.SELECT == _workTypeViewMode)
            {
                _workTypeStore.SelectWorkType(_selectedWorkType);
            }
        }

        #endregion Commands

        private void OnCreateNewWorkType(WorkType newWorktype)
        {
            //int index = RootNodes.IndexOf(SelectedItem);
            //RootNodes[index].WorkType.Children.Add(newWorktype);
            _selectedWorkType.Children.Add(newWorktype);
            OnPropertyChanged(nameof(RootNodes));
        }

        private void OnUpdateWorkType(WorkType newWorktype)
        {
            _selectedWorkType = newWorktype;
        }

        private void OnSelectNewParent(WorkType newParentWorktype)
        {
            if (_selectedWorkType == newParentWorktype)
            {
                MessageBox.Show("Узел не может быть перенесен сам в себя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _dbWorkType.ChangeWorkTypeParent((int)_selectedWorkType.Id, (int)newParentWorktype.Id);
                _selectedWorkType.ParentId = newParentWorktype.Id;
                // Скорее всего нужно будет проходить всех Childern of _rootWorkType и вносить изменения туда!!!
            }
            catch (Exception e)
            {
                WorkType parent = _dbWorkType.getWorkType((int)_selectedWorkType.ParentId);
                Logger.Log.Error(string.Format(
                    "Произошла ошибка при смене группы ('{0}') типа работ '{1}' на новую группу '{2}': {3}.",
                    parent.WorkTypeName,
                    _selectedWorkType.WorkTypeName,
                    newParentWorktype.WorkTypeName,
                    e.Message
                    )
                );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
}
