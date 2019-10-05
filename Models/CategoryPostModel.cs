using MicroBlog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBlog.Models
{
    public class CategoryPostModel
    {
        public IEnumerable<Post> posts { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public Post post { get; set; }
        public Category category { get; set; }
    }
}
