using System;
using System.Windows;
using WorkshopApp.Views.Pages.Auth;

namespace WorkshopApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            WorkshopFrame.NavigationService.Navigate(new SignInPage());
        }

        private void WorkshopFrameContendRendered(object sender, EventArgs e) { }

        public void GoBack()
        {
            if (WorkshopFrame.CanGoBack)
            {
                WorkshopFrame.GoBack();
            }
        }
    }
}
