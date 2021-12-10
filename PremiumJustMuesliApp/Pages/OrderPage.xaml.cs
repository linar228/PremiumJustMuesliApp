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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        List<Model.MuesliMix> muesliMixes = MainWindow.db.MuesliMix.Where(c => c.UserId == MainWindow.user.Id).ToList();
        public OrderPage()
        {
            InitializeComponent();
            for (int i = 0; i < muesliMixes.Count; i++)
            {
                muesliMixes[i].TotalPrice = Math.Round(muesliMixes[i].Price * muesliMixes[i].Quantity, 2);
            }
            LVOrderMuesliMix.DataContext = muesliMixes;
            RefreshCost();
        }
        private void BSubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            Model.Order order = new Model.Order() { TotalPrice = Math.Round(Convert.ToDouble(TBGrandTotalCost.Text), 2), OrderDate = DateTime.Now, UserId = MainWindow.user.Id };
            MainWindow.db.Order.Add(order);
            MainWindow.db.SaveChanges();
            foreach(var c in muesliMixes)
            {
                Model.OrderMuesliMixes orderMuesliMixes = new Model.OrderMuesliMixes() { OrderId = order.Id, MuesliMixId = c.Id};
                MainWindow.db.OrderMuesliMixes.Add(orderMuesliMixes);
            }
            MainWindow.db.SaveChanges();
            MessageBox.Show($"Order ID: {order.Id}\nDate: {order.OrderDate}\nUser: {MainWindow.user.Name}\nTotal Price: {order.TotalPrice}");
            NavigationService.GoBack();
        }
        private void LVOrderMuesliMix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int result;
            if (!int.TryParse(textBox.Text, out result) || Convert.ToInt32(textBox.Text) <= 0)
            {
                MessageBox.Show("Quantity Is Digit");
                textBox.Text = "1";
                return;
            }
            Model.MuesliMix muesliMixSelectedItem = (Model.MuesliMix)textBox.DataContext;
            muesliMixSelectedItem.Quantity = Convert.ToInt32(textBox.Text);
            if(muesliMixSelectedItem.SizeXXL == true)
            {
                muesliMixSelectedItem.TotalPrice = Math.Round(muesliMixSelectedItem.Price * muesliMixSelectedItem.Quantity*4, 2);

            }
            else
            {
                muesliMixSelectedItem.TotalPrice = Math.Round(muesliMixSelectedItem.Price * muesliMixSelectedItem.Quantity, 2);
            }
            LVOrderMuesliMix.DataContext = null;
            LVOrderMuesliMix.DataContext = muesliMixes;
            RefreshCost();
        }
        double MuesliCost = 0;
        double ShippingCost = 0;
        double TaxesCost = 0;
        double GrandTotalCost = 0;
        private void RefreshCost()
        {
            GrandTotalCost = 0;
            MuesliCost = 0;
            TaxesCost = 0;
            ShippingCost = 0;
            foreach (var c in muesliMixes)
            {
                MuesliCost += c.TotalPrice;
            }
            TaxesCost += MuesliCost * 2.5 / 100;
            if ((MuesliCost + TaxesCost) < 50 && (MuesliCost + TaxesCost) != 0)
            {
                ShippingCost = 8;
            }
            GrandTotalCost = MuesliCost + TaxesCost + ShippingCost;

            TBMuesliCost.Text = Convert.ToString(Math.Round(MuesliCost, 2));
            TBTaxesCost.Text = Convert.ToString(Math.Round(TaxesCost, 2));
            TBShippingCost.Text = Convert.ToString(Math.Round(ShippingCost, 2));
            TBGrandTotalCost.Text = Convert.ToString(Math.Round(GrandTotalCost, 2));
        }
        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            ((Model.MuesliMix)checkBox.DataContext).SizeXXL = true;
            ((Model.MuesliMix)checkBox.DataContext).TotalPrice = Math.Round(((Model.MuesliMix)checkBox.DataContext).TotalPrice * 4, 2);
            LVOrderMuesliMix.DataContext = null;
            LVOrderMuesliMix.DataContext = muesliMixes;
            RefreshCost();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            ((Model.MuesliMix)checkBox.DataContext).SizeXXL = false;

            ((Model.MuesliMix)checkBox.DataContext).TotalPrice = Math.Round(((Model.MuesliMix)checkBox.DataContext).TotalPrice / 4, 2);
            LVOrderMuesliMix.DataContext = null;
            LVOrderMuesliMix.DataContext = muesliMixes;
            RefreshCost();
        }
    }
}
