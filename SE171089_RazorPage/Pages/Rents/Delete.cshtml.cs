using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SE171089_BusinessObjects;
using SE171089_Services.AccountService;
using SE171089_Services.BookService;
using SE171089_Services.RentService;

namespace SE171089_RazorPage.Pages.Rents
{
    public class DeleteModel : PageModel
    {
        private readonly IRentService rentService;
        private readonly IBookService bookService;
        private readonly IAccountService accountService;

        public DeleteModel(IRentService rentService, IBookService bookService, IAccountService accountService)
        {
            this.rentService = rentService;
            this.bookService = bookService;
            this.accountService = accountService;
        }
        public Rent Rent { get; set; } = default!;
        public List<RentDetail> RentDetails { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Rent rent = await rentService.GetRentById(id.Value);
            if (rent == null)
            {
                return NotFound();
            }
            Rent = rent;
            Account account = await accountService.GetAccountById(rent.UserId);
            rent.User = account;
            RentDetails = await rentService.GetRentDetails(id.Value);
            foreach (RentDetail rentDetail in RentDetails)
            {
                rentDetail.Book = await bookService.GetBookById(rentDetail.BookId.GetValueOrDefault());
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Rent rent = await rentService.GetRentById(id.GetValueOrDefault());

            if (rent == null)
            {
                return NotFound();
            }
            try
            {
                await rentService.Remove(rent);
            }
            catch (Exception e)
            {
                Rent = rent;
                RentDetails = await rentService.GetRentDetails(id.Value);
                ViewData["ErrorMessage"] = e.Message;
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}