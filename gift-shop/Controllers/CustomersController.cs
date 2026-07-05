using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
