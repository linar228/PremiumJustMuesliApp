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
using MuesliCore.ViewModels;

namespace PremiumJustMuesliApp.Pages
{
    /// <summary>
    /// Interaction logic for MuesliMixerPage.xaml
    /// </summary>
    public partial class MuesliMixerPage : Page
    {
        public MuesliMixerPage()
        {
            InitializeComponent();
            //Дублирование кода - нужно исправить
            CbBasics.ItemsSource = DBConnect.GetIngredientsByType(1);
            CbCereal.ItemsSource = DBConnect.GetIngredientsByType(2);
            CbChoco.ItemsSource = DBConnect.GetIngredientsByType(3);
            CbFruit.ItemsSource = DBConnect.GetIngredientsByType(4);
            CbNuts.ItemsSource = DBConnect.GetIngredientsByType(5);
            CbSpecials.ItemsSource = DBConnect.GetIngredientsByType(6);
            this.DataContext = this;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Проверка полноты данных - Хорошее решение
            if (tbName.Text == "")
            {
                MessageBox.Show("Введите название микса");
                return;
            }
            DBConnect.AddMuesliMix(new MixModel() 
            {
                Name = tbName.Text,
                Ingredients = new int[6] 
                {
                    (CbBasics.SelectedItem as Ingredient).ID,
                    (CbCereal.SelectedItem as Ingredient).ID,
                    (CbFruit.SelectedItem as Ingredient).ID,
                    (CbNuts.SelectedItem as Ingredient).ID,
                    (CbChoco.SelectedItem as Ingredient).ID,
                    (CbSpecials.SelectedItem as Ingredient).ID
                }
            });
            MessageBox.Show("Микс добавлен");
        }
    }
}
