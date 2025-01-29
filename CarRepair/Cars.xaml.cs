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

    public partial class Cars : Window
    {

        BasicButtons basicButtons = new BasicButtons();
        private CarRepairEntities6 context = new CarRepairEntities6();

        public Cars()
        {
            InitializeComponent();
            CarGrid.ItemsSource = context.Cars.ToList();


            MarkBox.ItemsSource = context.Marks.ToList();
            MarkBox.DisplayMemberPath = "NameMark";

            ModelBox.ItemsSource = context.ModelCars.ToList();
            ModelBox.DisplayMemberPath = "NameModel";

            ClientBox.ItemsSource = context.Clients.ToList();
            ClientBox.DisplayMemberPath = "NameClient";

        }

        private void ExitBut_Click(object sender, RoutedEventArgs e)
        {
            int role = GlobalVariables.RoleUser;
            Window newwindow = basicButtons.Exit(role);
            newwindow.Show();
            this.Close();
        }

        private void NumberCar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void NumberCar_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var mark = MarkBox.SelectedItem as Mark;
                var model = ModelBox.SelectedItem as ModelCar;
                var client = ClientBox.SelectedItem as Client;

                if (client != null && model != null && client != null)
                {
                    Car car = new Car();

                    car.Mark_ID = mark.ID_Mark;
                    car.ModelCar_ID = model.ID_Model;
                    car.Client_ID = client.ID_Client;
                    car.NumberCar = NumberCar.Text;

                    context.Cars.Add(car);
                    context.SaveChanges();
                    CarGrid.ItemsSource = context.Cars.ToList();
                }
                else
                {
                    MessageBox.Show("Заполните все данные");

                }


            }
            catch {

                MessageBox.Show("Заполните все данные");


            }
        }

        private void EditBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CarGrid.SelectedItem != null)
                {
                    var selected = CarGrid.SelectedItem as Car;

                    var mark = MarkBox.SelectedItem as Mark;
                    var model = ModelBox.SelectedItem as ModelCar;
                    var client = ClientBox.SelectedItem as Client;
                    if (client != null && model != null && client != null)
                    {
                        selected.Mark_ID = mark.ID_Mark;
                        selected.ModelCar_ID = model.ID_Model;
                        selected.Client_ID = client.ID_Client;
                        selected.NumberCar = NumberCar.Text;

                        context.SaveChanges();
                        CarGrid.ItemsSource = context.Cars.ToList();

                    }
                    else
                    {
                        MessageBox.Show("Ошибка добавления");

                    }

                }
            }
            catch {
                MessageBox.Show("Ошибка добавления");
            }
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(CarGrid.SelectedItem != null)
                {
                    context.Cars.Remove(CarGrid.SelectedItem as Car);

                    context.SaveChanges();
                    CarGrid.ItemsSource = context.Cars.ToList();

                }
            }
            catch
            {
                MessageBox.Show("Ошибка удаления данные используются");

            }
        }

        private void CarGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarGrid.SelectedItem != null)
            {
                var selected = CarGrid.SelectedItem as Car;

                NumberCar.Text = selected.NumberCar;


            }
        }

    }
}
