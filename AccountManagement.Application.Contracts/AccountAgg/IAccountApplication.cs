using Framework.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.AccountAgg
{
    public interface IAccountApplication
    {
        OperationResult Create(CreateAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        EditAccount EditGet(long id);
        List<AccountViewModel> Search(AccountSearchModel command);
    }

}
