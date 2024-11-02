using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE171089_BusinessObjects;
using SE171089_Services.AccountService;

namespace SE171089_RazorPage.Pages.Accounts
{
    public class DetailsModel : PageModel
    {
        private readonly IAccountService accountService;

        public DetailsModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }

      public Account Account { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || accountService == null)
            {
                return NotFound();
            }
            var account = await accountService.GetAccountById(id.Value);
            if (account == null)
            {
                return NotFound();
            }
            else 
            {
                Account = account;
            }
            return Page();
        }
    }
}
