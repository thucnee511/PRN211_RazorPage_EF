using SE171089_BusinessObjects;
using SE171089_Daos.BookDao;

namespace SE171089_Repositories.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private static BookRepository? instance;
        private readonly IBookDao bookDao;
        private BookRepository()
        {
            bookDao = BookDao.Instance;
        }
        public static BookRepository Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Book?> Add(Book obj)
        {
            return await bookDao.Add(obj);
        }

        public async Task<Book?> Delete(int id)
        {
            return await bookDao.Delete(id);
        }

        public async Task<List<Book>> GetActiveBooks()
        {
            return await bookDao.GetActiveBooks();
        }

        public async Task<List<Book>> GetBooksByAuthor(string author)
        {
            return await bookDao.GetBooksByAuthor(author);
        }

        public async Task<List<Book>> GetBooksByCategory(int cateId)
        {
            return await bookDao.GetBooksByCategory(cateId);
        }

        public async Task<List<Book>> GetBooksByName(string name)
        {
            return await bookDao.GetBooksByName(name);
        }

        public async Task<List<Book>> GetList()
        {
            return await bookDao.GetList();
        }

        public async Task<Book?> GetOne(int id)
        {
            return await bookDao.GetOne(id);
        }

        public async Task<List<Book>> SearchBook(int cateId, string keyword)
        {
            return await bookDao.SearchBook(cateId, keyword);
        }

        public async Task<Book?> Update(Book obj)
        {
            return await bookDao.Update(obj);
        }
    }
}
