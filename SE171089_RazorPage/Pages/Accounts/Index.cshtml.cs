using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE171089_BusinessObjects;
using SE171089_Services.AccountService;

namespace SE171089_RazorPage.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService accountService;

        public IndexModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if(accountService != null)
            {
                Account = await accountService.getActiveAccounts();
                foreach(var item in Account)
                {
                    Role role = await accountService.GetRole(item.RoleId);
                    item.Role = role;
                }
                return Page();
            }
            return Page();
        }
    }
}
