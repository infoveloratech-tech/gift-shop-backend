using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
