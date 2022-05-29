using AutoMapper;
using DiscountManagement.Application.Contracts.ColleagueDiscountAgg;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application.AutoMapperProfiles
{
    public class ColleagueDiscountProfile:Profile
    {
        public ColleagueDiscountProfile()
        {
            CreateMap<ColleagueDiscount, ColleagueDiscountViewModel>();
            CreateMap<ColleagueDiscount, EditColleagueDiscount>();
        }
    }
}
