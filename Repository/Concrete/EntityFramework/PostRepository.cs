using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBlog.Models
{

    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _appDbContext;

        public PostRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddPost(Post post)
        {
            _appDbContext.posts.Add(post);
            _appDbContext.SaveChanges();
        }

        public List<Post> GetAllPosts()
        {
            //return _appDbContext.posts.ToList<Post>();
            return _appDbContext.posts.Include(c => c.user).ToList<Post>();
        }

        public Post GetPost(int id)
        {
            Post temp = _appDbContext.posts.Include(c => c.user).FirstOrDefault(i => i.id == id);

            return temp;
        }

        public void RemovePost(int id)
        {
            Post temp = _appDbContext.posts.FirstOrDefault(i => i.id == id);
            _appDbContext.posts.Remove(temp);
            _appDbContext.SaveChanges();
        }
        public void UpdatePost(int id, Post post, int flag)
        {
            var temp = GetPost(id);
            temp.title = post.title;
            temp.content = post.content;
            temp.categoryId = post.categoryId;
            if (flag == 1)
            {
                temp.Image = post.Image;
            }

            _appDbContext.Update(temp);
            _appDbContext.SaveChanges();
        }


        public List<Post> GetByUser(IdentityUser user)
        {

            var temp = _appDbContext.posts.Include(i => i.user).ToList<Post>();
            foreach (var i in temp.ToArray())
            {

                if (i.user != user) temp.Remove(i);
            }
            return temp;
        }

        public List<Post> GetByCategory(int id)
        {
            return _appDbContext.posts.Where(i => i.categoryId == id).Include(c => c.user).ToList();
        }

        public List<Post> SearchByWord(string word)
        {


            //   return _appDbContext.posts.Where(i => i.title.Contains(word) || i.content.Contains(word)).Include(c => c.user).ToList();
            return _appDbContext.posts.Where(i => i.title.ToLower().Contains(word.ToLower())).Include(c => c.user).ToList();
        }
    }
}
