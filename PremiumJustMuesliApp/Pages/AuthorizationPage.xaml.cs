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
using Dapper;
using MuesliCore.ViewModels;

namespace PremiumJustMuesliApp.Pages
{
    /// <summary>
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void BAuthorzationPage_Click(object sender, RoutedEventArgs e)
        {
            if (DBConnect.IsLoginCorrect(new LoginModel() {Email = TBLogin.Text,Password = PBPassword.Password }))
            {
                NavigationService.Navigate(new MainMenuPage());
            }
            else
            {
                MessageBox.Show("Unkown User");
                return;
            }
            
        }
    }
}
