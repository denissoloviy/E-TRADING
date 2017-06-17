using AutoMapper;
using E_TRADING.Data;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Managers;
using E_TRADING.Data.Repositories;
using E_TRADING.Mapper.Configurations;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using Ninject.Web.Common;

namespace E_TRADING.Ninject.Modules
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            AutoMapperCoreConfiguration.Configure();
            Bind<IMapper>().ToConstant(AutoMapper.Mapper.Instance).InSingletonScope();
            Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            Bind<IBuyerRepository>().To<BuyerRepository>().InRequestScope();
            Bind<ICategoryRepository>().To<CategoryRepository>().InRequestScope();
            Bind<IImageRepository>().To<ImageRepository>().InRequestScope();
            Bind<IOrderRepository>().To<OrderRepository>().InRequestScope();
            Bind<IProductRepository>().To<ProductRepository>().InRequestScope();
            Bind<ISellerRepository>().To<SellerRepository>().InRequestScope();
            Bind<IAuctionRepository>().To<AuctionRepository>().InRequestScope();
            Bind<IShoppingCartRepository>().To<ShoppingCartRepository>().InRequestScope();
            Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            if (Kernel != null)
            {
                Bind<ApplicationUserManager>()
                .ToMethod(
                    context =>
                        new ApplicationUserManager(
                            new UserStore<User>(Kernel.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext)))
                .InRequestScope();
            }
        }
    }
}