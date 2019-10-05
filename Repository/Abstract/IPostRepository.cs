using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBlog.Models
{
    public interface IPostRepository
    {
        void AddPost(Post post);
        Post GetPost(int id);
        List<Post> GetAllPosts();
        void RemovePost(int id);
        void UpdatePost(int id, Post post,int flag);
        List<Post> GetByCategory(int id);
        List<Post> GetByUser(IdentityUser user);

        List<Post> SearchByWord(string word);
    }
}
