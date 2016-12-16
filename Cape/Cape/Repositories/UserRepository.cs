using System.Linq;
using Cape.Models;
using Cape.Data;
using Cape.Interfaces;
using System;
using Microsoft.Practices.Unity;

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
            context.Users.Add(obj);

            context.SaveChangesAsync();
        }

        public ApplicationUser GetById(string UserId)
        {
            ApplicationUser selectedUser = context.Users.Single(user => user.Id == UserId);

            return selectedUser;
        }

        public void Update(ApplicationUser obj)
        {
            context.Entry(obj);

            context.ChangeTracker.DetectChanges();

            context.SaveChangesAsync();
        }
    }
}
