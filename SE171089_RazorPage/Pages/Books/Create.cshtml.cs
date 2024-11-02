using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SE171089_BusinessObjects;
using SE171089_Daos;

namespace SE171089_RazorPage.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly SE171089_Daos.LibraryManagementContext _context;

        public CreateModel(SE171089_Daos.LibraryManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CateId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Books == null || Book == null)
            {
                return Page();
            }

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
