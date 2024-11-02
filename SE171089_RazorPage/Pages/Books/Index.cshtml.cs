using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObjects;
using SE171089_Daos;

namespace SE171089_RazorPage.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly SE171089_Daos.LibraryManagementContext _context;

        public IndexModel(SE171089_Daos.LibraryManagementContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Book = await _context.Books
                .Include(b => b.Cate).ToListAsync();
            }
        }
    }
}
