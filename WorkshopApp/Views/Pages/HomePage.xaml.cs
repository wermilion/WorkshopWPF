using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WorkshopApp.Controllers;
using WorkshopApp.Enums;

namespace WorkshopApp.Views.Pages
{
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

        public HomePage()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (App.authUser.RoleID == (int)RoleEnum.INTERN || App.authUser.RoleID == (int)RoleEnum.CLIENT)
            {
                ActionButtons.Visibility = Visibility.Hidden;
            }

            if (App.authUser.RoleID == (int)RoleEnum.ADMINISTRATOR)
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

        private void BtnCreateClick(object sender, RoutedEventArgs e)
        {
            switch (_selectedTabItem.Header)
            {
                case "Пользователи":

                    break;

                case "Заказы":
                    break;

                case "Типы":
                    break;

                case "Доп. услуги":
                    break;
            }
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl tabControl)
            {
                SelectedTabItem = tabControl.SelectedItem as TabItem;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
