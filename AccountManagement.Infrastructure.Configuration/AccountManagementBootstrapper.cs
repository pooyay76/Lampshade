using Microsoft.Extensions.DependencyInjection;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Infrastructure.EfCore.Repository;
using AccountManagement.Application.Contracts.AccountAgg;
using AccountManagement.Application;
using AccountManagement.Application.AutoMapperProfiles;

namespace AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddAutoMapper(typeof(AccountProfile).Assembly);

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddDbContext<AccountContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}
