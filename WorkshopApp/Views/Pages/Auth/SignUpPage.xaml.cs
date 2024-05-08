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
        /// <summary>
        /// Выбранный ID роли из ComboBox RoleID
        /// </summary>
        private int _role;

        public SignUpPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Заполняет RoleID (ComboBox) данными. Отрабатывает после загрузки страницы
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Событие</param>
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            RoleID.DisplayMemberPath = "Name";
            RoleID.SelectedValuePath = "RoleID";
            RoleID.ItemsSource = RoleController.Index();

            RoleID.SelectedValue = RoleController.Index().First(x => x.Name == "Клиент").RoleID;

            RoleID.IsEnabled = false;

            Role selectedRole = RoleID.SelectedItem as Role;
            _role = selectedRole.RoleID;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Зарегистироваться"
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Событие</param>
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
