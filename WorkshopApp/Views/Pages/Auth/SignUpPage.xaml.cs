using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WorkshopApp.Controllers;
using WorkshopApp.Models;
using WorkshopApp.Models.Validators;

namespace WorkshopApp.Views.Pages.Auth
{
    public partial class SignUpPage : Page
    {
        private int _role;

        public SignUpPage()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            var roles = RoleController.Index();

            RoleID.DisplayMemberPath = "Name";
            RoleID.SelectedValuePath = "RoleID";
            RoleID.ItemsSource = roles;

            RoleID.SelectedValue = roles.First(x => x.Name == "Клиент").RoleID;

            RoleID.IsEnabled = false;

            Role selectedRole = RoleID.SelectedItem as Role;
            _role = selectedRole.RoleID;
        }

        private void BtnRegisterClick(object sender, RoutedEventArgs e)
        {
            var data = new Dictionary<string, string>
            {
                {"Name", Name.Text},
                {"Surname", Surname.Text},
                {"Patronymic", Patronymic.Text},
                {"Phone", Phone.Text},
                {"Login", Login.Text},
                {"Password", Password.Password},
                {"RoleID", _role.ToString()},
            };

            var (isValid, errors) = UserValidator.Validate(data);

            if (isValid)
            {
                App.authUser = UserController.Create(data);
                NavigationService.Navigate(new HomePage());
            }
            else
            {
                var errorMessage = string.Join("\n", errors);
                MessageBox.Show(errorMessage, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
