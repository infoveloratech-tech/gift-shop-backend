using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Authentication
{
    public class GoogleAuthService : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
