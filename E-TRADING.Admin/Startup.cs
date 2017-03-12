using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_TRADING.Admin.Startup))]
namespace E_TRADING.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
