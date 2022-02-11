using _0_Framework.Application;
using AutoMapper;

using DiscountManagement.Application.Contracts.CustomerDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application.AutoMapperProfiles
{
    public class CustomerDiscountProfile : Profile
    {
        public CustomerDiscountProfile()
        {
            CreateMap<object, CustomerDiscountViewModel>();
            CreateMap<CustomerDiscount, CustomerDiscountViewModel>()
                .ForMember(x => x.StartDate, cfg => cfg.MapFrom(y => y.StartDate.Date.ToFarsi()))
                .ForMember(x => x.StartDateFa, cfg => cfg.MapFrom(y => y.StartDate.Date.ToFarsi().ToString()))
                .ForMember(x => x.EndDateFa, cfg => cfg.MapFrom(y => y.StartDate.Date.ToFarsi().ToString()))
                .ForMember(x => x.EndDate, cfg => cfg.MapFrom(y => y.EndDate.Date.ToFarsi()));
            CreateMap<CustomerDiscount, EditCustomerDiscount>()
                .ForMember(x=>x.StartDate,cfg=>cfg.MapFrom(y=>y.StartDate.Date.ToFarsi()))
                .ForMember(x => x.EndDate, cfg => cfg.MapFrom(y => y.EndDate.Date.ToFarsi()));
        }
    }
}
