using _0_Framework.Application;
using AutoMapper;
using ShopManagement.Application.Contracts.SlideAgg;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application.AutoMapperProfiles
{
    public class SlideProfile : Profile
    {
        public SlideProfile()
        {
            CreateMap<Slide, SlideViewModel>()
                .ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(inp => inp.CreationDateTime.ToFarsi()));
;
            CreateMap<Slide, SlideMinimalViewModel>()
                .ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(inp => inp.CreationDateTime.ToFarsi()));
;
            CreateMap<Slide, EditSlide>();
        }
    }
}
