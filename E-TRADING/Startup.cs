using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_TRADING.Startup))]
namespace E_TRADING
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
