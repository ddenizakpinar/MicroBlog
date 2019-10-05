using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MicroBlog.Entity;
using MicroBlog.Models;
using MicroBlog.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MicroBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICategoryRepository _categoryRepository;

        public PostController(IPostRepository postRepository, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
        }

        [Authorize]
        public IActionResult AddPost()
        {

            CategoryPostModel categoryPostModel = new CategoryPostModel { categories = _categoryRepository.GetAllCategories() };
            return View(categoryPostModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(Post post, IFormFile file)
        {

            ModelState.Clear();
            // Adding user to post which is not included at form
            post.user = await _userManager.GetUserAsync(HttpContext.User);
            // formu tekrar doğruluyoruz
            TryValidateModel(post);
            if (ModelState.IsValid)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                // Adding Datetime and image name
                post.Image = file.FileName;
                post.DateTime = DateTime.Now;
                _postRepository.AddPost(post);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }


        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {

            CategoryPostModel categoryPostModel = new CategoryPostModel { categories = _categoryRepository.GetAllCategories() };
            var user = await _userManager.GetUserAsync(HttpContext.User);
            try
            {
                if (_postRepository.GetPost(id).user == user)
                    categoryPostModel.post = _postRepository.GetPost(id);
                else
                    return NotFound("404");
            }
            catch (Exception e)
            {
                return NotFound("404");
            }

            return View(categoryPostModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Post post, IFormFile file)
        {
            ModelState.Clear();
            post.user = await _userManager.GetUserAsync(HttpContext.User);
            TryValidateModel(post);
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    post.Image = file.FileName;
                    _postRepository.UpdatePost(id, post, 1);
                }
                else
                {
                    _postRepository.UpdatePost(id, post, 2);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public async Task<IActionResult> RemovePost(int id)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            try
            {

                // Check is post's owner, the user
                if (_postRepository.GetPost(id).user == user)
                    _postRepository.RemovePost(id);
                else
                    return NotFound("404");
            }
            catch (Exception e)
            {
                return NotFound("404");
            }
            return RedirectToAction("Index", "Home");
        }


    }
}