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
    /// Логика взаимодействия для СarMechanicianMain.xaml
    /// </summary>
    public partial class СarMechanicianMain : Window
    {
        public СarMechanicianMain()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Orders order = new Orders();
            order.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Clients client = new Clients();
            client.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Cars car = new Cars();
            car.Show();
            this.Close();
        }

        private void PartsBut_Click(object sender, RoutedEventArgs e)
        {
            Parts part = new Parts();
            part.Show();
            this.Close();
        }
    }
}
