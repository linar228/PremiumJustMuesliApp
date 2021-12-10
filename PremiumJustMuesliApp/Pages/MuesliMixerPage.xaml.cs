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
    /// Interaction logic for MuesliMixerPage.xaml
    /// </summary>
    public partial class MuesliMixerPage : Page
    {
        public MuesliMixerPage()
        {
            InitializeComponent();
            foreach (var c in MainWindow.db.Type.ToList())
            {
                TCMuesliMenu.Items.Add(c);
            }
            for (int i = 0; i < 12; i++)
            {
                listViewSelectedMuesliPerfomances.Push(new ListViewSelectedMuesliPerfomance());
            }
            LVSelectedMuesliMenu.ItemsSource = listViewSelectedMuesliPerfomances;


        }
        public MuesliMixerPage(Model.MuesliMix muesliMix)
        {
            InitializeComponent();
            foreach (var c in MainWindow.db.Type.ToList())
            {
                TCMuesliMenu.Items.Add(c);
            }
            for (int i = 0; i < 12; i++)
            {
                listViewSelectedMuesliPerfomances.Push(new ListViewSelectedMuesliPerfomance());
            }
            selectedMueslis = muesliMix.MuesliMixMuesli.Select(c => c.Muesli).ToList();
            basicMuesli = selectedMueslis[0];

            basicMuesli.Grams = selectedMueslis[0].PortionSize;
            TBBasicMuesliName.Text = basicMuesli.NameEN;
            TBBasicMuesliSize.Text = basicMuesli.Grams.ToString();
            for (int i = 1; i < selectedMueslis.Count - 1; i++)
            {
                var selectedMuesli = selectedMueslis[i];
                if (stackCounter == selectedMueslis.Count - 1)
                {
                    stackCounter = 0;
                    basicMuesli = null;
                    selectedMueslis = new List<Model.Muesli>();
                }
                EditRefresh(selectedMuesli);
                stackCounter++;
            }
        }
        Stack<ListViewSelectedMuesliPerfomance> listViewSelectedMuesliPerfomances = new Stack<ListViewSelectedMuesliPerfomance>();
        List<Model.Muesli> selectedMueslis = new List<Model.Muesli>();
        Model.Muesli basicMuesli;
        int stackCounter = 0;
        private void BAddMuesli_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Model.Muesli selectedMuesli = (Model.Muesli)button.DataContext;
            if (stackCounter == 12)
            {
                stackCounter = 0;
                basicMuesli = null;
                selectedMueslis = new List<Model.Muesli>();
            }
            if (basicMuesli == null)
            {
                if (stackCounter == 0)
                {
                    if (selectedMueslis.Contains(selectedMuesli))
                    {
                        MessageBox.Show("Select Another Muesli");
                        return;
                    }
                    if (selectedMuesli.Type.Name != "Basics")
                    {
                        MessageBox.Show("Select Basic Muesli");
                        return;
                    }

                    basicMuesli = selectedMuesli;
                    basicMuesli.Grams = basicMuesli.PortionSize;
                    TBBasicMuesliName.Text = basicMuesli.NameEN;
                    TBBasicMuesliSize.Text = basicMuesli.Grams.ToString();
                    selectedMueslis.Add(basicMuesli);
                    return;
                }
            }
            if (selectedMuesli.Type.Name == "Basics")
            {
                MessageBox.Show("Select Extra Muesli");
                return;
            }
            if (selectedMueslis.Contains(selectedMuesli))
            {
                MessageBox.Show("Select Another Muesli");
                return;
            }
            Refresh(selectedMuesli);
            stackCounter++;
        }
        private void Refresh(Model.Muesli selectedMuesli)
        {
            listViewSelectedMuesliPerfomances.ElementAt(stackCounter).PortionSize = selectedMuesli.PortionSize;
            listViewSelectedMuesliPerfomances.ElementAt(stackCounter).muesli = selectedMuesli;
            listViewSelectedMuesliPerfomances.ElementAt(stackCounter).Name = selectedMuesli.NameEN;
            LVSelectedMuesliMenu.ItemsSource = null;
            LVSelectedMuesliMenu.ItemsSource = listViewSelectedMuesliPerfomances;
            selectedMuesli.Grams = selectedMuesli.PortionSize;
            selectedMueslis.Add(selectedMuesli);
            basicMuesli.Grams -= selectedMuesli.PortionSize;
            if (basicMuesli.Price / basicMuesli.Grams <= 0)
            {
                MessageBox.Show("Max Musli size is 600 grams");
                stackCounter = 12;
                return;
            }
            basicMuesli.PricePerGram = basicMuesli.Price / basicMuesli.Grams;
            selectedMueslis[0] = basicMuesli;
            TBBasicMuesliSize.Text = basicMuesli.Grams.ToString();
        }
        private void EditRefresh(Model.Muesli selectedMuesli)
        {
            listViewSelectedMuesliPerfomances.ElementAt(stackCounter).PortionSize = selectedMuesli.PortionSize;
            listViewSelectedMuesliPerfomances.ElementAt(stackCounter).muesli = selectedMuesli;
            listViewSelectedMuesliPerfomances.ElementAt(stackCounter).Name = selectedMuesli.NameEN;
            LVSelectedMuesliMenu.ItemsSource = null;
            LVSelectedMuesliMenu.ItemsSource = listViewSelectedMuesliPerfomances;
            selectedMuesli.Grams = selectedMuesli.PortionSize;
            basicMuesli.Grams -= selectedMuesli.PortionSize;
            if (basicMuesli.Price / basicMuesli.Grams <= 0)
            {
                MessageBox.Show("Max Musli size is 600 grams");
                stackCounter = 12;
                return;
            }
            basicMuesli.PricePerGram = basicMuesli.Price / basicMuesli.Grams;
            selectedMueslis[0] = basicMuesli;
            TBBasicMuesliSize.Text = basicMuesli.Grams.ToString();
        }
        private void BDetails_Click(object sender, RoutedEventArgs e)
        {
            double Carbohydrates = 0, Proteins = 0, Fats = 0;
            foreach (var c in selectedMueslis)
            {
                double portion = c.PortionSize;
                double carbohydratesPerGramm = c.Carbohydrates / portion;
                double proteinsPerGramm = c.Protein / portion;
                double fatsPerGramm = c.Fat / portion;
                Carbohydrates += carbohydratesPerGramm * c.Grams;
                Proteins += proteinsPerGramm * c.Grams;
                Fats += fatsPerGramm * c.Grams;
            }
            MessageBox.Show($"Carbohydrates: {Math.Round(Carbohydrates, 2)}\nProteins: {Math.Round(Proteins, 2)}\nFats: {Math.Round(Fats, 2)}");
            Carbohydrates = 0;
            Proteins = 0;
            Fats = 0;
        }

        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BSaveMuesli_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBName.Text))
            {
                MessageBox.Show("Put Muesli Mix Name");
                return;
            }
            double price = 0;
            Model.MuesliMix muesliMix = new Model.MuesliMix() { UserId = MainWindow.user.Id, Name = TBName.Text, CreatedDate = DateTime.Now };
            MainWindow.db.MuesliMix.Add(muesliMix);
            MainWindow.db.SaveChanges();
            double muesliPerGramm = 0;
            muesliPerGramm += basicMuesli.Price / Convert.ToDouble(TBBasicMuesliSize.Text);
            foreach (var c in selectedMueslis)
            {
                Model.MuesliMixMuesli muesliMixMuesli = new Model.MuesliMixMuesli() { MuesliId = c.Id, MuesliMixId = muesliMix.Id, Grams = Convert.ToInt32(c.Grams) };
                MainWindow.db.MuesliMixMuesli.Add(muesliMixMuesli);
                price += Math.Round(c.Price / c.PortionSize * c.Grams, 2);
            }
            muesliMix.Price = price;
            MainWindow.db.SaveChanges();
            NavigationService.GoBack();
        }
    }
    class ListViewSelectedMuesliPerfomance
    {
        public string Name { get; set; } = "empty";
        public Model.Muesli muesli { get; set; } = null;
        public int PortionSize { get; set; } = 0;
        public ListViewSelectedMuesliPerfomance()
        {

        }
        public ListViewSelectedMuesliPerfomance(string Name, Model.Muesli muesli)
        {
            this.Name = Name;
            this.muesli = muesli;
        }

    }
}
