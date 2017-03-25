using E_TRADING.Data;
using Ninject.Modules;
using Ninject.Web.Common;

namespace E_TRADING.Ninject.Modules
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ApplicationDbContext>().ToSelf().InRequestScope();
        }
    }
}