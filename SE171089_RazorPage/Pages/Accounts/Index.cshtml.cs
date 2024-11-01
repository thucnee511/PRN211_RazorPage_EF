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
    public class IndexModel : PageModel
    {
        private readonly IAccountService accountService;

        public IndexModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if(accountService != null)
            {
                Account = await accountService.getActiveAccounts();
            }
        }
    }
}
