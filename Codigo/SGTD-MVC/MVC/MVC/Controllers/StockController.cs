using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class StockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
