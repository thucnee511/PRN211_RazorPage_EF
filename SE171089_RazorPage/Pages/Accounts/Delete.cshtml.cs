using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObjects;
using SE171089_Daos;
using SE171089_Services.AccountService;

namespace SE171089_RazorPage.Pages.Accounts
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountService accountService;

        public DeleteModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || accountService == null)
            {
                return NotFound();
            }
            var account = await accountService.GetAccountById(id.GetValueOrDefault());
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || accountService == null)
            {
                return NotFound();
            }
            var account = await accountService.GetAccountById(id.GetValueOrDefault());
            if (account != null)
            {
                Account = account;
                Account = await accountService.Delete(Account.Id);
            }
            return RedirectToPage("./Index");
        }
    }
}
