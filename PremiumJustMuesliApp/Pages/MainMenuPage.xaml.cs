using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PremiumJustMuesliApp.Pages
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void BOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderPage());
        }

        private void BMyMuesliMixes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyMuesliMixesPage());
        }

        private void BMix_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MuesliMixerPage());
        }

        private void BEditCustomerDetails_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditCustomerDetailsPage());
        }
        private void BExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
