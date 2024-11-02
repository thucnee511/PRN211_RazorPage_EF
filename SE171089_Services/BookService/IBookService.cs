using SE171089_BusinessObjects;

namespace SE171089_Services.BookService
{
    public interface IBookService
    {
        Task<Book?> DeleteBook(int id);
        Task<List<Category>> GetAllCategories();
        Task<Book?> GetBookById(int v);
        Task<List<Book>> GetBooks(int cateId, string? keyword);
        Task<Category?> GetCategoryById(int id);
        Task<Book?> Update(Book book);
    }
}
