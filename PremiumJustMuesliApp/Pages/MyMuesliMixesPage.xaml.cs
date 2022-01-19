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
using MuesliCore;

namespace PremiumJustMuesliApp.Pages
{
    /// <summary>
    /// Interaction logic for MyMuesliMixesPage.xaml
    /// </summary>
	
    //Страница сделана хорошо
    public partial class MyMuesliMixesPage : Page
    {
        public MyMuesliMixesPage()
        {
            InitializeComponent();
            DGMyMuesliMixes.ItemsSource = DBConnect.GetMuesliMixes();
        }
        
        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPage());
        }

        private void BOrder_Click(object sender, RoutedEventArgs e)
        {
            if (DGMyMuesliMixes.SelectedItem != null)
            {
                DBConnect.CreateOrder(DGMyMuesliMixes.SelectedItem as MuesliMix);
                MessageBox.Show("Заказ оформлен");
            }
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DGMyMuesliMixes.SelectedItem != null)
            {
                DBConnect.RemoveMix((DGMyMuesliMixes.SelectedItem as MuesliMix).ID);
                MessageBox.Show("Микс удален");
            }
            NavigationService.Navigate(new MyMuesliMixesPage());
        }   
    }
}
