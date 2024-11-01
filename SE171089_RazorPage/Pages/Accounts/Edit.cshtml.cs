using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObjects;
using SE171089_Daos;
using SE171089_Services.AccountService;

namespace SE171089_RazorPage.Pages.Accounts
{
    public class EditModel : PageModel
    {
        private readonly IAccountService accountService;
        public EditModel(IAccountService accountService)
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
            Account? account = await accountService.GetAccountById(id.GetValueOrDefault());
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                Account? updatedAccount = await accountService.Update(Account);
                if (updatedAccount == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Update failed: {ex.Message}";
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
