using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? cateId, string? accountKeyword, string? bookKeyword)
        {
            Accounts = await accountService.Search(accountKeyword ?? string.Empty);
            Books = await bookService.GetBooks(cateId.GetValueOrDefault(), bookKeyword ?? string.Empty);
            Categories = await bookService.GetAllCategories();
            RentDetails = await GetRentDetail();
            return Page();
        }

        public async Task<IActionResult> OnPostAddRentDetailAsync(int? cateId, string? accountKeyword, string? bookKeyword)
        {
            Accounts = await accountService.Search(accountKeyword ?? string.Empty);
            Books = await bookService.GetBooks(cateId.GetValueOrDefault(), bookKeyword ?? string.Empty);
            Categories = await bookService.GetAllCategories();
            RentDetails = await GetRentDetail();
            try
            {
                RentDetails = await AddRentDetail(RentDetails, new RentDetail
                {
                    BookId = BookId,
                    Quantity = Quantity
                });
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = e.Message;
                return Page();
            }
            await SaveRentDetail(RentDetails);
            return RedirectToPage("./Create");
        }
        public async Task<IActionResult> OnPostRemoveRentDetailAsync(int? cateId, string? accountKeyword, string? bookKeyword, int bookId)
        {
            Accounts = await accountService.Search(accountKeyword ?? string.Empty);
            Books = await bookService.GetBooks(cateId.GetValueOrDefault(), bookKeyword ?? string.Empty);
            Categories = await bookService.GetAllCategories();
            RentDetails = await GetRentDetail();
            RentDetails = await RemoveRentDetail(RentDetails, bookId);
            await SaveRentDetail(RentDetails);
            return RedirectToPage("./Create");
        }

        public async Task<IActionResult> OnPostAsync(int? cateId, string? accountKeyword, string? bookKeyword)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await HttpContext.Session.CommitAsync();
            Accounts = await accountService.Search(accountKeyword ?? string.Empty);
            Books = await bookService.GetBooks(cateId.GetValueOrDefault(), bookKeyword ?? string.Empty);
            Categories = await bookService.GetAllCategories();
            RentDetails = await GetRentDetail();
            int totalQuantity = RentDetails.Sum(rd => rd.Quantity).GetValueOrDefault();
            try
            {
                if (totalQuantity == 0)
                {
                    throw new Exception("No book to rent");
                }
                foreach (var rentDetail in RentDetails)
                {
                    if (rentDetail.Quantity > rentDetail.Book.Quantity)
                    {
                        throw new Exception("Not enough book to rent");
                    }
                    Book book = await bookService.GetBookById(rentDetail.BookId.GetValueOrDefault());
                    book.Quantity -= rentDetail.Quantity;
                    await bookService.Update(book);
                }
                await rentService.RentBook(AccountId, totalQuantity, RentDetails);
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = e.Message;
                return Page();
            }
            HttpContext.Session.Remove("rd");
            return RedirectToPage("./Index");
        }

        private async Task SaveRentDetail(List<RentDetail> rentDetails)
        {
            string rd = "";
            foreach (var rentDetail in rentDetails)
            {
                rd += rentDetail.BookId + ":" + rentDetail.Quantity + ";";
            }
            await HttpContext.Session.CommitAsync();
            HttpContext.Session.SetString("rd", rd);
        }
        private async Task<List<RentDetail>> GetRentDetail()
        {
            await HttpContext.Session.CommitAsync();
            List<RentDetail> rentDetails = new List<RentDetail>();
            string? rd = HttpContext.Session.GetString("rd");
            if (rd != null)
            {
                string[] rdArray = rd.Split(";");
                foreach (var item in rdArray)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        continue;
                    }
                    string[] rdItem = item.Split(":");
                    rentDetails.Add(new RentDetail
                    {
                        BookId = int.Parse(rdItem[0]),
                        Quantity = int.Parse(rdItem[1]),
                        Book = await bookService.GetBookById(int.Parse(rdItem[0]))
                    });
                }
            }
            return rentDetails;
        }
        private async Task<List<RentDetail>> AddRentDetail(List<RentDetail> rentDetails, RentDetail rentDetail)
        {
            foreach (var item in rentDetails)
            {
                if (item.BookId == rentDetail.BookId)
                {
                    if (item.Quantity + rentDetail.Quantity > item.Book.Quantity)
                    {
                        throw new Exception("Not enough book to rent");
                    }
                    item.Quantity += rentDetail.Quantity;
                    return rentDetails;
                }
            }
            rentDetails.Add(rentDetail);
            return rentDetails;
        }
        private async Task<List<RentDetail>> RemoveRentDetail(List<RentDetail> rentDetails, int bookId)
        {
            rentDetails.RemoveAll(rd => rd.BookId == bookId);
            return rentDetails;
        }
    }
}