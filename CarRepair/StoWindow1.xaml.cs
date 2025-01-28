using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRepair
{
    /// <summary>
    /// Логика взаимодействия для StoWindow1.xaml
    /// </summary>
    public partial class StoWindow1 : Window
    {
        BasicButtons basicButtons = new BasicButtons();
        private CarRepairEntities5 context = new CarRepairEntities5();

        public StoWindow1()
        {
            InitializeComponent();
            STOgrid.ItemsSource = context.STOes.ToList();

            Workload.ItemsSource = context.WorkloadCars.ToList();
            Workload.DisplayMemberPath = "NameWorkload";

        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                STO sto = new STO();
                var workload = Workload.SelectedItem as WorkloadCar;

                sto.AddressSTO = AddresSTO.Text;
                sto.ScheduleSTO = ScheldueSto.Text;
                sto.AmountPlaces = Convert.ToInt32(AmountOfPlaces.Text);
                sto.PhoneNumber = PhoneNumber.Text;
                sto.ProfitSTO = Convert.ToDecimal(ProfitSTO.Text);
                sto.Workload_ID = workload.ID_Workload;

                context.STOes.Add(sto);
                context.SaveChanges();
                STOgrid.ItemsSource = context.STOes.ToList();

            }
            catch
            {
                MessageBox.Show("Ошибка добавления данных, заполните все поля");
                
            }
        }

        private void ProfitSTO_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void ProfitSTO_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || (e.Key >= Key.A && e.Key <= Key.Z))
            {
                e.Handled = true;
            }
        }

        private void AmountPlaces_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void AmountPlaces_PreviewKeyDown(object sender, KeyEventArgs e)
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
                if(STOgrid.SelectedItem != null)
                {
                    var selected = STOgrid.SelectedItem as STO;

                    var workload = Workload.SelectedItem as WorkloadCar;

                    selected.AddressSTO = AddresSTO.Text;
                    selected.ScheduleSTO = ScheldueSto.Text;
                    selected.AmountPlaces = Convert.ToInt32(AmountOfPlaces.Text);
                    selected.PhoneNumber = PhoneNumber.Text;
                    selected.ProfitSTO = Convert.ToDecimal(ProfitSTO.Text);
                    selected.Workload_ID = workload.ID_Workload;

                    context.SaveChanges();
                    STOgrid.ItemsSource = context.STOes.ToList();
                }
            }

            catch
            {
                MessageBox.Show("Ошиьбка изменения");
            }
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            if (STOgrid.SelectedItem != null)
            {

                context.STOes.Remove(STOgrid.SelectedItem as STO);
                context.SaveChanges();
                STOgrid.ItemsSource = context.STOes.ToList();
            }
        }

        private void STOgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (STOgrid.SelectedItem != null)
            {
                var selected = STOgrid.SelectedItem as STO;

                AddresSTO.Text = selected.AddressSTO.ToString();
                ScheldueSto.Text = selected.ScheduleSTO.ToString();
                AmountOfPlaces.Text = selected.AmountPlaces.ToString();
                PhoneNumber.Text = selected.PhoneNumber.ToString();
                ProfitSTO.Text = selected.ProfitSTO.ToString();


            }
        }
    }
}
