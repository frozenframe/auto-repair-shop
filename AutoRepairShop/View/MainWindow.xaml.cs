using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            Client newClient = new Client("Иванов", "Иван", "Иванович", "+712312344556", "Тестовый");
            Client addedClient = dbManager.addClient(newClient);
            
            // Эмулируем заполнение ComboBox со списками марок авто и выбор нужной из этого списка
            CarBrands carBrands = new CarBrands(dbManager.getCarBrands());
            CarBrand newCarBrand = null;
            foreach (CarBrand brand in carBrands.getCarBrands().Values)
            {
                newCarBrand = brand;
                break;
            }
            // Эмулируем заполнение ComboBox со списками моделей авто и выбор нужной из этого списка
            List<CarModel> carModels = dbManager.getCarModels(newCarBrand);
            CarModel newCarModel = carModels.First();
            
            // добавим новую машину для нового клиента и занесем ее в базу.
            Car newCar = new Car((int)addedClient.Id, newCarModel, "C123XY 70", "test car");
            Car addedCar = dbManager.addClientCar(addedClient, newCar);

            // Выведем информацию о новом клиенте
            List<Client> clients = dbManager.getClients();
            foreach(Client client in clients) {
                Console.WriteLine("Фамилия клиента: {0}, Имя: {1}", client.Lastname, client.Name);
            }

            //Получение списка работ
            SortedList<int, WorkType> worksTree = dbManager.getAllWorkTypes();

            // Пока заремарил код ниже. Так как с TreeView нужно разбираться уже предметно в классах шаблона MVVM. 
            // Набросок заполнения контрола здесь делать бесполезно

            //TreeView treeView1 = new TreeView();
            //foreach (WorkType workType in worksTree.Values)
            //{
            //    TreeNode node = new TreeNode(workType.WorkTypeName);
            //    //Items node = new TreeNode(workType.WorkTypeName);
            //    TreeNode parent;
            //    if (workType.ParentId == null)
            //        treeView1.Nodes.Add(node); // Добавление ноды в контрол TreeView
            //        //treeView1.Items.Add(node); // Добавление ноды в контрол TreeView
            //    else if (worksTree.TryGetValue(workType.ParentId.Value, out parent))
            //        parent.Nodes.Add(node);
            //    //nodes.Add(cat_id, node);
            //}

        }
    }
}
