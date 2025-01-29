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

        public class SparePartReport
        {
            public string Description { get; set; }
        }

        private void LoadSparePartsWarnings()
        {
            var messages = _sparePartsService.CheckSparePartsStock();
            foreach (var message in messages)
            {
                
               MessageBox.Show(message); 
            }
        }

        private void LoadSpareParts()
        {
            var mostPopularWork = context.OrderCars
                    .GroupBy(o => o.ListOfWorks)
                    .Select(g => new
                    {
                        Work = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(g => g.Count)
                    .FirstOrDefault();

            var mostUsedSparePart = context.OrderCars
                .GroupBy(o => o.SpareParts_ID)
                .Select(g => new
                {
                    SparePartId = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .FirstOrDefault();

            string sparePartName = null;
            if (mostUsedSparePart != null)
            {
                sparePartName = context.SpareParts
                    .Where(sp => sp.ID_SpareParts == mostUsedSparePart.SparePartId)
                    .Select(sp => sp.NameSparePart)
                    .FirstOrDefault();
            }

            var results = new List<SparePartReport>();

            if (mostPopularWork != null)
            {
                results.Add(new SparePartReport { Description = $"Самая популярная работа: {mostPopularWork.Work} (Количество: {mostPopularWork.Count})" });
            }
            else
            {
                results.Add(new SparePartReport { Description = "Нет популярных работ." });
            }

            if (!string.IsNullOrEmpty(sparePartName))
            {
                results.Add(new SparePartReport { Description = $"Самая используемая деталь: {sparePartName} (Количество использований: {mostUsedSparePart.Count})" });
            }
            else
            {
                results.Add(new SparePartReport { Description = "Нет используемых деталей." });
            }

            PopularDetailsGrid.ItemsSource = results;
        }




        BasicButtons basicButtons = new BasicButtons();

        private CarRepairEntities6 context = new CarRepairEntities6();
        private readonly ReportService _reportService;
        private readonly SparePartsService _sparePartsService;
        public Reports()
        {
            InitializeComponent();
            _reportService = new ReportService(context);             
            LoadData();
            LoadSpareParts();
            _sparePartsService = new SparePartsService(context);
            LoadSparePartsWarnings();
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
                    page.Canvas.DrawString(line, new PdfTrueTypeFont(new Font("Arial", 20f), true), PdfBrushes.Black, new PointF(10, yPosition)); yPosition += 20; // Смещение для следующей строки
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


                float yPosition = 50;

                var spareParts = context.SpareParts
                    .Select(s => new
                    {
                        s.ID_SpareParts,
                        s.NameSparePart,
                        s.QuantityInStock,
                        s.ArticleSparePart
                    })
                    .ToList();

                foreach (var item in spareParts)
                {
                    string line = $"Деталь: {item.NameSparePart}, Остаток: {item.QuantityInStock}, Артикул: {item.ArticleSparePart}";
                    page.Canvas.DrawString(line, new PdfTrueTypeFont(new Font("Arial", 20f), true), PdfBrushes.Black, new PointF(10, yPosition) );
                    yPosition += 20;
                    // Смещение для следующей строки
                }

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(desktopPath, "Отчет_по_деталям.pdf");

                document.SaveToFile(filePath);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new CarRepairEntities6())
            {
                // Определяем самую популярную работу
                var mostPopularWork = context.OrderCars
                    .GroupBy(o => o.ListOfWorks)
                    .Select(g => new
                    {
                        Work = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(g => g.Count)
                    .FirstOrDefault();

                // Определяем самую используемую деталь
                var mostUsedSparePart = context.OrderCars
                    .GroupBy(o => o.SpareParts_ID)
                    .Select(g => new
                    {
                        SparePartId = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(g => g.Count)
                    .FirstOrDefault();

                // Получаем название самой используемой детали
                var sparePartName = context.SpareParts
                    .Where(sp => sp.ID_SpareParts == mostUsedSparePart.SparePartId)
                    .Select(sp => sp.NameSparePart)
                    .FirstOrDefault();

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

                    // Добавляем самую популярную работу
                    if (mostPopularWork != null)
                    {
                        string workLine = $"Самая популярная работа: {mostPopularWork.Work} (Количество: {mostPopularWork.Count})";
                        page.Canvas.DrawString(workLine, font, PdfBrushes.Black, new PointF(10, yPosition));
                        yPosition += 25; // Смещение для следующей строки
                    }

                    // Добавляем самую используемую деталь
                    if (mostUsedSparePart != null && sparePartName != null)
                    {
                        string sparePartLine = $"Самая используемая деталь: {sparePartName} )";
                        page.Canvas.DrawString(sparePartLine, font, PdfBrushes.Black, new PointF(10, yPosition));
                        yPosition += 25; // Смещение для следующей строки
                    }

                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, "Отчет_по_популярные_датали_работы.pdf");

                    document.SaveToFile(filePath);
                }
            }

        }
    }
}
