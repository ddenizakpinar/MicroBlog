using MicroBlog.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBlog.Models
{
    public class Post
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        [Required]
        public IdentityUser user { get; set; }
        [Required]
        public int categoryId { get; set; }
        public string Image { get; set; }

        public DateTime DateTime { get; set; }
    }
}
