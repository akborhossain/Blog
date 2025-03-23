using System.Diagnostics;
using BlogWeb.Models;
using BlogWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IBlogPost BlogManager { get; }

        public HomeController(ILogger<HomeController> logger, IBlogPost blogManager)
        {
            _logger = logger;
            BlogManager = blogManager;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await BlogManager.GetAllAsync();

            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
