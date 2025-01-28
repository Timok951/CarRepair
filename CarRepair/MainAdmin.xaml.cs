using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
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
    /// Логика взаимодействия для MainAdmin.xaml
    /// </summary>
    public partial class MainAdmin : Window
    {
        public MainAdmin()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RolesButton_Click(object sender, RoutedEventArgs e)
        {
            Role role = new Role();
            role.Show();
            this.Close();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            Orders order = new Orders();
            order.Show();
            this.Close();
        }

        private void ClientsBut_Click(object sender, RoutedEventArgs e)
        {
            Clients client = new Clients();
            client.Show();
            this.Close();
        }

        private void СlientsBut2_Click(object sender, RoutedEventArgs e)
        {
            ClientsCompany clients = new ClientsCompany();
            clients.Show();
            this.Close();
        }

        private void CarsBut_Click(object sender, RoutedEventArgs e)
        {
            Cars car = new Cars();
            car.Show();
            this.Close();
        }

        private void RepairHistoryBut_Click(object sender, RoutedEventArgs e)
        {
            RepairHistory repairHistory = new RepairHistory();
            repairHistory.Show();
            this.Close();
        }

        private void PartsBut_Click(object sender, RoutedEventArgs e)
        {
            Parts part = new Parts();
            part.Show();
            this.Close();
        }

        private void STOBut_Click(object sender, RoutedEventArgs e)
        {
            StoWindow1 sto = new StoWindow1();
            sto.Show();
            this.Close();
        }

        private void ScheldueWorkers_Click(object sender, RoutedEventArgs e)
        {
            ScheldueStaff schldue = new ScheldueStaff();
            schldue.Show();
            this.Close();
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
            this.Close();
        }

        private void Workers_Click(object sender, RoutedEventArgs e)
        {
            Workers workers = new Workers();
            workers.Show();
            this.Close();
        }
    }
}
