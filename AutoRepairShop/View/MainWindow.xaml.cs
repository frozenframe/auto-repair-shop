using AutoRepairShop.MetaModel;
using AutoRepairShop.Model;
using AutoRepairShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AutoRepairShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};"; // User Id=admin;Password=; - это если база будет защищена паролем
        //private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};";
        //private string dbSourceFromConfig = "C:\\Users\\Wcoat\\source\\repos\\frozenframe\\auto-repair-shop\\CarRepair.accdb";

        public MainWindow()
        {
            InitializeComponent();
            Logger.InitLogger();
            DataContext = new MainViewModel();
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    row.IsSelected = true;
                    break;
                }
        }


        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    //row.IsSelected = true;
                    break;
                }
        }
    }
}
