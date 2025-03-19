using BlogWeb.Models.Entities;
using BlogWeb.Models.viewModel;
using BlogWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogWeb.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly ITag tagManager;

        private readonly IBlogPost blogPostManager;

        public BlogPostController(ITag tagManager, IBlogPost blogPostManager)
        {
            this.tagManager = tagManager;
            this.blogPostManager = blogPostManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await blogPostManager.GetAllAsync();

            return View(posts);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var tags = await tagManager.GetAllAsync();
            var model = new BlogViewModel
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(BlogViewModel post)
        {
            var blogPost = new BlogPost
            {
                Heading = post.Heading,
                PageTitle = post.PageTitle,
                Content = post.Content,
                ShortDescription = post.ShortDescription,
                FeaturedImageUrl = post.FeaturedImageUrl,   
                UrlHandle = post.UrlHandle,
                PublishedDate = post.PublishedDate, 
                CreatedBy = post.CreatedBy,
                Visible = post.Visible,
                UpdatedAt= DateTime.Now,
                UpdatedBy= post.CreatedBy,

            };
            var tags= new List<Tag>();
            foreach (var selectedTagId in post.SelectedTags)
            {
                var guid= Guid.Parse(selectedTagId);
                var tag = await tagManager.GetByIdAsync(guid);
                if (tag != null) { 
                    tags.Add(tag);
                }
            }
            blogPost.Tags = tags;

            await blogPostManager.AddAsync(blogPost);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>Edit(Guid id)
        {
            var post =await blogPostManager.GetByIdAsync(id);
            var tag = await tagManager.GetAllAsync();
            if (post != null)
            {
                var model = new EditPostViewModel
                {
                    Id = post.Id,
                    Heading = post.Heading,
                    PageTitle = post.PageTitle,
                    Content = post.Content,
                    ShortDescription = post.ShortDescription,
                    FeaturedImageUrl = post.FeaturedImageUrl,
                    UrlHandle = post.UrlHandle,
                    PublishedDate = post.PublishedDate,
                    CreatedBy = post.CreatedBy,
                    Visible = post.Visible,
                    Tags = tag.Select(x=> new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()

                    }),
                    SelectedTags = post.Tags.Select(x=>x.Id.ToString()).ToArray()
                    

                };
                return View(model);
            }
            return View(null);  
        }
        [HttpPost]
        public async Task<IActionResult>Edit(EditPostViewModel post)
        {
            var blogPost = new BlogPost
            {
                Id=post.Id,
                Heading = post.Heading,
                PageTitle = post.PageTitle,
                Content = post.Content,
                ShortDescription = post.ShortDescription,
                FeaturedImageUrl = post.FeaturedImageUrl,
                UrlHandle = post.UrlHandle,
                PublishedDate = post.PublishedDate,
                CreatedBy = post.CreatedBy,
                Visible = post.Visible,
                UpdatedAt = DateTime.Now,
                UpdatedBy = post.CreatedBy,

            };
            var tags = new List<Tag>();
            foreach (var selectedTagId in post.SelectedTags)
            {
                var guid = Guid.Parse(selectedTagId);
                var tag = await tagManager.GetByIdAsync(guid);
                if (tag != null)
                {
                    tags.Add(tag);
                }
            }
            blogPost.Tags = tags;
            var updatePost= await blogPostManager.UpdateAsync(blogPost);
            if (updatePost != null) 
            {
                return RedirectToAction("Index");
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult>Delete(EditPostViewModel post)
        {
            var deletePost= await blogPostManager.DeleteAsync(post.Id);
            if (deletePost != null)
            {
                return RedirectToAction("Index");
            }
            return View("Edit", new { id = post.Id });

        }
    }
}
