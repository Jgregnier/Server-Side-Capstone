using Microsoft.Practices.Unity;
using System.Web.Http;
using Cape.Repositories;
using Cape.Interfaces;

namespace Cape
{
    public class RegisterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<ITransactionRepository, TransactionRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            

            var container1 = new UnityContainer();
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container1);
        }
    }
}