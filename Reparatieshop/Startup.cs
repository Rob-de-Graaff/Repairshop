using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Reparatieshop.Startup))]
namespace Reparatieshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
