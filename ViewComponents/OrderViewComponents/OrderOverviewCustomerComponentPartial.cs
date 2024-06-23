using Microsoft.AspNetCore.Mvc;

namespace MongoTestPro.ViewComponents.OrderViewComponents
{
    public class OrderOverviewCustomerComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(string id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
