using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
