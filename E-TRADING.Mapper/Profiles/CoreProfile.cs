using AutoMapper;
using E_TRADING.Common.Extensions;
using E_TRADING.Common.Models;
using E_TRADING.Common.OrderStatuses;
using E_TRADING.Data.Entities;
using System.Linq;

namespace E_TRADING.Mapper.Profiles
{
    public class CoreProfile : Profile
    {
        public CoreProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.Alias))
                .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => src.AddedDate.DateTimeToFormatString()));

            CreateMap<ShoppingCart, ProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ShopCartId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Product.Category.Name))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Product.SellerId))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Product.Seller.Alias))
                .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => src.AddedDate.DateTimeToFormatString()));

            CreateMap<Buyer, BuyerProfileHelperViewModel>()
                .ForMember(dest => dest.ActiveOrdersCount, opt => opt.MapFrom(src =>
                    src.Orders.Where(item => StatusTypes.ActiveStatuses.Contains(item.Status)).Count()))
                .ForMember(dest => dest.InactiveOrdersCount, opt => opt.MapFrom(src =>
                    src.Orders.Where(item => StatusTypes.InactiveStatuses.Contains(item.Status)).Count()))
                .ForMember(dest => dest.ShoppingCartCount, opt => opt.MapFrom(src =>
                    src.ShoppingCarts.Sum(item => item.Amount)));

            CreateMap<Seller, SellerProfileHelperViewModel>()
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.Products.Where(item => !item.IsDeleted).Sum(item => item.Amount)))
                .ForMember(dest => dest.ActiveOrdersCount, opt => opt.MapFrom(src => src.Products.SelectMany(item => item.Orders)
                    .Where(item => StatusTypes.ActiveStatuses.Contains(item.Status)).Sum(item => item.Amount)))
                .ForMember(dest => dest.InactiveOrdersCount, opt => opt.MapFrom(src => src.Products.SelectMany(item => item.Orders)
                    .Where(item => StatusTypes.InactiveStatuses.Contains(item.Status)).Sum(item => item.Amount)))
                .ForMember(dest => dest.ArchiveProductsCount, opt => opt.MapFrom(src => src.Products.Where(item => item.IsDeleted || item.Amount == 0).Sum(item => item.Amount)));

            CreateMap<Seller, SellerViewEditViewModel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Helper, opt => opt.MapFrom(src =>
                    AutoMapper.Mapper.Map<SellerProfileHelperViewModel>(src)));

            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.StatusType, opt => opt.MapFrom(src => src.Status.GetStatusType()))
                .ForMember(dest => dest.Buyer, opt => opt.MapFrom(src => src.Buyer.User.UserName))
                .ForMember(dest => dest.Seller, opt => opt.MapFrom(src => src.Product.Seller.User.UserName));

            CreateMap<Buyer, BuyerViewEditViewModel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Helper, opt => opt.MapFrom(src =>
                    AutoMapper.Mapper.Map<BuyerProfileHelperViewModel>(src)));
        }
    }
}
