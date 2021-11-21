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

        private string dbSourceFromConfig = "C:\\Users\\Wcoat\\source\\repos\\frozenframe\\auto-repair-shop\\CarRepair.accdb";

        public MainWindow()
        {
            InitializeComponent();
            Logger.InitLogger();
            DbManager dbManager = new DbManager(String.Format(connectionString, dbSourceFromConfig));
            Client newClient = new Client("Иванов", "Иван", "Иванович", "+71231234455", "Тестовый");
            Client addedClient = dbManager.addClient(newClient);
            List<Client> clients = dbManager.getClients();

            foreach(Client client in clients) {
                Console.WriteLine("Фамилия клиента: {0}, Имя: {1}", client.Lastname, client.Name);
            }
        }
    }
}
