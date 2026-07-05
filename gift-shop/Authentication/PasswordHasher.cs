using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Authentication
{
    public class PasswordHasher : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
