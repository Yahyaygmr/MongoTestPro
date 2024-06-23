
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using MongoTestPro.Services.OrderServices;
using MongoTestPro.Services.ProductServices;
using OfficeOpenXml;


namespace MongoTestPro.Controllers
{
    public class ReportController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public ReportController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        public async Task<IActionResult> ExportOrderListToPdf()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            using (var ms = new MemoryStream())
            {
                var writer = new PdfWriter(ms);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                var table = new Table(5);

                table.AddCell("#");
                table.AddCell("Müşteri Adı");
                table.AddCell("Sipariş Tarihi");
                table.AddCell("Adres");
                table.AddCell("Toplam Tutar (TL)");

                int count = 0;
                foreach (var order in orders)
                {
                    count++;
                    table.AddCell($"{count}");
                    table.AddCell(order.CustomerId);
                    table.AddCell(order.OrderDate.ToString("dd MMM yyyy HH:mm"));
                    table.AddCell(order.OrderAddress);
                    table.AddCell(order.OrderTotalPrice.ToString("c"));
                }
                document.Add(table);
                document.Close();
                return File(ms.ToArray(), "applicaton/pdf", "SiparisListesi.pdf");

            }
        }
        public async Task<IActionResult> ExportProductListToExcel()
        {
            var products = await _productService.GetAllProductsWithCategoryAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Ürünler");

                worksheet.Cells[1, 1].Value = "#";
                worksheet.Cells[1, 2].Value = "Ürün Adı";
                worksheet.Cells[1, 3].Value = "Kategori";
                worksheet.Cells[1, 4].Value = "Stok Sayısı (Adet)";
                worksheet.Cells[1, 5].Value = "Fiyatı";

                for (int i = 0; i < products.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = i.ToString();
                    worksheet.Cells[i + 2, 2].Value = products[i].Name;
                    worksheet.Cells[i + 2, 3].Value = products[i].CategoryName;
                    worksheet.Cells[i + 2, 4].Value = products[i].Stock;
                    worksheet.Cells[i + 2, 5].Value = products[i].Price.ToString("c");
                }
                var ms = new MemoryStream();
                package.SaveAs(ms);
                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "urunler.xlsx");
            }
        }
    }
}
