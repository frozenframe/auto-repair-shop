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

namespace AutoRepairShop.View
{
    /// <summary>
    /// Логика взаимодействия для ClientDataUserControl.xaml
    /// </summary>
    public partial class ClientDataUserControl : UserControl
    {
        public ClientDataUserControl()
        {
            InitializeComponent();
            
        }

        private void Surname_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(Surname_Popup.IsOpen == false)
            {
                Surname_Popup.IsOpen = true;
                SurnamePopup_TextBlock.Text = string.IsNullOrWhiteSpace(Surname_TextBox.Text) ? string.Empty : $@"Старое значение: {Surname_TextBox.Text}";
                Surname_TextBox.SelectAll();
            }            
        }

        private void Surname_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void FirstName_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FirstName_Popup.IsOpen == false)
            {
                FirstName_Popup.IsOpen = true;
                NamePopup_TextBlock.Text = string.IsNullOrWhiteSpace(FirstName_TextBox.Text) ? string.Empty : $@"Старое значение: {FirstName_TextBox.Text}";
                FirstName_TextBox.SelectAll();
            }
        }
    }
}
