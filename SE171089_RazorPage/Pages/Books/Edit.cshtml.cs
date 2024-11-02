using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObjects;
using SE171089_Services.BookService;
using System.ComponentModel.DataAnnotations;

namespace SE171089_RazorPage.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IBookService bookService;

        public EditModel(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public Book Book { get; set; } = default!;
        public List<Category> Categories { get; set; } = default!;
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
            Book = book;
            Categories = await bookService.GetAllCategories();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                Book = await bookService.GetBookById(id.GetValueOrDefault());
                Book.Name = Name;
                Book.Author = Author;
                Book.Description = Description;
                Book.Quantity = Quantity;
                Book.CateId = CateId;
                await bookService.Create(Book);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Update failed: {ex.Message}";
            }

            return RedirectToPage("./Index");
        }
    }
}
