using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoRepairShop.WorkTypeManager
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class WorkTypeView : UserControl
    {
        public WorkTypeView()
        {
            InitializeComponent();

            WorkTypes workTypes = new WorkTypes();
            // По-хорошему нужно иметь возможность обрабатывать случай, когда есть несколько корневых узлов у дерева иерархии
            //Но пока зашиваемся, что он жестко один!
            WorkTypeViewModel workTypeViewModel = new WorkTypeViewModel(workTypes.RootWorkTypes.First());
            this.DataContext = this;
        }
    }
}
