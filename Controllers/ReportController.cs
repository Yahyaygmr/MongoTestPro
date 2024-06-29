
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using MongoTestPro.Entities;
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
            var orders = await _orderService.GetAllOrdersWithCustomerAsync();

            using (var ms = new MemoryStream())
            {
                var writer = new PdfWriter(ms);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                var table = new Table(6);

                table.AddCell("#");
                table.AddCell("Müşteri Adı");
                table.AddCell("Müşteri Tel");
                table.AddCell("Sipariş Tarihi");
                table.AddCell("Adres");
                table.AddCell("Toplam Tutar (TL)");

                int count = 0;
                foreach (var order in orders)
                {
                    count++;
                    table.AddCell($"{count}");
                    table.AddCell(order.CustomerName + " " + order.CustomerSurname);
                    table.AddCell(order.CustomerPhoneNumber);
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
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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

        public async Task<IActionResult> ExportOrderListToPdf2()
        {
            var orders = await _orderService.GetAllOrdersWithCustomerAsync();

            using (var ms = new MemoryStream())
            {
                var writer = new PdfWriter(ms);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Add header
                Paragraph header = new Paragraph("Order Confirmation")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetBold();
                document.Add(header);

                Paragraph subHeader = new Paragraph("Thank you for your purchase!")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(14);
                document.Add(subHeader);

                document.Add(new Paragraph("\n"));

                // Customer Information
                var customerInfoTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2 })).UseAllAvailableWidth();
                customerInfoTable.AddCell(new Cell().Add(new Paragraph("Customer Information").SetBold()));
                customerInfoTable.AddCell(new Cell().Add(new Paragraph("Order Information").SetBold()));

                customerInfoTable.AddCell(new Cell().Add(new Paragraph("Name: John Doe")));
                customerInfoTable.AddCell(new Cell().Add(new Paragraph($"Order Date: {DateTime.Now:yyyy-MM-dd}")));

                customerInfoTable.AddCell(new Cell().Add(new Paragraph("Email: john.doe@example.com")));
                customerInfoTable.AddCell(new Cell().Add(new Paragraph("Order Number: #123456")));

                customerInfoTable.AddCell(new Cell().Add(new Paragraph("Address: 1234 Main St, City, Country")));
                customerInfoTable.AddCell(new Cell().Add(new Paragraph("Payment Method: Credit Card")));

                document.Add(customerInfoTable);

                document.Add(new Paragraph("\n"));

                // Order Items
                var orderTable = new Table(UnitValue.CreatePercentArray(new float[] { 2, 1, 1, 1 })).UseAllAvailableWidth();
                orderTable.AddHeaderCell(new Cell().Add(new Paragraph("Product").SetBackgroundColor(ColorConstants.YELLOW)));
                orderTable.AddHeaderCell(new Cell().Add(new Paragraph("Quantity").SetBackgroundColor(ColorConstants.YELLOW)));
                orderTable.AddHeaderCell(new Cell().Add(new Paragraph("Price").SetBackgroundColor(ColorConstants.YELLOW)));
                orderTable.AddHeaderCell(new Cell().Add(new Paragraph("Total").SetBackgroundColor(ColorConstants.YELLOW)));

                var orderRow = new OrderRow
                {
                    ProductId = "asdfghjkjnbvc",
                    OrderId = "order Id",
                    ProductCount = 1,
                    RowPrice = 300
                };
                orderTable.AddCell(new Cell().Add(new Paragraph(orderRow.ProductId)));
                orderTable.AddCell(new Cell().Add(new Paragraph(orderRow.ProductCount.ToString())));
                orderTable.AddCell(new Cell().Add(new Paragraph(orderRow.RowPrice.ToString("c"))));
                orderTable.AddCell(new Cell().Add(new Paragraph((orderRow.ProductCount * orderRow.RowPrice).ToString("c"))));
                //foreach (var order in orders)
                //{
                //    foreach (var item in order.Items)
                //    {
                //        orderTable.AddCell(new Cell().Add(new Paragraph(item.ProductName)));
                //        orderTable.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToString())));
                //        orderTable.AddCell(new Cell().Add(new Paragraph(item.Price.ToString("c"))));
                //        orderTable.AddCell(new Cell().Add(new Paragraph((item.Quantity * item.Price).ToString("c"))));
                //    }
                //}

                // Adding Total
                var totalCell = new Cell(1, 3).Add(new Paragraph("Total").SetTextAlignment(TextAlignment.RIGHT).SetBold());
                orderTable.AddCell(totalCell);
                orderTable.AddCell(new Cell().Add(new Paragraph(orders.Sum(o => o.OrderTotalPrice).ToString("c"))));

                document.Add(orderTable);

                document.Add(new Paragraph("\n"));

                // Contact Information
                Paragraph contactInfo = new Paragraph("If you have any questions, please contact us at support@example.com.")
                    .SetTextAlignment(TextAlignment.CENTER);
                document.Add(contactInfo);

                document.Close();
                return File(ms.ToArray(), "application/pdf", "SiparisListesi.pdf");
            }
        }
    }
}
