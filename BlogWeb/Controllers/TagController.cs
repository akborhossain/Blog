using BlogWeb.Data;
using BlogWeb.Models.Entities;
using BlogWeb.Models.viewModel;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class TagController : Controller
    {
        private readonly BlogDbContext context;
        public TagController(BlogDbContext _context)
        {
            this.context = _context;
        }
        public IActionResult Index()
        {
            var tags=context.Tags.ToList();
            return View(tags);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(TagViewModel tag)
        {
            if (tag == null)
            { 
                return View();
            }
            var tag1 = new Tag
            {
                Name = tag.Name,
                DisplayName = tag.DisplayName,
            };
            context.Tags.Add(tag1);
            context.SaveChanges();
            return View(tag);
        }
    }
}
