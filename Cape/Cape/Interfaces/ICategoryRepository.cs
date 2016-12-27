using Cape.Models;
using System.Collections.Generic;

namespace Cape.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetById(int categoryId);
        List<Category> GetAll();
        void Create(Category obj);
        void Update(Category obj);
    }
}
