using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Cape.Startup))]
namespace Cape
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            
        }
    }
}
