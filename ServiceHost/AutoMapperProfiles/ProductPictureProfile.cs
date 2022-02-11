using AutoMapper;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Application.Contracts.ProductPictureAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.AutoMapperProfiles
{
    public class ProductPictureProfile : Profile
    {
        public ProductPictureProfile()
        {
            CreateMap<ProductPicture, ProductPictureViewModel>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(inp => inp.Product.Name)); ;
            CreateMap<ProductPicture, ProductPictureMinimalViewModel>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(inp => inp.Product.Name)); ;
            CreateMap<ProductPicture, EditProductCategory>();
        }
    }
}
