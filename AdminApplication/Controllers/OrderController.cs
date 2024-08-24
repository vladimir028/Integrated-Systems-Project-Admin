using AdminApplication.Models;
using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdminApplication.Controllers
{
    public class OrderController : Controller
    {
        public OrderController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:65124/api/Admin/GetAllOrders";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Order>>().Result;
            return View(data);
        }

        public IActionResult Details(Guid Id)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:65124/api/Admin/GetDetailsForOrder";
            var model = new
            {
                Id = Id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Order>().Result;
            return View(data);
        }

        public FileContentResult CreateInvoice(Guid Id)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:65124/api/Admin/GetDetailsForOrder";
            var model = new
            {
                Id = Id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Order>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);
            document.Content.Replace("{{Date}}", DateTime.Today.ToString("dd/MM/yyyy"));
            document.Content.Replace("{{OrderNumber}}", data.Id.ToString());
            //document.Content.Replace("{{FirsName}}", data.EShopApplicationUser.FirstName);
            document.Content.Replace("{{UserName}}", data.EShopApplicationUser.UserName);
            StringBuilder sb = new StringBuilder();
            var total = 0;
            foreach (var item in data.TravelPackageInOrders)
            {
                sb.Append("Travel Package " + item.TravelPackage.Name + " with total Number of travelers:  " + item.NumberOfTravelers + " with price " + item.TravelPackage.Price + "$\n");
                total += (item.NumberOfTravelers * Convert.ToInt32(item.TravelPackage.Price));
            }
            document.Content.Replace("{{TravelPackageList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", total.ToString() + "$");

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportedInvoice.pdf");

        }

        [HttpGet]
        public FileContentResult ExportOrders()
        {
            string fileName = "AllOrders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workBook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workBook.Worksheets.Add("Orders");

                worksheet.Cell(1, 1).Value = "Order ID";
                worksheet.Cell(1, 2).Value = "Customer Username";
                worksheet.Cell(1, 3).Value = "Customer First name";
                worksheet.Cell(1, 4).Value = "Customer Last name";

                HttpClient client = new HttpClient();
                string URL = "http://localhost:65124/api/Admin/GetAllOrders";
                HttpResponseMessage response = client.GetAsync(URL).Result;

                var data = response.Content.ReadAsAsync<List<Order>>().Result;

                for (int i = 0; i < data.Count(); i++)
                {
                    var order = data[i];
                    worksheet.Cell(i + 2, 1).Value = order.Id.ToString();
                    worksheet.Cell(i + 2, 2).Value = order.EShopApplicationUser.UserName;
                    worksheet.Cell(i + 2, 3).Value = order.EShopApplicationUser.FirstName;
                    worksheet.Cell(i + 2, 4).Value = order.EShopApplicationUser.LastName;

                    for (int j = 0; j < order.TravelPackageInOrders.Count(); j++)
                    {
                        worksheet.Cell(1, j + 5).Value = "Travel Package - " + (j + 1);
                        worksheet.Cell(i + 2, j + 5).Value = order.TravelPackageInOrders.ElementAt(j).TravelPackage.Name;

                        //worksheet.Cell(1, j + 6).Value = "Travel Package - " + (j + 1) + "Itinerary Dates";
                        //var itinerary = order.TravelPackageInOrders.ElementAt(j).TravelPackage.Itinerary;
                        //worksheet.Cell(i + 2, j + 6).Value = itinerary.StartDate.ToString("dd/MM/yyyy") + " - " + itinerary.EndDate.;
                       
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }

        }
    }

}
