using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Controllers
{
    public class SuppliersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
