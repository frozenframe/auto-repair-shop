﻿using System;
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
    /// Interaction logic for AddWorkUserControl.xaml
    /// </summary>
    public partial class AddTechEventUserControl : UserControl
    {
        public AddTechEventUserControl()
        {
            InitializeComponent();
        }

        private void SetTechEventEndDate_Button_Click(object sender, RoutedEventArgs e)
        {
            TechEventEndDate_DatePicker.SelectedDate = DateTime.Now;
        }
    }
}
