using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
