using AutoRepairShop.MetaModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AutoRepairShop.WorkTypeManager
{
    public class WorkTypeViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<WorkTypeViewModel> _children;
        private WorkTypeViewModel _parent;
        private WorkType _workType;

        bool _isExpanded;
        bool _isSelected;

        readonly ICommand _addCommand; //Надо посмотреть как народ обрабатывает команды во ВьюМодели.
        readonly ICommand _changeCommand;
        readonly ICommand _deleteCommand;
        readonly ICommand _changeParentCommand;

        #endregion // Fields

        #region Constructor

        public WorkTypeViewModel(WorkType workType) : this(workType, null)
        {
        }

        public WorkTypeViewModel(WorkType workType, WorkTypeViewModel parent)
        {
            _workType = workType;
            _parent = parent;

            _children = new ObservableCollection<WorkTypeViewModel>(
                   (from child in _workType.Children
                    select new WorkTypeViewModel(child, this))
                    .ToList<WorkTypeViewModel>());

        }
        #endregion // Constructor

        #region Properties

        #region Children

        public ObservableCollection<WorkTypeViewModel> Children
        {
            get { return _children; }
        }

        public string WorkTypeName
        {
            get
            {
                return _workType.WorkTypeName;
            }
        }

        #endregion // Children

        #region IsExpanded

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
            }
        }

        #endregion // IsExpanded

        #region IsSelected

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion // IsSelected


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
        public ICommand ChangeCommand
        {
            get { return _changeCommand; }
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

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members











        //#region OldData

        //readonly ReadOnlyCollection<PersonViewModel> _firstGeneration;
        //readonly PersonViewModel _rootPerson;
        //readonly ICommand _searchCommand;

        //IEnumerator<PersonViewModel> _matchingPeopleEnumerator;
        //string _searchText = String.Empty;

        //#endregion // Data

        //#region Constructor

        //public WorkTypeViewModel(Person rootPerson)
        //{
        //    _rootPerson = new PersonViewModel(rootPerson);

        //    _firstGeneration = new ReadOnlyCollection<PersonViewModel>(
        //        new PersonViewModel[]
        //        {
        //            _rootPerson
        //        });

        //    _searchCommand = new SearchFamilyTreeCommand(this);
        //}

        //#endregion // Constructor

        //#region Properties

        //#region FirstGeneration

        ///// <summary>
        ///// Returns a read-only collection containing the first person 
        ///// in the family tree, to which the TreeView can bind.
        ///// </summary>
        //public ReadOnlyCollection<PersonViewModel> FirstGeneration
        //{
        //    get { return _firstGeneration; }
        //}

        //#endregion // FirstGeneration

        //#region SearchCommand

        ///// <summary>
        ///// Returns the command used to execute a search in the family tree.
        ///// </summary>
        //public ICommand SearchCommand
        //{
        //    get { return _searchCommand; }
        //}

        //private class SearchFamilyTreeCommand : ICommand
        //{
        //    readonly FamilyTreeViewModel _familyTree;

        //    public SearchFamilyTreeCommand(FamilyTreeViewModel familyTree)
        //    {
        //        _familyTree = familyTree;
        //    }

        //    public bool CanExecute(object parameter)
        //    {
        //        return true;
        //    }

        //    event EventHandler ICommand.CanExecuteChanged
        //    {
        //        // I intentionally left these empty because
        //        // this command never raises the event, and
        //        // not using the WeakEvent pattern here can
        //        // cause memory leaks.  WeakEvent pattern is
        //        // not simple to implement, so why bother.
        //        add { }
        //        remove { }
        //    }

        //    public void Execute(object parameter)
        //    {
        //        _familyTree.PerformSearch();
        //    }
        //}

        //#endregion // SearchCommand

        //#region SearchText

        ///// <summary>
        ///// Gets/sets a fragment of the name to search for.
        ///// </summary>
        //public string SearchText
        //{
        //    get { return _searchText; }
        //    set
        //    {
        //        if (value == _searchText)
        //            return;

        //        _searchText = value;

        //        _matchingPeopleEnumerator = null;
        //    }
        //}

        //#endregion // SearchText

        //#endregion // Properties

        //#region Search Logic

        //void PerformSearch()
        //{
        //    if (_matchingPeopleEnumerator == null || !_matchingPeopleEnumerator.MoveNext())
        //        this.VerifyMatchingPeopleEnumerator();

        //    var person = _matchingPeopleEnumerator.Current;

        //    if (person == null)
        //        return;

        //    // Ensure that this person is in view.
        //    if (person.Parent != null)
        //        person.Parent.IsExpanded = true;

        //    person.IsSelected = true;
        //}

        //void VerifyMatchingPeopleEnumerator()
        //{
        //    var matches = this.FindMatches(_searchText, _rootPerson);
        //    _matchingPeopleEnumerator = matches.GetEnumerator();

        //    if (!_matchingPeopleEnumerator.MoveNext())
        //    {
        //        MessageBox.Show(
        //            "No matching names were found.",
        //            "Try Again",
        //            MessageBoxButton.OK,
        //            MessageBoxImage.Information
        //            );
        //    }
        //}

        //IEnumerable<PersonViewModel> FindMatches(string searchText, PersonViewModel person)
        //{
        //    if (person.NameContainsText(searchText))
        //        yield return person;

        //    foreach (PersonViewModel child in person.Children)
        //        foreach (PersonViewModel match in this.FindMatches(searchText, child))
        //            yield return match;
        //}

        //#endregion // Search Logic
    }

}

