using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WorkshopApp.Controllers;
using WorkshopApp.Models;
using WorkshopApp.Models.Validators;

namespace WorkshopApp.Views.Pages.Types
{
    /// <summary>
    /// Страница создания типа заказа
    /// </summary>
    public partial class CreateTypePage : Page
    {
        private Type _type;
        public CreateTypePage(Type type = null)
        {
            InitializeComponent();

            if (type != null)
            {
                _type = type;
                Header.Text = "Редактирование типа заказа";
                BtnIntrecate.Content = "Редактировать";

                Name.Text = type.Name;
            }
        }

        private void BtnIntrecateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var data = new Dictionary<string, string>
            {
                {"Name", Name.Text},
            };

            var (isValid, errors) = TypeValidator.Validate(data);

            if (isValid)
            {
                if (_type != null)
                {
                    TypeController.Update(_type.TypeID, data);
                }
                else
                {
                    TypeController.Create(data);
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
