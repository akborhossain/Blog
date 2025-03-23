using BlogWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPost blogManager;

        public BlogsController(IBlogPost blogManager)
        {
            this.blogManager = blogManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var post =await blogManager.GetByUrlHandleAsync(urlHandle);   
            return View(post);
        }
    }
}
