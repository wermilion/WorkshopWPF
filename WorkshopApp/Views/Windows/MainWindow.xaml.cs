using System;
using System.Windows;
using System.Windows.Controls;
using WorkshopApp.Views.Pages.Auth;

namespace WorkshopApp
{
    public partial class MainWindow : Window
    {
        public bool HeaderVisibility { get; set; } = true;

        public static Page PreviousPage { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            WorkshopFrame.NavigationService.Navigate(new SignInPage());
        }

        private void WorkshopFrameContendRendered(object sender, EventArgs e)
        {
            /*if (WorkshopFrame.CanGoBack && !((PreviousPage is SignInPage) || (PreviousPage is SignUpPage)))
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Collapsed;
            }*/
        }

        public void GoBack()
        {
            if (WorkshopFrame.CanGoBack)
            {
                WorkshopFrame.GoBack();
            }
        }
    }
}
