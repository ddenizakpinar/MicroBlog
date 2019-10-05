using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroBlog.Models;
using Microsoft.AspNetCore.Identity;
using MicroBlog.Entity;
using MicroBlog.Repository.Abstract;

namespace MediumClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository repo;
        private readonly ICategoryRepository catrepo;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IPostRepository postRepository, ICategoryRepository categoryRepository, UserManager<IdentityUser> userManager)
        {
            repo = postRepository;
            catrepo = categoryRepository;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            /* Category c1 = new Category { name = "Science" };
             Category c2 = new Category { name = "Space" };
             Category c3 = new Category { name = "Nature" };
             Category c4 = new Category { name = "Technology" };
             Category c5 = new Category { name = "Future" };

             catrepo.addCategory(c2);
             catrepo.addCategory(c3);
             catrepo.addCategory(c4);
             catrepo.addCategory(c5);
             catrepo.addCategory(c1);*/



            CategoryPostModel categoryPostModel = new CategoryPostModel { posts = repo.GetAllPosts(), categories = catrepo.GetAllCategories() };
            categoryPostModel.posts = categoryPostModel.posts.Reverse();

            return View(categoryPostModel);
        }

        public IActionResult Category(int id)
        {



            CategoryPostModel categoryPostModel = new CategoryPostModel { posts = repo.GetByCategory(id), categories = catrepo.GetAllCategories() };
            ViewBag.Categoryid = id;
            categoryPostModel.posts = categoryPostModel.posts.Reverse();
            return View(categoryPostModel);
        }
        // link belirliyoruz
        [Route("{username}")]
        public async Task<IActionResult> PostsofUser(string username)
        {
            
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound("404 Sayfa Bulunamadı");
            var temp = repo.GetByUser(user);

            var userr = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.Title = username + "'s Blogs";
            ViewBag.User = userr.Id;
            CategoryPostModel categoryPostModel = new CategoryPostModel { posts = temp, categories = catrepo.GetAllCategories() };
         
            categoryPostModel.posts = categoryPostModel.posts.Reverse();
            return View("Index", categoryPostModel);
        }

        [Route("{username}/post/{id?}")]
        public IActionResult PostofUser(string username, int id)
        {
            return View(repo.GetPost(id));
        }

        [HttpPost]
        public IActionResult Search(string word)
        {


            ViewBag.Title = "Search Results: " + word;
            CategoryPostModel categoryPostModel = new CategoryPostModel { posts = repo.SearchByWord(word), categories = catrepo.GetAllCategories() };
            return View("Index", categoryPostModel);

        }

    }
}