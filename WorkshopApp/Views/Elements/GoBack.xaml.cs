using System.Windows;
using System.Windows.Controls;

namespace WorkshopApp.Views.Elements
{
    public partial class GoBack : UserControl
    {
        public GoBack()
        {
            InitializeComponent();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this) as MainWindow;

            window?.GoBack();
        }
    }
}
