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
    /// Логика взаимодействия для ScheldueStaff.xaml
    /// </summary>
    public partial class ScheldueStaff : Window
    {
        BasicButtons basicButtons = new BasicButtons();
        private CarRepairEntities5 context = new CarRepairEntities5();


        public ScheldueStaff()
        {
            InitializeComponent();

            StaffSchedule.ItemsSource = context.ScheduleStaffs.ToList();

            OrdersCmbx.ItemsSource = context.OrderCars.ToList();
            OrdersCmbx.DisplayMemberPath = "ListOfWorks";

            StaffCmbx.ItemsSource = context.Staffs.ToList();
            StaffCmbx.DisplayMemberPath = "NameSaff";
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var staff = StaffCmbx.SelectedItem as Staff;
                var order = OrdersCmbx.SelectedItem as OrderCar;
                if (staff != null && order != null)
                {
                    ScheduleStaff scheduleStaff = new ScheduleStaff();
                    scheduleStaff.Staff_ID = staff.ID_Staff;
                    scheduleStaff.Order_ID = order.ID_Order;


                    context.ScheduleStaffs.Add(scheduleStaff);
                    context.SaveChanges();
                    StaffSchedule.ItemsSource = context.ScheduleStaffs.ToList();
                }
                else
                {
                    MessageBox.Show("Ошибка Добавления, заполните все данные");
                }

            }
            catch {

                MessageBox.Show("Ошибка Добавления, заполните все данные");
            
            }


        }

        private void EditBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StaffSchedule.SelectedItem != null)
                {
                    var selected = StaffCmbx.SelectedItem as ScheduleStaff;

                    var staff = StaffCmbx.SelectedItem as Staff;
                    var order = OrdersCmbx.SelectedItem as OrderCar;



                    if (staff != null && order != null)
                    {

                        selected.Staff_ID = staff.ID_Staff;
                        selected.Order_ID = order.ID_Order;

                        context.SaveChanges();
                        StaffSchedule.ItemsSource = context.ScheduleStaffs.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка Добавления, заполните все данные");

                    }
                }
            }

            catch
            {
                MessageBox.Show("Ошибка Изменения");
            }
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StaffSchedule.SelectedItem != null)
                {

                    
                    context.ScheduleStaffs.Remove(StaffSchedule.SelectedItem as ScheduleStaff);
                    context.SaveChanges();
                    StaffSchedule.ItemsSource = context.ScheduleStaffs.ToList();

                }
            }

            catch
            {
                MessageBox.Show("Ошибка удаления, данные используются");
            }
        }

        private void ExitBut_Click(object sender, RoutedEventArgs e)
        {
            int role = GlobalVariables.RoleUser;
            Window newwindow = basicButtons.Exit(role);
            newwindow.Show();
            this.Close();
        }

        private void RepairHistoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
