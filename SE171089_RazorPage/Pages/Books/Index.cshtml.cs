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
    public class IndexModel : PageModel
    {
        private readonly IBookService bookService;

        public IndexModel(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IList<Book> Book { get;set; } = default!;
        public IList<Category> Category { get;set; } = default!;
        public async Task OnGetAsync(int? cateId, string? keyword)
        {
            Category = await bookService.GetAllCategories();
            Book = await bookService.GetBooks(cateId.GetValueOrDefault(), keyword);
            foreach(var item in Book)
            {
                Category category = await bookService.GetCategoryById(item.CateId);
                item.Cate = category;
            }
        }
    }
}
