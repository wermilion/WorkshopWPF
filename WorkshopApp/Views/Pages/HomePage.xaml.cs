using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WorkshopApp.Controllers;
using WorkshopApp.Enums;
using WorkshopApp.Models;
using WorkshopApp.Views.Pages.Orders;
using WorkshopApp.Views.Pages.Services;
using WorkshopApp.Views.Pages.Types;
using WorkshopApp.Views.Pages.Users;

namespace WorkshopApp.Views.Pages
{
    /// <summary>
    /// Домашняя страница приложения
    /// </summary>
    public partial class HomePage : Page, INotifyPropertyChanged
    {
        private TabItem _selectedTabItem;
        public TabItem SelectedTabItem
        {
            get { return _selectedTabItem; }
            set
            {
                _selectedTabItem = value;
                OnPropertyChanged(nameof(SelectedTabItem));
            }
        }

        private int? _selectedRow = null;
        public int? SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                _selectedRow = value;
                OnPropertyChanged(nameof(SelectedRow));
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При загрузке страницы будет вызван этот метод. Он заполнит страницу данными для работы
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Событие</param>
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            User authUser = App.authUser;

            if (authUser.RoleID == (int)RoleEnum.INTERN || authUser.RoleID == (int)RoleEnum.CLIENT)
            {
                ActionButtons.Visibility = Visibility.Hidden;
            }

            if (authUser.RoleID == (int)RoleEnum.ADMINISTRATOR)
            {
                DataUsers.ItemsSource = UserController.Index();
            }
            else
            {
                TabUsers.Visibility = Visibility.Collapsed;
                TabOrders.IsSelected = true;
            }

            DataOrders.ItemsSource = OrderController.Index();
            DataTypes.ItemsSource = TypeController.Index();
            DataServices.ItemsSource = ServiceController.Index();
        }

        /// <summary>
        /// Событие, которое перенаправляет на страницу создания при нажатии на кнопку `Редактировать`
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Событие</param>
        private void BtnCreateClick(object sender, RoutedEventArgs e)
        {
            switch (_selectedTabItem.Header)
            {
                case "Пользователи":
                    NavigationService.Navigate(new CreateUserPage());
                    break;

                case "Заказы":
                    NavigationService.Navigate(new CreateOrderPage());
                    break;

                case "Типы":
                    NavigationService.Navigate(new CreateTypePage());
                    break;

                case "Доп. услуги":
                    NavigationService.Navigate(new CreateServicePage());
                    break;
            }
        }

        /// <summary>
        /// Событие, которое перенаправляет на страницу редактирования выбранную запись из БД при нажатии на кнопку `Редактировать`
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Событие</param>
        private void BtnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (_selectedRow != null)
            {
                int id = (int)_selectedRow;
                switch (_selectedTabItem.Header)
                {
                    case "Пользователи":
                        NavigationService.Navigate(new CreateUserPage(UserController.Find(id)));
                        break;

                    case "Заказы":
                        NavigationService.Navigate(new CreateOrderPage(OrderController.Find(id)));
                        break;

                    case "Типы":
                        NavigationService.Navigate(new CreateTypePage(TypeController.Find(id)));
                        break;

                    case "Доп. услуги":
                        NavigationService.Navigate(new CreateServicePage(ServiceController.Find(id)));
                        break;
                }

                return;
            }

            MessageBox.Show("Для обновления записи сперва на неё нужно нажать", "Минорная ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Событие, которое удаляет выбранную запись из БД при нажатии на кнопку `Удалить`
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Событие</param>
        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            switch (_selectedTabItem.Header)
            {
                case "Пользователи":
                    DeleteUser();
                    break;

                case "Заказы":
                    DeleteOrder();
                    break;

                case "Типы":
                    DeleteType();
                    break;

                case "Доп. услуги":
                    DeleteService();
                    break;
            }

            // Обновление всех таблиц
            DataUsers.ItemsSource = Connection.db.Users.ToList();
            DataUsers.Items.Refresh();
            DataOrders.ItemsSource = Connection.db.Orders.ToList();
            DataOrders.Items.Refresh();
            DataTypes.ItemsSource = Connection.db.Types.ToList();
            DataTypes.Items.Refresh();
            DataServices.ItemsSource = Connection.db.Services.ToList();
            DataServices.Items.Refresh();
        }

        /// <summary>
        /// Метод для удаления пользователя
        /// </summary>
        private void DeleteUser()
        {
            dynamic selectedItem = DataUsers.SelectedItem;

            if (selectedItem != null && !selectedItem.Equals(CollectionView.NewItemPlaceholder))
            {
                UserController.Delete(selectedItem.UserID);
                _selectedRow = null;
                return;
            }

            MessageBox.Show("Для удаления записи сперва на неё нужно нажать", "Минорная ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Метод для удаления заказа
        /// </summary>
        private void DeleteOrder()
        {
            dynamic selectedItem = DataOrders.SelectedItem;

            if (selectedItem != null && !selectedItem.Equals(CollectionView.NewItemPlaceholder))
            {
                OrderController.Delete(selectedItem.OrderID);
                _selectedRow = null;
                return;
            }

            MessageBox.Show("Для удаления записи сперва на неё нужно нажать", "Минорная ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Метод для удаления типа заказа
        /// </summary>
        private void DeleteType()
        {
            dynamic selectedItem = DataTypes.SelectedItem;

            if (selectedItem != null && !selectedItem.Equals(CollectionView.NewItemPlaceholder))
            {
                TypeController.Delete(selectedItem.TypeID);
                _selectedRow = null;
                return;
            }

            MessageBox.Show("Для удаления записи сперва на неё нужно нажать", "Минорная ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Метод для удаления доп. услуги
        /// </summary>
        private void DeleteService()
        {
            dynamic selectedItem = DataServices.SelectedItem;

            if (selectedItem != null && !selectedItem.Equals(CollectionView.NewItemPlaceholder))
            {
                ServiceController.Delete(selectedItem.ServiceID);
                _selectedRow = null;
                return;
            }

            MessageBox.Show("Для удаления записи сперва на неё нужно нажать", "Минорная ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Событие, которое фиксирует смену таблицы
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Событие</param>
        private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl tabControl)
            {
                SelectedTabItem = tabControl.SelectedItem as TabItem;
            }
        }

        /// <summary>
        /// Событие, которое фиксирует выбранный элемент DataGrid
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Событие</param>
        private void DataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataGrid dataGrid = (DataGrid)sender;
                dynamic row = dataGrid.SelectedItem;

                if (row != null)
                {
                    switch (_selectedTabItem.Header)
                    {
                        case "Пользователи":
                            _selectedRow = row.UserID;
                            break;

                        case "Заказы":
                            _selectedRow = row.OrderID;
                            break;

                        case "Типы":
                            _selectedRow = row.TypeID;
                            break;

                        case "Доп. услуги":
                            _selectedRow = row.ServiceID;
                            break;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}