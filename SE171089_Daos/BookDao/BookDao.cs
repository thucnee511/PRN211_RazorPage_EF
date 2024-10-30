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
            Book? book = await context.Books.Include(b => b.Cate).OrderByDescending(b => b.Id).FirstOrDefaultAsync();
            return book;
        }

        public async Task<Book?> Delete(int id)
        {
            Book? book = await GetOne(id) ?? throw new KeyNotFoundException("Book not found");
            book.Status = 0;
            book = await Update(book);
            return book;
        }

        public Task<List<Book>> GetActiveBooks()
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooksByAuthor(string author)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooksByCategory(int cateId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooksByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetList()
        {
            List<Book> books = await context.Books.Include(b => b.Cate).ToListAsync();
            return books;
        }

        public async Task<Book?> GetOne(int id)
        {
            Book? book = await context.Books.Include(b => b.Cate).FirstOrDefaultAsync(b => b.Id == id);
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
