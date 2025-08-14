using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ProveedoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
