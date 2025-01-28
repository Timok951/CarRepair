using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarRepair
{
    internal class BasicButtons
    {

        public Window Exit(int role) {

            switch (role)
            {
                case 1:
                    MainAdmin mainAdmin = new MainAdmin();
                    return (mainAdmin);
                    break;
                case 2:
                    MasterConsultMain consult = new MasterConsultMain();
                    return (consult);
                    break;

                case 3:
                    СarMechanicianMain carmech = new СarMechanicianMain();
                    return (carmech);
                    break;
                case 4:
                    AcountantMain accauntant = new AcountantMain();
                    return (accauntant);
                    break;

            }
        return null;

        }
    }
}
