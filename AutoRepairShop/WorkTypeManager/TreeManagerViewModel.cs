using AutoRepairShop.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AutoRepairShop.WorkTypeManager
{
    public class TreeManagerViewModel
    {
        #region Data

        DbWorkType _dbWorkType;

        private readonly ObservableCollection<WorkTypeViewModel> _rootNodes;
        private WorkTypeViewModel _rootWorkType;

        private RelayCommand openTreeViewWindowCommand;
        private readonly ICommand _addCommand; //Надо посмотреть как народ обрабатывает команды во ВьюМодели.
        private readonly ICommand _updateCommand;
        private readonly ICommand _deleteCommand;
        private readonly ICommand _changeParentCommand;

        #endregion // Data

        #region Constructor

        public TreeManagerViewModel()   //WorkType rootWorkType
        {
            _dbWorkType = new DbWorkType();
            //WorkTypeViewModel workTypeViewModel = new WorkTypeViewModel(_dbWorkType.RootWorkTypes.First());
            //this.DataContext = workTypeViewModel;

            // По-хорошему нужно иметь возможность обрабатывать случай, когда есть несколько корневых узлов у дерева иерархии
            //Но пока зашиваемся, что он жестко один!
            _rootWorkType = new WorkTypeViewModel(_dbWorkType.RootWorkTypes.First()); //rootWorkType
            _rootNodes = new ObservableCollection<WorkTypeViewModel>(
                new WorkTypeViewModel[]
                {
                    _rootWorkType
                });
        }

        #endregion // Constructor

        #region Properties

        #region RootNodes

        ///// <summary>
        ///// Returns a read-only collection containing the first person 
        ///// in the family tree, to which the TreeView can bind.
        ///// </summary>
        //public ObservableCollection<WorkTypeViewModel> RootNodes => _rootNodes;

        #endregion // RootNodes

        //public string WorkTypeName { 
        //    get
        //    {
        //        return _rootWorkType.WorkTypeName;
        //    }
        //}

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
            new WindowService().ShowWindow(new WorkTypeViewModel(_dbWorkType.RootWorkTypes.First()));//new ClientDataViewModel(_clientStore)
        }

        /// <summary>
        /// Returns the command used to add work type to tree.
        /// </summary>
        public ICommand AddCommand
        {
            get { return _addCommand; }
        }

        /// <summary>
        /// Returns the command used to add work type to tree.
        /// </summary>
        public ICommand UpdateCommand
        {
            get { return _updateCommand; }
        }

        /// <summary>
        /// Returns the command used to add work type to tree.
        /// </summary>
        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
        }

        /// <summary>
        /// Returns the command used to add work type to tree.
        /// </summary>
        public ICommand ChangeParentCommand
        {
            get { return _changeParentCommand; }
        }

        #endregion // Commands

        #endregion // Properties


    }
}
