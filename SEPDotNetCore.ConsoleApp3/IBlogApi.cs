﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace SEPDotNetCore.ConsoleApp3
{
    public interface IBlogApi
    {
        [Get("/api/Blogs")]
        Task<List<BlogModel>> GetBlogs();

        [Get("/api/Blogs/{id}")]
        Task<List<BlogModel>> GetBlog(int id);

        [Post("/api/Blogs")]
        Task<List<BlogModel>> CreateBlog(BlogModel blog);
    }

    public class BlogModel
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string BlogContent { get; set; } = null!;

        public bool DeleteFlag { get; set; }

    }
}