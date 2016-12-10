using Cape.Models;

namespace Cape.Interfaces
{
    public interface ICategoryRepository
    {
        void Create(Category obj);
        void Save();
    }
}
