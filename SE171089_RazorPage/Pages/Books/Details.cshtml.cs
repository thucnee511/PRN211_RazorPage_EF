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
    public class DetailsModel : PageModel
    {
        private readonly IBookService bookService;

        public DetailsModel(IBookService bookService)
        {
            this.bookService = bookService;
        }

      public Book Book { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || bookService == null)
            {
                return NotFound();
            }

            var book = await bookService.GetBookById(id.GetValueOrDefault());
            var cate = await bookService.GetCategoryById(book.CateId);
            book.Cate = cate;
            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
            }
            return Page();
        }
    }
}
