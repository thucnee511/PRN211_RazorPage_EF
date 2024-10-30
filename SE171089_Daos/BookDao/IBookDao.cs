using SE171089_BusinessObjects;

namespace SE171089_Daos.BookDao
{
    public interface IBookDao : IDao<Book>
    {
        Task<List<Book>> GetActiveBooks();
        Task<List<Book>> GetBooksByAuthor(string author);
        Task<List<Book>> GetBooksByName(string name);
        Task<List<Book>> GetBooksByCategory(int cateId);
    }
}
