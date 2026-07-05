using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
