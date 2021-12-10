using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for EditCustomerDetailsPage.xaml
    /// </summary>
    public partial class EditCustomerDetailsPage : Page
    {
        public EditCustomerDetailsPage()
        {
            InitializeComponent();
            CBCountry.ItemsSource = MainWindow.db.Country.ToList();
            this.DataContext = MainWindow.user;
        }


        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                MainWindow.db.SaveChanges();
                MessageBox.Show("Changes saved");
                NavigationService.GoBack();
            }
        }
        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        public bool Validate()
        {
            string result = "";
            if (string.IsNullOrWhiteSpace(TBName.Text))
            {
                result += "Type Name\n";
            }
            if (string.IsNullOrWhiteSpace(TBAddress.Text))
            {
                result += "Type Address\n";
            }
            if (string.IsNullOrWhiteSpace(TBZip.Text))
            {
                result += "Type Zip Code\n";
            }
            if (string.IsNullOrWhiteSpace(TBCity.Text))
            {
                result += "Type City\n";
            }
            if (string.IsNullOrWhiteSpace(TBPhone.Text))
            {
                result += "Type Phone Number";
            }
            if (string.IsNullOrWhiteSpace(TBEmail.Text))
            {
                result += "Type Email";
            }
            if (TBName.Text.Length < 5)
            {
                result += "Name must be more than 5 characters\n";
            }
            if (TBAddress.Text.Length < 5)
            {
                result += "Address must be more than 5 characters\n";
            }
            if (TBZip.Text.Length < 4)
            {
                result += "Zip must be more than 5 characters\n";
            }
            int res;
            if (int.TryParse(TBZip.Text, out res) == false)
            {
                result += "Zip can only contain digits\n";
            }
            if (TBPhone.Text.Length < 10)
            {
                result += "Phone must be more than 10 characters\n";
            }
            EmailAddressAttribute foo = new EmailAddressAttribute();
            if (!foo.IsValid(TBEmail.Text))
            {
                result += "It's Not Email Address";
            }
            if (result != "")
            {
                MessageBox.Show(result);
                return false;
            }
            return true;
        }
    }
}
