using AutoMapper;
using E_TRADING.Common.Extensions;
using E_TRADING.Common.Models;
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
            CreateMap<Seller, SellerViewEditViewModel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.Products.Sum(item => item.Amount)));
        }
    }
}
