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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRepair
{
    

    public partial class MainWindow : Window
    {


        private CarRepairEntities5 context = new CarRepairEntities5();

        public MainWindow()
        {
            InitializeComponent();
            PasswordTextBox.PasswordChar = '*';

        }


        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            var user = context.UserCredentials.FirstOrDefault(a => a.LoginUser == LoginTextBox.Text && a.PasswordUser == PasswordTextBox.Password);

            if (user != null)
            {
                var staff = user.Staffs.FirstOrDefault();
                if (staff != null)
                {
                    var roleID = staff.Role_ID;

                    switch (roleID)
                    {
                        case 1:
                            MainAdmin admin = new MainAdmin();
                            admin.Show();
                            GlobalVariables.RoleUser = Convert.ToInt32(roleID);

                            this.Close();

                            break;
                        case 2:
                            MasterConsultMain consult = new MasterConsultMain();
                            consult.Show();
                            GlobalVariables.RoleUser = Convert.ToInt32(roleID);

                            this.Close();

                            break;

                        case 3:
                            СarMechanicianMain carmech = new СarMechanicianMain();
                            carmech.Show();
                            GlobalVariables.RoleUser = Convert.ToInt32(roleID);

                            this.Close();

                            break;
                        case 4:
                            AcountantMain accauntant = new AcountantMain();
                            accauntant.Show();
                            GlobalVariables.RoleUser = Convert.ToInt32(roleID);

                            this.Close();

                            break;




                    }
                }
                else
                {
                    MessageBox.Show("Проверьте данные");
                }


            }
            else
            {
                MessageBox.Show("Проверьте данные");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
