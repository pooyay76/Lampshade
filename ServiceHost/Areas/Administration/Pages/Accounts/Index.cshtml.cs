using AccountManagement.Application.Contracts.AccountAgg;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IAccountApplication accountApplication;
        public AccountSearchModel SearchModel { get; set; }
        public List<AccountViewModel> Items { get; set; }
        public IndexModel(IAccountApplication accountApplication)
        {
            this.accountApplication = accountApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            SearchModel = searchModel;
            Items = accountApplication.Search(SearchModel);
        }
        public PartialViewResult OnGetCreate()
        {
            return Partial("./Create", new CreateAccount());
        }
        public PartialViewResult OnGetEdit(long id)
        {
            return Partial("./Edit", accountApplication.EditGet(id));
        }
        public PartialViewResult OnGetChangePassword(long id)
        {
            return Partial("./ChangePassword", new ChangePassword() { Id = id }) ;
        }
        public JsonResult OnPostCreate(CreateAccount command)
        {
            if (ModelState.IsValid)
                return new JsonResult(accountApplication.Create(command));
            return new JsonResult(ValidationMessages.InvalidModelStateMessage);
        }
        public JsonResult OnPostEdit(EditAccount command)
        {
            if (ModelState.IsValid)
                return new JsonResult(accountApplication.Edit(command));
            return new JsonResult(ValidationMessages.InvalidModelStateMessage);
        }
        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            if (ModelState.IsValid)
                return new JsonResult(accountApplication.ChangePassword(command));
            return new JsonResult(ValidationMessages.InvalidModelStateMessage);
        }
    }
}
