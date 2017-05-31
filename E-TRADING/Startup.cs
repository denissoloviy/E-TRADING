using E_TRADING.Jobs;
using E_TRADING.Ninject.Core;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_TRADING.Startup))]
namespace E_TRADING
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => NinjectWebCommon.ServiceLocator.BeginScope());
            app.MapSignalR();
            AuctionUpdaterSheduler.Start();
            ConfigureAuth(app);
        }
    }
}
