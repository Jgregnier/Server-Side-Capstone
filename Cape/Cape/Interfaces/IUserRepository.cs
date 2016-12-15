using Cape.Data;
using Cape.Models;

namespace Cape.Interfaces
{
    public interface IUserRepository
    {
        void Create(ApplicationUser obj);
        ApplicationUser GetById(string UserId);
        void Update(ApplicationUser obj);
    }
}
