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
using System.Windows.Shapes;

namespace CarRepair
{
    /// <summary>
    /// Логика взаимодействия для Parts.xaml
    /// </summary>
    public partial class Parts : Window
    {
        private CarRepairEntities5 context = new CarRepairEntities5();

        BasicButtons basicButtons = new BasicButtons();

        private void ArtculPart_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void ArtculPart_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || (e.Key >= Key.A && e.Key <= Key.Z))
            {
                e.Handled = true;
            }
        }

        private void AmountPart_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void AmountPart_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || (e.Key >= Key.A && e.Key <= Key.Z))
            {
                e.Handled = true;
            }
        }

        private void CostPart_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void CostPart_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || (e.Key >= Key.A && e.Key <= Key.Z))
            {
                e.Handled = true;
            }
        }

        private static bool IsTextAllowed(string text)
        {
            return double.TryParse(text, out _);
        }


        public Parts()
        {
            InitializeComponent();
            PartsGrid.ItemsSource = context.SpareParts.ToList();

        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SparePart sparepart = new SparePart();
                sparepart.NameSparePart = NamePart.Text;
                sparepart.ArticleSparePart = Convert.ToInt32(ArtculPart.Text);
                sparepart.QuantityInStock = Convert.ToInt32(AmountPart.Text);
                sparepart.PriceSparePart = Convert.ToInt32(CostPart.Text);

                context.SpareParts.Add(sparepart);
                context.SaveChanges();
                PartsGrid.ItemsSource = context.SpareParts.ToList();
            }
            catch
            {
                MessageBox.Show("Ошибка добавления введите все поля");
            }
        }

        private void ExitBut_Click(object sender, RoutedEventArgs e)
        {
            int role = GlobalVariables.RoleUser;
            Window newwindow = basicButtons.Exit(role);
            newwindow.Show();
            this.Close();
        }

        private void EditBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PartsGrid.SelectedItem != null)
                {
                    var selected = PartsGrid.SelectedItem as SparePart;

                    selected.NameSparePart = NamePart.Text;
                    selected.ArticleSparePart = Convert.ToInt32(ArtculPart.Text);
                    selected.QuantityInStock = Convert.ToInt32(AmountPart.Text);
                    selected.PriceSparePart = Convert.ToInt32(CostPart.Text);

                    context.SaveChanges();
                    PartsGrid.ItemsSource = context.SpareParts.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка изменения");
            }
        }

        private void RemoveBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PartsGrid.SelectedItem != null)
                {
                    var selected = PartsGrid.SelectedItem as SparePart;
                    context.SpareParts.Remove(selected);
                    context.SaveChanges();
                    PartsGrid.ItemsSource = context.SpareParts.ToList();

                }
            }

            catch
            {

                MessageBox.Show("Ошибка удаления, данные используются");

            }
        }

        private void PartsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PartsGrid.SelectedItem != null)
            {
                var selected = PartsGrid.SelectedItem as SparePart;

                NamePart.Text = selected.NameSparePart;
                ArtculPart.Text = selected.ArticleSparePart.ToString();
                AmountPart.Text = selected.ArticleSparePart.ToString();
                CostPart.Text = selected.PriceSparePart.ToString();

            }
        }
    }
}
