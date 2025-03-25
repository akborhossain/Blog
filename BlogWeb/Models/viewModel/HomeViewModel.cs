using BlogWeb.Models.Entities;

namespace BlogWeb.Models.viewModel
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

    }
}
