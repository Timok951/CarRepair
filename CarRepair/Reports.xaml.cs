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
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;

namespace CarRepair
{


    /// <summary>
    /// Логика взаимодействия для Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {

        private void LoadSpareParts()
        {
            using (var context = new CarRepairEntities5())
            {
                var spareParts = context.SpareParts
                    .Select(s => new
                    {
                        s.ID_SpareParts,
                        s.NameSparePart,
                        s.QuantityInStock,
                        s.PriceSparePart,
                        OrdersCount = s.OrderCars.Count() 
                    })
                    .ToList();

                PopularDetailsGrid.ItemsSource = spareParts;
            }
        }



        BasicButtons basicButtons = new BasicButtons();

        private CarRepairEntities5 context = new CarRepairEntities5();
        private readonly ReportService _reportService;
        public Reports()
        {
            InitializeComponent();
            _reportService = new ReportService(new CarRepairEntities5());
            LoadData();
            LoadSpareParts();
        }

        private void LoadData()
        {
            var reportData = _reportService.GetTotalProfitByAddress();
            IncomeGrid.ItemsSource = reportData;

            var spareParts = context.SpareParts.ToList();
            PartsList.ItemsSource = spareParts;
            
        }
        private void exitbutton_Click(object sender, RoutedEventArgs e)
        {
            int role = GlobalVariables.RoleUser;
            Window newwindow = basicButtons.Exit(role);
            newwindow.Show();
            this.Close();
        }

        private void DownloadIncome_Click(object sender, RoutedEventArgs e)
        {
            using (var document = new Spire.Pdf.PdfDocument())
            {
                var page = document.Pages.Add();

                var reportData = IncomeGrid.ItemsSource as List<ProfitReport>;

                float yPosition = 50;

                foreach (var item in reportData)
                {
                    string line = $"Адрес: {item.Address}, Общая прибыль: {item.TotalProfit:C}, Количество мест: {item.TotalPlaces}";
                    page.Canvas.DrawString(line, new PdfTrueTypeFont(new Font("Arial", 20f), true), PdfBrushes.Red, new PointF(10, 15)); yPosition += 20; // Смещение для следующей строки
                }

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(desktopPath, "Отчет_по_прибыли.pdf");

                document.SaveToFile(filePath);

            }
        }

        private void PartsDownload_Click(object sender, RoutedEventArgs e)
        {
            using (var document = new Spire.Pdf.PdfDocument())
            {
                var page = document.Pages.Add();

                var spareParts = context.SpareParts;

                float yPosition = 50;

                foreach (var item in spareParts)
                {
                    string line = $"Деталь: {item.NameSparePart}, Остаток: {item.QuantityInStock}, Артикул: {item.ArticleSparePart}";
                    page.Canvas.DrawString(line, new PdfTrueTypeFont(new Font("Arial", 20f), true), PdfBrushes.Red, new PointF(10, 15)); yPosition += 20; // Смещение для следующей строки
                }

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(desktopPath, "Отчет_по_деталям.pdf");

                document.SaveToFile(filePath);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новый PDF документ
            using (var document = new Spire.Pdf.PdfDocument())
            {
                // Добавляем страницу
                var page = document.Pages.Add();

                // Устанавливаем шрифт
                var font = new PdfTrueTypeFont(new Font("Arial", 20f), true);

                // Устанавливаем начальную позицию для текста
                float yPosition = 50;

                // Заголовок
                page.Canvas.DrawString("Отчет по деталям", font, PdfBrushes.Black, new PointF(10, yPosition));
                yPosition += 30; // Смещение для следующей строки

                // Получаем список запасных частей
                using (var context = new CarRepairEntities5())
                {
                    var spareParts = context.SpareParts
                        .Select(s => new
                        {
                            s.ID_SpareParts,
                            s.NameSparePart,
                            s.QuantityInStock,
                            s.ArticleSparePart
                        })
                        .ToList();

                    // Перебираем запасные части и добавляем их в PDF
                    foreach (var item in spareParts)
                    {
                        string line = $"Деталь: {item.NameSparePart}";
                        page.Canvas.DrawString(line, font, PdfBrushes.Black, new PointF(10, yPosition));
                        yPosition += 25; // Смещение для следующей строки
                    }
                }

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(desktopPath, "Отчет_по_используемым_деталям.pdf");

                document.SaveToFile(filePath);
            }

                    }
    }
}
