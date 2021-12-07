using System.Windows;

namespace AutoRepairShop
{
    public class WindowService : IWindowService
    {
        public void ShowWindow(object viewModel)
        {
            var window = new Window
            {
                Height = 450,
                Width = 800
            };
            window.Content = viewModel;            
            window.Show();
        }
    }
}
