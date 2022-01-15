using System.Windows;

namespace AutoRepairShop
{
    public class WindowService : IWindowService
    {
        private void ShowWindow(Window window, object viewModel, bool showDialog)
        {
            window.Content = viewModel;
            if (showDialog)
            {
                window.ShowDialog();
            }
            else {
                window.Show();
            }
        }
        public void ShowWindow(object viewModel, double height, double width, string title, bool showDialog)
        {
            var window = new Window
            {   
                Height = height,
                Width = width,
                Title = title
            };
            ShowWindow(window, viewModel, showDialog);
        }
        public void ShowWindow(object viewModel)
        {
            ShowWindow(viewModel, 450, 800, "", false);
        }
    }
}
