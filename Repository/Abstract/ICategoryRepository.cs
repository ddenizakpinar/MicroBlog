using MicroBlog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBlog.Repository.Abstract
{
    public interface ICategoryRepository
    {
        void addCategory(Category category);
        Category GetPost(int id);
        List<Category> GetAllCategories();
        void RemoveCategory(int id);
        void UpdatePost(int id, Category post);
    }
}
