using Cape.Models;

namespace Cape.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetById(int categoryId);
        void Create(Category obj);
        void Update(Category obj);
    }
}
