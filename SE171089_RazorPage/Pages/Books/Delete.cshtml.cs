using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObjects;
using SE171089_Daos;
using SE171089_Services.BookService;

namespace SE171089_RazorPage.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly IBookService bookService;

        public DeleteModel(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [BindProperty]
      public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || bookService == null)
            {
                return NotFound();
            }
            var book = await bookService.GetBookById(id.GetValueOrDefault());
            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                var cate = await bookService.GetCategoryById(book.CateId);
                book.Cate = cate;
                Book = book;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || bookService == null)
            {
                return NotFound();
            }
            var book = await bookService.GetBookById(id.GetValueOrDefault());

            if (book != null)
            {
                try
                {
                    Book = book;
                    await bookService.DeleteBook(Book.Id);
                }catch(Exception e)
                {
                    ViewData["ErrorMessage"] = e.Message;
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
