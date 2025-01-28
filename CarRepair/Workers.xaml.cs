using System;
using System.Collections;
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
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class Workers : Window
    {
        BasicButtons basicButtons = new BasicButtons();
        private CarRepairEntities5 context = new CarRepairEntities5();


        public Workers()
        {
            InitializeComponent();
            WorkersGrid.DataContext = context.Staffs.ToList();

            Role.ItemsSource = context.RoleStaffs.ToList();
            Role.DisplayMemberPath = "NameRole";

            Credentials.ItemsSource = context.UserCredentials.ToList();
            Credentials.DisplayMemberPath = "LoginUser";

            Qualification.ItemsSource = context.Qualifications.ToList();
            Qualification.DisplayMemberPath = "NameQualification";

        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var role = Role.SelectedItem as RoleStaff;
                var credentials = Credentials.SelectedItem as UserCredential;
                var qualification = Qualification.SelectedItem as Qualification;

                Staff staff = new Staff();
                staff.SurnameStaff = Surname.Text;
                staff.NameSaff = Name.Text;
                staff.PatronymicStaff = Patronimic.Text;
                staff.Role_ID = role.ID_Role;
                staff.Role_ID = credentials.ID_UserCredentials;
                staff.Qualification_ID = qualification.ID_Qualification;

                context.Staffs.Add(staff);
                context.SaveChanges();
                WorkersGrid.DataContext = context.Staffs.ToList();

            }
            catch {

                MessageBox.Show("Введены не все данные");

            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (WorkersGrid.SelectedItem != null)
                {
                    var selected = WorkersGrid.SelectedItem as Staff;

                    context.Staffs.Remove(selected);
                    context.SaveChanges();
                    WorkersGrid.DataContext = context.Staffs.ToList();

                }
            }

            catch
            {
                MessageBox.Show("Данные не могут быть удалены, они используются");
            }
        }

        private void EditBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(WorkersGrid.SelectedItem != null)
                {
                   var selected =  WorkersGrid.SelectedItem as Staff;

                    var role = Role.SelectedItem as RoleStaff;
                    var credentials = Credentials.SelectedItem as UserCredential;
                    var qualification = Qualification.SelectedItem as Qualification;

                    selected.SurnameStaff = Surname.Text;
                    selected.NameSaff = Name.Text;
                    selected.PatronymicStaff = Patronimic.Text;
                    selected.Role_ID = role.ID_Role;
                    selected.Role_ID = credentials.ID_UserCredentials;
                    selected.Qualification_ID = qualification.ID_Qualification;

                    context.SaveChanges();
                    WorkersGrid.DataContext = context.Staffs.ToList();

                }

            }

            catch
            {
                MessageBox.Show("Ошибка изменения");
            }
        }

        private void WorkersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WorkersGrid.SelectedItem != null)
            {
                var selected = WorkersGrid.SelectedItem as Staff;

                Surname.Text = selected.SurnameStaff;
                Name.Text  = selected.NameSaff;
                Patronimic.Text = selected.PatronymicStaff;
            }
        }
    }
}
