using System.Diagnostics;
using BlogWeb.Models;
using BlogWeb.Models.viewModel;
using BlogWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IBlogPost blogManager;

        private readonly ITag tagManager;

        public HomeController(ILogger<HomeController> logger,
            IBlogPost blogManager,
            ITag tagManager)
        {
            _logger = logger;
            this.blogManager = blogManager;
            this.tagManager = tagManager;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await blogManager.GetAllAsync();
            var tags= await tagManager.GetAllAsync();
            var model = new HomeViewModel
            {
                BlogPosts = posts,
                Tags = tags
            };
            return View(model);
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
