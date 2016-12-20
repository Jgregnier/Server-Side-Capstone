using Ninject;
using Ninject.Modules;
using System.Web.Mvc;
using System;
using Cape.Interfaces;
using Cape.Repositories;

namespace Cape.DependancyResolver
{
    public class DependancyResolver : DefaultControllerFactory
    {
        private IKernel kernel = new StandardKernel(new NinjectBindingDefinitions());

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }
            return (IController)kernel.Get(controllerType);
        }

        public class NinjectBindingDefinitions : NinjectModule
        {
            public override void Load()
            {
                Bind(typeof(ITransactionRepository))
                    .To(typeof(TransactionRepository));

                Bind(typeof(IUserRepository))
                    .To(typeof(UserRepository));

                Bind(typeof(IReportRepository))
                    .To(typeof(ReportRepository));
            }
        }
    }
}