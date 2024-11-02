using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObjects;

namespace SE171089_Daos.BookDao
{
    public class BookDao : IBookDao
    {
        private static BookDao? instance;
        private readonly LibraryManagementContext context;
        private BookDao()
        {
            context = new LibraryManagementContext();
        }
        public static BookDao Instance
        {
            get
            {
                instance ??= new BookDao();
                return instance;
            }
        }

        public async Task<Book?> Add(Book obj)
        {
            context.Books.Add(obj);
            await context.SaveChangesAsync();
            Book? book = await context.Books.OrderByDescending(b => b.Id).FirstOrDefaultAsync();
            return book;
        }

        public async Task<Book?> Delete(int id)
        {
            Book? book = await GetOne(id) ?? throw new KeyNotFoundException("Book not found");
            book.Status = 0;
            book = await Update(book);
            return book;
        }

        public async Task<List<Book>> GetActiveBooks()
        {
            List<Book> books = await context.Books.Where(b => b.Status == 1).ToListAsync();
            return books;
        }

        public async Task<List<Book>> GetBooksByAuthor(string author)
        {
            return await context.Books
                .Where(b => b.Author.ToLower().Contains(author.ToLower()))
                .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByCategory(int cateId)
        {
            return await context.Books
                .Where(b => b.CateId == cateId)
                .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByName(string name)
        {
            return await context.Books
                .Where(b => b.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<List<Book>> GetList()
        {
            List<Book> books = await context.Books.ToListAsync();
            return books;
        }

        public async Task<Book?> GetOne(int id)
        {
            Book? book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            return book;
        }

        public async Task<Book?> Update(Book obj)
        {
            context.Books.Update(obj);
            await context.SaveChangesAsync();
            Book? book = await GetOne(obj.Id);
            return book;
        }
    }
}
