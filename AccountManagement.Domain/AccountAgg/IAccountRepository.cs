using AccountManagement.Application.Contracts.AccountAgg;
using Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository:IRepository<long,Account>
    {
        EditAccount EditGet(long id);
        IEnumerable<AccountViewModel> Search(AccountSearchModel command);
    }
}
