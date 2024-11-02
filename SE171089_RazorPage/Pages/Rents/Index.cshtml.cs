using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE171089_BusinessObjects;
using SE171089_Services.AccountService;
using SE171089_Services.BookService;
using SE171089_Services.RentService;

namespace SE171089_RazorPage.Pages.Rents
{
    public class IndexModel : PageModel
    {
        private readonly IRentService rentService;
        private readonly IAccountService accountService;
        private readonly IBookService bookService;
        public IndexModel(IRentService rentService, IAccountService accountService, IBookService bookService)
        {
            this.rentService = rentService;
            this.accountService = accountService;
            this.bookService = bookService;
        }

        public List<Rent> Rent { get; set; }
        public List<Account> Account { get; set; }

        public async Task<IActionResult> OnGetAsync(string? keyword, DateTime? fromDate, DateTime? toDate)
        {
            if(fromDate != null) fromDate = fromDate.Value.Date.At(0, 0, 0, 0);
            if(toDate != null) toDate = toDate.Value.Date.At(23, 59, 59);
            Account = await accountService.Search(keyword ?? "");
            Rent = new();
            foreach (Account account in Account)
            {
                Rent.AddRange(await rentService.GetRentsByAccount(account.Id));
            }
            if (fromDate != null){
                Rent = Rent.Where(rent => rent.RentTime >= fromDate).ToList();
            }
            if (toDate != null){
                Rent = Rent.Where(rent => rent.RentTime <= toDate).ToList();
            }
            foreach (Rent rent in Rent)
            {
                rent.User = await accountService.GetAccountById(rent.UserId);
            }
            return Page();
        }
    }
}
