using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WorkshopApp.Controllers;
using WorkshopApp.Models;
using WorkshopApp.Models.Validators;

namespace WorkshopApp.Views.Pages.Services
{
    /// <summary>
    /// Страница создания доп. услуги
    /// </summary>
    public partial class CreateServicePage : Page
    {
        private Service _service;

        public CreateServicePage(Service service = null)
        {
            InitializeComponent();

            if (service != null)
            {
                _service = service;
                Header.Text = "Редактирование доп. услуги";
                BtnIntrecate.Content = "Редактировать";

                Name.Text = service.Name;
                Price.Text = service.Price.ToString();
            }
        }

        private void BtnIntrecateClick(object sender, RoutedEventArgs e)
        {
            var data = new Dictionary<string, string>
            {
                {"Name", Name.Text},
                {"Price", Price.Text},
            };

            var (isValid, errors) = ServiceValidator.Validate(data);

            if (isValid)
            {
                if (_service != null)
                {
                    ServiceController.Update(_service.ServiceID, data);
                }
                else
                {
                    ServiceController.Create(data);
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
