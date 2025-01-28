using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Role.xaml
    /// </summary>
    public partial class Role : Window
    {
        private CarRepairEntities5 context = new CarRepairEntities5();

        BasicButtons basicButtons = new BasicButtons();

        public Role()
        {
            InitializeComponent();
            RoleGrid.ItemsSource = context.RoleStaffs.ToList();

        }

        private void RoleGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleGrid.SelectedItem != null)
            {
                var selected = RoleGrid.SelectedItem as RoleStaff;


                RoleTextBox.Text = Convert.ToString(selected.NameRole);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                RoleStaff role = new RoleStaff();
                role.NameRole = RoleTextBox.Text;
                context.RoleStaffs.Add(role);
                context.SaveChanges();
                RoleGrid.ItemsSource = context.RoleStaffs.ToList();
            }
            catch
            {
                MessageBox.Show("Добавьте данные");
            }

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RoleGrid.SelectedItem != null)
                {
                    context.RoleStaffs.Remove(RoleGrid.SelectedItem as RoleStaff);
                    context.SaveChanges();
                    RoleGrid.ItemsSource = context.RoleStaffs.ToList();


                }
            }
            catch
            {
                MessageBox.Show("Нельзя удалить, данные используются");
            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (RoleGrid.SelectedItem != null)
                {
                    var selected = RoleGrid.SelectedItem as RoleStaff;

                    selected.NameRole = RoleTextBox.Text;
                    context.SaveChanges();
                    RoleGrid.ItemsSource = context.RoleStaffs.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка изменения");
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            int role = GlobalVariables.RoleUser;
            Window newwindow = basicButtons.Exit(role);
            newwindow.Show();
            this.Close();



        }
    }
}
