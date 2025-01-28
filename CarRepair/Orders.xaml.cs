using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {

        BasicButtons basicButtons = new BasicButtons();


        private string GenerateReceipt(OrderCar order)
        {
            var sparePart = context.SpareParts.Find(order.SpareParts_ID);
            var status = context.StatusCars.Find(order.Status_ID);
            var sto = context.STOes.Find(order.STO_ID);
            var car = context.Cars.Find(order.Car_ID);

            var receipt = new Receipt
            {
                CarNumber = car?.NumberCar, // Предполагается, что у вас есть свойство Number в классе Car
                SparePartName = sparePart?.NameSparePart,
                Status = status?.NameStatus, // Предполагается, что у вас есть свойство NameStatus в классе StatusCar
                STOName = sto?.AddressSTO, // Предполагается, что у вас есть свойство NameSTO в классе STO
                WorkDescription = order.ListOfWorks,
                TotalPrice = order.TotalPrice,
                DateRequest = order.DateRequest
            };

            // Форматируем чек в строку
            return $"Чек\n" +
                   $"Номер автомобиля: {receipt.CarNumber}\n" +
                   $"Деталь: {receipt.SparePartName}\n" +
                   $"Статус: {receipt.Status}\n" +
                   $"СТО: {receipt.STOName}\n" +
                   $"Описание работ: {receipt.WorkDescription}\n" +
                   $"Итоговая цена: {receipt.TotalPrice} руб.\n" +
                   $"Дата запроса: {receipt.DateRequest}";
        }
        private void SaveReceiptToFile(string receiptText)
        {

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = $"Чек_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt"; 
            string filePath = System.IO.Path.Combine(desktopPath, fileName);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(receiptText);
            }

            MessageBox.Show($"Чек сохранен: {filePath}", "Сохранение чека", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CostBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void CostBox_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private CarRepairEntities5 context = new CarRepairEntities5();

        public Orders()
        {
            InitializeComponent();
            OrderGrid.ItemsSource = context.OrderCars.ToList();

            StatusCmb.ItemsSource = context.StatusCars.ToList();
            StatusCmb.DisplayMemberPath = "NameStatus";

            SparePartsCmbx.ItemsSource = context.SpareParts.ToList();
            SparePartsCmbx.DisplayMemberPath = "NameSparePart";

            StoCmbx.ItemsSource = context.STOes.ToList();
            StoCmbx.DisplayMemberPath = "AddressSTO";

            CarCmbx.ItemsSource = context.Cars.ToList();
            CarCmbx.DisplayMemberPath = "NumberCar";

        }

        private void OrderGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderGrid.SelectedItem != null)
            {
                var selected = OrderGrid.SelectedItem as OrderCar;
                WorkBox.Text = Convert.ToString(selected.ListOfWorks);
                CostBox.Text = Convert.ToString(selected.TotalPrice);
                StatusCmb.SelectedItem = selected.Status_ID;
                SparePartsCmbx.SelectedItem = selected.SpareParts_ID;
                StoCmbx.SelectedItem = selected.ID_Order; 
                CarCmbx.SelectedItem = selected.Car_ID;

            }

        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var status = StatusCmb.SelectedItem as StatusCar;
                var sparepart = SparePartsCmbx.SelectedItem as SparePart;
                var sto = StoCmbx.SelectedItem as STO;
                var carnum = CarCmbx.SelectedItem as Car;
                
                if (status != null && sparepart != null && sto != null && carnum != null)
                {
                    OrderCar car = new OrderCar();

                    car.Status_ID = status.ID_Status;
                    car.SpareParts_ID = sparepart.ID_SpareParts;
                    car.Car_ID = carnum.ID_Car;
                    car.STO_ID = sto.ID_STO;
                    car.ListOfWorks = WorkBox.Text;
                    car.TotalPrice = Convert.ToDecimal(CostBox.Text);
                    car.DateRequest = DatePick.Text;

                    context.OrderCars.Add(car);
                    context.SaveChanges();
                    OrderGrid.ItemsSource = context.OrderCars.ToList();

                    var receiptText = GenerateReceipt(car);
                    MessageBox.Show(receiptText, "Чек", MessageBoxButton.OK, MessageBoxImage.Information);
                    SaveReceiptToFile(receiptText);
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            int role = GlobalVariables.RoleUser;
            Window newwindow = basicButtons.Exit(role);
            newwindow.Show();
            this.Close();
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OrderGrid.SelectedItem != null)
                {
                    context.OrderCars.Remove(OrderGrid.SelectedItem as OrderCar);
                    context.SaveChanges();
                    OrderGrid.ItemsSource = context.OrderCars.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Нельзя удалить, данные используются");
            }
        }

        private void EditBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (OrderGrid.SelectedItem != null)
                {
                    var selected = OrderGrid.SelectedItem as OrderCar;

                    var status = StatusCmb.SelectedItem as StatusCar;
                    var sparepart = SparePartsCmbx.SelectedItem as SparePart;
                    var sto = StoCmbx.SelectedItem as STO;
                    var carnum = CarCmbx.SelectedItem as Car;

                    selected.Status_ID = status.ID_Status;
                    selected.SpareParts_ID = sparepart.ID_SpareParts;
                    selected.Car_ID = carnum.ID_Car;
                    selected.STO_ID = sto.ID_STO;
                    selected.ListOfWorks = WorkBox.Text;
                    selected.TotalPrice = Convert.ToDecimal(CostBox.Text);
                    selected.DateRequest = DatePick.Text;

                    context.SaveChanges();
                    OrderGrid.ItemsSource = context.OrderCars.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка изменения");
            }
        }
    }
}
