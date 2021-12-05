using AutoRepairShop.Model;
using AutoRepairShop.WorkTypeManager;
using System.Linq;
using System.Windows.Controls;

namespace AutoRepairShop.View
{
    public partial class WorkTypeViewUserControl : UserControl
    {
        //DbWorkType _dbWorkType;

        public WorkTypeViewUserControl()
        {
            InitializeComponent();

            //_dbWorkType = new DbWorkType();
            //// По-хорошему нужно иметь возможность обрабатывать случай, когда есть несколько корневых узлов у дерева иерархии
            ////Но пока зашиваемся, что он жестко один!
            //WorkTypeViewModel workTypeViewModel = new WorkTypeViewModel(_dbWorkType.RootWorkTypes.First());
            //this.DataContext = workTypeViewModel;
        }
    }
}
