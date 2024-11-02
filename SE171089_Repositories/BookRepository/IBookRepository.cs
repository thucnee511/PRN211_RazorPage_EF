using SE171089_BusinessObjects;

namespace SE171089_Repositories.BookRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> GetActiveBooks();
        Task<List<Book>> GetBooksByAuthor(string author);
        Task<List<Book>> GetBooksByName(string name);
        Task<List<Book>> GetBooksByCategory(int cateId);
        Task<List<Book>> SearchBook(int cateId, string keyword);
    }
}
