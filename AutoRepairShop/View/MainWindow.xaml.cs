using AutoRepairShop.Stores;
using AutoRepairShop.ViewModel;
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

namespace AutoRepairShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        //private string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};"; // User Id=admin;Password=; - это если база будет защищена паролем
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};";
        // TODO: возможно эта строка подключения более правильная: 
        // new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=baza1.accdb");

        private string dbSourceFromConfig = @"C:\Users\FrozenFrame\source\repos\AutoRepairShop\CarRepair.accdb";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            //Logger.InitLogger();
            //DbManager dbManager = new DbManager(string.Format(connectionString, dbSourceFromConfig));
            //Client newClient = new Client("Иванов", "Иван", "Иванович", "+71231234455", "Тестовый");
            //dbManager.addClient(newClient);
            //List<Client> clients = dbManager.getClients();

            //foreach (Client client in clients)
            //{
            //    Console.WriteLine("Фамилия клиента: {0}, Имя: {1}", client.Lastname, client.Name);
            //}
        }
    }
}
