using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE171089_BusinessObjects;
using SE171089_Services.AccountService;
using System.ComponentModel.DataAnnotations;

namespace SE171089_RazorPage.Pages.Accounts
{
    public class EditModel : PageModel
    {
        private readonly IAccountService accountService;
        public EditModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        public Account Account { get; set; } = default!;
        [BindProperty]
        [Required(ErrorMessage ="Username is required")]
        public string Username { get; set; } = "";
        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; } = "";
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || accountService == null)
            {
                return NotFound();
            }
            Account? account = await accountService.GetAccountById(id.GetValueOrDefault());
            Role? role = await accountService.GetRole(account.RoleId);
            ViewData["RoleName"] = role.Name;
            ViewData["Username"] = account.Username;
            ViewData["Email"] = account.Email;
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                Account? account = await accountService.GetAccountById(id.GetValueOrDefault());
                account.Username = Username;
                account.Email = Email;
                Account? updatedAccount = await accountService.Update(account);
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
