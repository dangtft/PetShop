using Microsoft.AspNetCore.Mvc;

namespace PetShopsMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
