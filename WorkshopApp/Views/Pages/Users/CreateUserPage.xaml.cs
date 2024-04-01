using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WorkshopApp.Controllers;
using WorkshopApp.Models;
using WorkshopApp.Models.Validators;

namespace WorkshopApp.Views.Pages.Users
{
    /// <summary>
    /// Страница создания пользователя
    /// </summary>
    public partial class CreateUserPage : Page
    {
        private User _user;
        public CreateUserPage(User user = null)
        {
            InitializeComponent();

            RoleID.DisplayMemberPath = "Name";
            RoleID.SelectedValuePath = "RoleID";
            RoleID.ItemsSource = RoleController.Index();

            if (user != null)
            {
                _user = user;

                Header.Text = "Редактирование пользователя";
                BtnIntrecate.Content = "Редактировать";

                Name.Text = user.Name;
                Surname.Text = user.Surname;
                Patronymic.Text = user.Patronimyc;
                Login.Text = user.Login;
                Password.Password = user.Password;
                Phone.Text = user.Phone;
                RoleID.SelectedValue = user.RoleID;
            }
        }

        private void BtnIntrecateClick(object sender, RoutedEventArgs e)
        {
            string roleId = RoleID.SelectedValue != null ? RoleID.SelectedValue.ToString() : string.Empty;

            var data = new Dictionary<string, string>
            {
                {"Name", Name.Text},
                {"Surname", Surname.Text},
                {"Patronymic", Patronymic.Text},
                {"Phone", Phone.Text},
                {"Login", Login.Text},
                {"Password", Password.Password},
                {"RoleID", roleId},
            };

            var (isValid, errors) = UserValidator.Validate(data);

            if (isValid)
            {
                if (_user != null)
                {
                    UserController.Update(_user.UserID, data);
                }
                else
                {
                    UserController.Create(data);
                }
                NavigationService.GoBack();
            }
            else
            {
                var errorMessage = string.Join("\n", errors);
                MessageBox.Show(errorMessage, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
