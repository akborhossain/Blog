using BlogWeb.Data;
using BlogWeb.Models.Entities;
using BlogWeb.Models.viewModel;
using BlogWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWeb.Controllers
{
    public class TagController : Controller
    {
        private readonly ITag tagManager;

        public TagController(ITag tagManager)
        {
            this.tagManager = tagManager;
        }
        public async Task<IActionResult> Index()
        {
            var tags = await tagManager.GetAllAsync();
            return View(tags);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(TagViewModel tag)
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
            await tagManager.AddAsync(tag1);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagManager.GetByIdAsync(id);
            if(tag != null)
            {
                return View(tag);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Tag tag)
        {
            var tag1 = await tagManager.UpdateAsync(tag);
            if (tag1 != null)
            {
                return RedirectToAction("Index");
            }
            else 
            { 
            
            }

            return View(tag);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Tag tag)
        {
            var ex_tag = await tagManager.DeleteAsync(tag.Id);
            if (ex_tag != null)
            {
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Edit", new {id=tag.Id});     
        }
    }
}
