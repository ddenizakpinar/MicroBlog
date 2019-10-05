using MicroBlog.Entity;
using MicroBlog.Models;
using MicroBlog.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBlog.Repository.Concrete.EntityFramework
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void addCategory(Category category)
        {
            _appDbContext.categories.Add(category);
            _appDbContext.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return _appDbContext.categories.ToList<Category>();
      
        }

        public Category GetPost(int id)
        {
            return _appDbContext.categories.FirstOrDefault(i => i.id == id);
        }

        public void RemoveCategory(int id)
        {

            _appDbContext.categories.Remove(_appDbContext.categories.FirstOrDefault(i => i.id == id));
            _appDbContext.SaveChanges();
        }

        public void UpdatePost(int id, Category category)
        {
            var temp = GetPost(id);
            temp.name = category.name;

            _appDbContext.Update(temp);
            _appDbContext.SaveChanges();
        }
    }
}
