﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogWeb.Models.viewModel
{
    public class BlogViewModel
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool Visible { get; set; }
        public IEnumerable<SelectListItem> Tags { get; set; }
        public string[] SelectedTags { get; set; }= Array.Empty<string>();
    }
}
