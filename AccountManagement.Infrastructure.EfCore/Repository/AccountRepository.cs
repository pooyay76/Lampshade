using AccountManagement.Application.Contracts.AccountAgg;
using AccountManagement.Domain.AccountAgg;
using Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext accountContext;
        public AccountRepository(AccountContext context) : base(context)
        {
            accountContext = context;
        }

        public EditAccount EditGet(long id)
        {
            var target = accountContext.Accounts.FirstOrDefault(x=>x.Id == id);
            if (target == null)
                return null;
            return new EditAccount {
                Id = target.Id,
                FullName = target.FullName,
                PhoneNumber = target.PhoneNumber,
                ProfilePicture = target.ProfilePicture,
                RoleId = target.RoleId,
                Username = target.Username
            };
        }

        public IEnumerable<AccountViewModel> Search(AccountSearchModel command)
        {
            var query = accountContext.Accounts;
            if (!string.IsNullOrWhiteSpace(command.Username))
                query.Where(x => x.Username.Contains(command.Username));

            if (!string.IsNullOrWhiteSpace(command.Name))
                query.Where(x => x.FullName.Contains(command.Name));

            if (!string.IsNullOrWhiteSpace(command.PhoneNumber))
                query.Where(x => x.PhoneNumber.Contains(command.PhoneNumber));

            if (command.RoleId != default)
                query.Where(x => x.RoleId == command.RoleId);

            return query.Select(x => new AccountViewModel { 
                Id= x.Id,
                RoleId = x.RoleId,
                PhoneNumber = x.PhoneNumber,
                FullName = x.FullName,
                ProfilePicture = x.ProfilePicture,
                Username = x.Username
            });

        }
    }
}
