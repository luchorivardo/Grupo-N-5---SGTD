using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
