using _0_Framework.Application;
using AutoMapper;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest=>dest.CreationDateTime,opt=>opt.MapFrom(inp=>inp.CreationDateTime.ToFarsi()))
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(inp => inp.Category.Name));

            CreateMap<Product, ProductMinimalViewModel>()
                .ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(inp => inp.CreationDateTime.ToFarsi()))
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(inp => inp.Category.Name));
           
            CreateMap<Product, EditProduct>();
        }
    }
}
