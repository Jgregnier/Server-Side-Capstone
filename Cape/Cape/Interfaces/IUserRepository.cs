using Cape.Data;
using Cape.Models;

namespace Cape.Interfaces
{
    interface IUserRepository
    {
        void Create(ApplicationUser obj);
        void Update(ApplicationUser obj);
        void Save();
    }
}
