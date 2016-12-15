using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cape.Interfaces
{
    public interface IDependencyScope : IDisposable
    {
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
    }
}