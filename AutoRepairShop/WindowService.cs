using System.Windows;

namespace AutoRepairShop
{
    public class WindowService : IWindowService
    {
        private void ShowWindow(Window window, object viewModel)
        {
            window.Content = viewModel;
            window.Show();
        }
        public void ShowWindow(object viewModel, double height, double width, string title)
        {
            var window = new Window
            {   
                Height = height,
                Width = width,
                Title = title
            };
            ShowWindow(window, viewModel);
        }
        public void ShowWindow(object viewModel)
        {
            ShowWindow(viewModel, 450, 800, "");
        }
    }
}
