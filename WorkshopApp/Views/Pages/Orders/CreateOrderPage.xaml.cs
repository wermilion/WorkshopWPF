using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WorkshopApp.Controllers;
using WorkshopApp.Models;
using WorkshopApp.Models.Validators;

namespace WorkshopApp.Views.Pages.Orders
{
    /// <summary>
    /// Страница создания заказа
    /// </summary>
    public partial class CreateOrderPage : Page
    {
        private Order _order;

        public CreateOrderPage(Order order = null)
        {
            InitializeComponent();

            TypeID.DisplayMemberPath = "Name";
            TypeID.SelectedValuePath = "TypeID";
            TypeID.ItemsSource = TypeController.Index();

            StatusID.DisplayMemberPath = "Name";
            StatusID.SelectedValuePath = "StatusID";
            StatusID.ItemsSource = StatusController.Index();

            Services.DisplayMemberPath = "Name";
            Services.ItemsSource = ServiceController.Index();

            if (order != null)
            {
                _order = order;
                Header.Text = "Редактирование заказа";
                BtnInteracte.Content = "Редактировать";

                TypeID.SelectedValue = order.TypeID;
                StatusID.SelectedValue = order.StatusID;
                Date.Text = order.Date.ToString();
            }
        }

        private void BtnInteracteClick(object sender, RoutedEventArgs e)
        {
            dynamic services = Services.SelectedItems;
            string typeID = TypeID.SelectedValue != null ? TypeID.SelectedValue.ToString() : string.Empty;
            string statusID = StatusID.SelectedValue != null ? StatusID.SelectedValue.ToString() : string.Empty;

            double totalPrice = 0;
            foreach (var item in services)
            {
                totalPrice += (double)item.Price;
            }

            var data = new Dictionary<string, dynamic>
            {
                {"UserID", App.authUser.UserID},
                {"TypeID", typeID},
                {"StatusID", statusID},
                {"Date", Date.SelectedDate.Value.Date},
                {"CountServices", services.Count},
                {"TotalPrice", totalPrice},
            };

            var (isValid, errors) = OrderValidator.Validate(data);

            if (isValid)
            {
                if (_order != null)
                {
                    OrderController.Update(_order.OrderID, data);
                }
                else
                {
                    Order order = OrderController.Create(data);
                    CreateOrderServices(services, order);
                }
                NavigationService.GoBack();
            }
            else
            {
                var errorMessage = string.Join("\n", errors);
                MessageBox.Show(errorMessage, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Создает записи в промежуточной таблице OrderServices
        /// </summary>
        /// <param name="services">Коллекция доп. услуг</param>
        /// <param name="order">Заказ, с которым будут связываться доп. услуги</param>
        private void CreateOrderServices(dynamic services, Order order)
        {
            foreach (var service in services)
            {
                order.Services.Add(service);
            }
            Connection.db.SaveChanges();
        }
    }
}