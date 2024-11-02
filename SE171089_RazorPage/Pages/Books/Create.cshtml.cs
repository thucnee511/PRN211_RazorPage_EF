using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SE171089_BusinessObjects;
using SE171089_Daos;
using SE171089_Services.BookService;

namespace SE171089_RazorPage.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IBookService bookService;

        public CreateModel(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public List<Category> Categories { get; set; } = new();
        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await bookService.GetAllCategories();
            return Page();
        }

        [BindProperty]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        [BindProperty]
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [BindProperty]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
        [BindProperty]
        public int CateId { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || bookService == null)
            {
                return Page();
            }
            Categories = await bookService.GetAllCategories();
            Book book = new()
            {
                Name = Name,
                Author = Author,
                Description = Description,
                Quantity = Quantity,
                CateId = CateId
            };
            try
            {
                book = await bookService.Create(book);
            }
            catch(Exception e)
            {
                ViewData["ErrorMessage"] = e.Message;
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
