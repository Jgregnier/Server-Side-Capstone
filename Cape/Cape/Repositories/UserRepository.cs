using System.Linq;
using Cape.Models;
using Cape.Data;
using Cape.Interfaces;
using System;

namespace Cape.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(UserRepositoryConnection connection)
        {
            context = connection.AppContext;
        }

        public void Create(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }
    }
}
