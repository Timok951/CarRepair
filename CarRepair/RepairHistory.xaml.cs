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
    /// Логика взаимодействия для RepairHistory.xaml
    /// </summary>
    public partial class RepairHistory : Window
    {

        BasicButtons basicButtons = new BasicButtons();
        private CarRepairEntities5 context = new CarRepairEntities5();

        public RepairHistory()
        {
            InitializeComponent();
            RepairHistoryGrid.ItemsSource = context.RepeatHistories.ToList();

            OrdersCmbx.ItemsSource = context.OrderCars.ToList();
            OrdersCmbx.DisplayMemberPath = "ListOfWorks";


            StaffCmbx.ItemsSource = context.Staffs.ToList();
            StaffCmbx.DisplayMemberPath = "NameSaff";

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
                RepeatHistory repeatHistory = new RepeatHistory();

                var order = OrdersCmbx.SelectedItem as OrderCar; ;
                var staff = StaffCmbx.SelectedItem as Staff;

                repeatHistory.ListOfRepair = ListofWork.Text;
                repeatHistory.Staff_ID = staff.ID_Staff;
                repeatHistory.Orders_ID = order.ID_Order;

                context.RepeatHistories.Add(repeatHistory);
                context.SaveChanges();
                RepairHistoryGrid.ItemsSource = context.RepeatHistories.ToList();
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
                if(RepairHistoryGrid.SelectedItem != null)
                {
                    var selected = RepairHistoryGrid.SelectedItem as RepeatHistory;

                    var order = OrdersCmbx.SelectedItem as OrderCar; ;
                    var staff = StaffCmbx.SelectedItem as Staff;

                    selected.ListOfRepair = ListofWork.Text;
                    selected.Staff_ID = staff.ID_Staff;
                    selected.Orders_ID = order.ID_Order;


                    context.SaveChanges();
                    RepairHistoryGrid.ItemsSource = context.RepeatHistories.ToList();
                }

            }

            catch
            {
                MessageBox.Show("Ошибка обновления");
            }
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RepairHistoryGrid.SelectedItem != null)
                {
                    var selected = RepairHistoryGrid.SelectedItem as RepeatHistory;
                    context.RepeatHistories.Remove(selected);

                    context.SaveChanges();
                    RepairHistoryGrid.ItemsSource = context.RepeatHistories.ToList();

                }
            }
            catch
            {
                MessageBox.Show("Ошибка удаления");
            }
        }

        private void RepairHistoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RepairHistoryGrid.SelectedItem != null)
            {
                var selected = RepairHistoryGrid.SelectedItem as RepeatHistory;
                ListofWork.Text = selected.ListOfRepair;

            }

        }
    }
}
