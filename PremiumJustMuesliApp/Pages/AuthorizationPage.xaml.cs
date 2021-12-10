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
            List<Model.User> allUsers = MainWindow.db.User.ToList();
            Model.User user = allUsers.FirstOrDefault(c => c.Login == TBLogin.Text && c.Password == PBPassword.Password);

            if(user is null)
            {
                MessageBox.Show("Unkown User");
                return;
            }
            MainWindow.user = user;
            NavigationService.Navigate(new MainMenuPage());
        }
    }
}
