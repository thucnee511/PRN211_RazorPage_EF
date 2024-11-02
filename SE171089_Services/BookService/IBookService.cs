using SE171089_BusinessObjects;

namespace SE171089_Services.BookService
{
    public interface IBookService
    {
        Task<List<Category>> GetAllCategories();
        Task<List<Book>> GetBooks(int cateId, string? keyword);
        Task<Category?> GetCategoryById(int id);
    }
}
