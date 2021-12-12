using AutoRepairShop.MetaModel;
using AutoRepairShop.Model;
using AutoRepairShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AutoRepairShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};"; // User Id=admin;Password=; - это если база будет защищена паролем
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};";
        private string dbSourceFromConfig = "C:\\Users\\Wcoat\\source\\repos\\frozenframe\\auto-repair-shop\\CarRepair.accdb";

        public MainWindow()
        {
            InitializeComponent();
            Logger.InitLogger();
            DataContext = new MainViewModel();            
        }
    }
}
