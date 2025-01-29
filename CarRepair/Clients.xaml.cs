using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
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
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        BasicButtons basicButtons = new BasicButtons();
        private CarRepairEntities6 context = new CarRepairEntities6();

        public Clients()
        {
            InitializeComponent();
            ClientGrid.ItemsSource = context.Clients.ToList();
        }

        private void ExitBut_Click(object sender, RoutedEventArgs e)
        {
            int role = GlobalVariables.RoleUser;
            Window newwindow = basicButtons.Exit(role);
            newwindow.Show();
            this.Close();
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client client = new Client();
                client.NameClient = NameBox.Text;
                client.SurnameClient = SurnameBox.Text;
                client.PatronymiClient = PatrinimiBox.Text;
                client.TypeClient_ID = 2;
                client.NameCompany = null;
                client.AddressCompany = null;

                context.Clients.Add(client);

                context.SaveChanges();
                ClientGrid.ItemsSource = context.Clients.ToList();

            }
            catch 
            {
                MessageBox.Show("Введите все данные");
            }
        }

        private void EditBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientGrid.SelectedItem != null) { 
                
                    var selected = ClientGrid.SelectedItem as Client;

                    selected.NameClient = NameBox.Text;
                    selected.SurnameClient = SurnameBox.Text;
                    selected.PatronymiClient = SurnameBox.Text;
                    selected.TypeClient_ID = 2;

                    context.SaveChanges();
                    ClientGrid.ItemsSource = context.Clients.ToList();

                }
            }
            catch
            {
                MessageBox.Show("Ошибка добавления");
            }
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientGrid.SelectedItem != null)
                {
                    context.Clients.Remove(ClientGrid.SelectedItem as Client);

                    context.SaveChanges();
                    ClientGrid.ItemsSource = context.Clients.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Нельзя удалить, данные используются");
            }

        }

        private void ClientGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientGrid.SelectedItem != null)
            {
                var selected = ClientGrid.SelectedItem as Client;
                NameBox.Text =  selected.NameClient.ToString();
                SurnameBox.Text = selected.SurnameClient.ToString();
                PatrinimiBox.Text = selected.PatronymiClient.ToString();
            }
        }
    }
}
