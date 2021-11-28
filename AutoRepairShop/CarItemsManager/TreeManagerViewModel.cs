using AutoRepairShop.WorkTypeManager;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutoRepairShop.CarItemsManager
{
    public class TreeManagerViewModel
    {
        #region Data

        private Collection<WorkTypeViewModel> _rootNodes;
        private WorkTypeViewModel _rootWorkType;

        readonly ICommand _addCommand; //Надо посмотреть как народ обрабатывает команды во ВьюМодели.
        readonly ICommand _updateCommand;
        readonly ICommand _deleteCommand;
        readonly ICommand _changeParentCommand;

        #endregion // Data

        #region Constructor

        public TreeManagerViewModel(WorkType rootWorkType)
        {
            _rootWorkType = new WorkTypeViewModel(rootWorkType);
            _rootNodes = new Collection<WorkTypeViewModel>(
                new WorkTypeViewModel[]
                {
                    _rootWorkType
                });
        }

        #endregion // Constructor

        #region Properties

        #region RootNodes

        /// <summary>
        /// Returns a read-only collection containing the first person 
        /// in the family tree, to which the TreeView can bind.
        /// </summary>
        public Collection<WorkTypeViewModel> RootNodes
        {
            get { return _rootNodes; }
        }

        #endregion // RootNodes

        #region Commands

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
