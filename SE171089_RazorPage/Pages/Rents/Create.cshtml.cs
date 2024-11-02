using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE171089_BusinessObjects;
using SE171089_Services.AccountService;
using SE171089_Services.BookService;
using SE171089_Services.RentService;

namespace SE171089_RazorPage.Pages.Rents
{
    public class CreateModel : PageModel
    {
        private readonly IRentService rentService;
        private readonly IBookService bookService;
        private readonly IAccountService accountService;

        public CreateModel(IRentService rentService, IBookService bookService, IAccountService accountService)
        {
            this.rentService = rentService;
            this.bookService = bookService;
            this.accountService = accountService;
        }

        public List<Account> Accounts { get; set; } = default!;
        public List<Book> Books { get; set; } = default!;
        public List<Category> Categories { get; set; } = default!;
        public List<RentDetail> RentDetails { get; set; } = default!;

        [BindProperty]
        public int AccountId { get; set; }
        [BindProperty]
        public int BookId { get; set; }
        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? cateId, string? accountKeyword, string? bookKeyword)
        {
            Accounts = await accountService.Search(accountKeyword ?? string.Empty);
            Books = await bookService.GetBooks(cateId.GetValueOrDefault(), bookKeyword ?? string.Empty);
            Categories = await bookService.GetAllCategories();
            RentDetails = new List<RentDetail>();
            return Page();
        }

        public async Task<IActionResult> OnPostAddRentDetailAsync()
        {
            RentDetails ??= new List<RentDetail>();
            RentDetails.Add(new RentDetail
            {
                BookId = BookId,
                Quantity = Quantity,
                Book = await bookService.GetBookById(BookId)
            });
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Rent rent = new Rent
            {
                UserId = AccountId,
                RentTime = DateTime.Now,
                Status = "renting",
                RentDetails = RentDetails
            };
            await rentService.RentBook(AccountId, RentDetails.Sum(rd => rd.Quantity).GetValueOrDefault(), RentDetails);
            return RedirectToPage("./Index");
        }
    }
}