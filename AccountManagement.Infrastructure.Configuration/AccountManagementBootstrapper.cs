using Microsoft.Extensions.DependencyInjection;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Infrastructure.EfCore.Repository;
using AccountManagement.Application.Contracts.AccountAgg;
using AccountManagement.Application;

namespace AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountApplication, AccountApplication>();


            services.AddDbContext<AccountContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}
