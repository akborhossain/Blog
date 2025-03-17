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
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = context.Tags.FirstOrDefault(x => x.Id == id);
            if(tag != null)
            {
                return View(tag);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            var tag1= context.Tags.FirstOrDefault(x=>x.Id==tag.Id);
            if (tag1 != null) { 
                
                tag1.Name = tag.Name;
                tag1.DisplayName = tag.DisplayName;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }
        [HttpPost]
        public IActionResult Delete(Tag tag)
        {
            var tag1= context.Tags.FirstOrDefault(x=>x.Id == tag.Id);
            if (tag1 != null) {
             context.Tags.Remove(tag1);
             context.SaveChanges();
             return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new {id=tag.Id});     
        }
    }
}
