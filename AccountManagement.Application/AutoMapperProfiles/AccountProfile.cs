using AccountManagement.Application.Contracts.AccountAgg;
using AccountManagement.Domain.AccountAgg;
using AutoMapper;

namespace AccountManagement.Application.AutoMapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account,AccountViewModel>();
            CreateMap<Account, EditAccount>();
        }
    }
}
