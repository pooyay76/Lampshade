using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IAccountApplication accountApplication;

        [TempData]
        public string Message { get; set; } = null;
        [TempData]
        public string RegisterMessage { get; set; } = null;
        public AccountModel(IAccountApplication accountApplication)
        {
            this.accountApplication = accountApplication;
        }

        public void OnGet() {
        }
        public IActionResult OnPostLogin(Login command)
        {
            if (ModelState.IsValid)
            {
                var result = accountApplication.Login(command);
                Message = result.Message;
                if (result.IsSucceeded)
                    return RedirectToPage("/Index");
            }
          
            return RedirectToPage("/Account");
        }
        public IActionResult OnPostRegister(RegisterAccount command)
        {
            if (ModelState.IsValid)
            {
                var result = accountApplication.Register(command);
                Message = result.Message;
                if (result.IsSucceeded)
                    return RedirectToPage("/Index");
            }
            return RedirectToPage("/Account");
        }

        public IActionResult OnGetLogout()
        {
            accountApplication.Logout();
            return RedirectToPage("/Index");
        }
    }
}
