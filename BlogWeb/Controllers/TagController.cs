using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
