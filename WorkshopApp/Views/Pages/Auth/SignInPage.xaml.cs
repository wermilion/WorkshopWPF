using System;
using System.Windows;
using System.Windows.Controls;
using WorkshopApp.Controllers;

namespace WorkshopApp.Views.Pages.Auth
{
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        private void HLinkRegistraionClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUpPage());
        }

        private void BtnEntryClick(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;

            try
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    throw new Exception("Логин и пароль не могут быть пустыми");
                }

                UserController.SignIn(Login.Text.Trim(), Password.Password.Trim());

                MainWindow.PreviousPage = new SignInPage();
                NavigationService.Navigate(new HomePage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
