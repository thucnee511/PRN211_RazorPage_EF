using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE171089_BusinessObjects;
using SE171089_Services.AccountService;
using System.ComponentModel.DataAnnotations;

namespace SE171089_RazorPage.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService accountService;
        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = string.Empty;
        public LoginModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Account? account = await accountService.Login(Email, Password);
                    await HttpContext.Session.CommitAsync();
                    HttpContext.Session.SetString("Username", account.Username);
                    HttpContext.Session.SetString("RoleId", account.RoleId.ToString());
                    return RedirectToPage("/Index");
                }
                catch (Exception e)
                {
                    ViewData["Error"] = e.Message;
                    return Page();
                }
            }
            ViewData["Error"] = "Invalid input";
            return Page();
        }
    }
}
