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
    /// Interaction logic for MyMuesliMixesPage.xaml
    /// </summary>
    public partial class MyMuesliMixesPage : Page
    {
        public MyMuesliMixesPage()
        {
            InitializeComponent();
            this.DataContext = MainWindow.db.MuesliMix.Where(c => c.UserId == MainWindow.user.Id).ToList();
        }

        private bool ValidateSelectedItem()
        {
            if (DGMyMuesliMixes.SelectedItem is null)
            {
                MessageBox.Show("Select Item");
                return false;
            }
            return true;

        }
        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateSelectedItem())
            {
                NavigationService.Navigate(new MuesliMixerPage((Model.MuesliMix)DGMyMuesliMixes.SelectedItem));
            }
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSelectedItem())
            {
                Model.MuesliMix muesli = (Model.MuesliMix)DGMyMuesliMixes.SelectedItem;
                MainWindow.db.MuesliMix.Remove(muesli);
                MainWindow.db.SaveChanges();
                this.DataContext = null;
                this.DataContext = MainWindow.db.MuesliMix.Where(c => c.UserId == MainWindow.user.Id).ToList();
            }
        }
        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
