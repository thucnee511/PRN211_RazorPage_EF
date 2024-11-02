using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SE171089_BusinessObjects;
using SE171089_Daos;
using SE171089_Services.AccountService;

namespace SE171089_RazorPage.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService accountService;

        public CreateModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Role? role = await accountService.GetRole(3);
            ViewData["RoleName"] = role.Name ?? "User";
            return Page();
        }

        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = "";
        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; } = "";
        
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [BindProperty] 
        public string Password { get; set; } = "";
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || accountService == null)
            {
                return Page();
            }
            Account account = new Account
            {
                Username = Username,
                Email = Email,
                Password = Password,
                RoleId = 3
            };
            try
            {
                await accountService.Insert(account);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Insert failed: {ex.Message}";
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
