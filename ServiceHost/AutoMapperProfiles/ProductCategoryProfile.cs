using _0_Framework.Application;
using AutoMapper;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.AutoMapperProfiles
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>().ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(inp => inp.CreationDateTime.ToFarsi()));
            CreateMap<ProductCategory, ProductCategoryMinimalViewModel>().ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(inp => inp.CreationDateTime.ToFarsi()));
            CreateMap<ProductCategory, EditProductCategory>();
        }
    }
}
