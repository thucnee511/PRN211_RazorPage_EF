using SE171089_BusinessObjects;
using SE171089_Repositories.BookRepository;
using SE171089_Repositories.CategoryRepository;

namespace SE171089_Services.BookService
{
    public class BookService : IBookService
    {
        private static BookService? instance;
        private readonly ICategoryRepository categoryRepository;
        private readonly IBookRepository bookRepository;
        private BookService()
        {
            categoryRepository = CategoryRepository.Instance;
            bookRepository = BookRepository.Instance;
        }
        public static BookService Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await categoryRepository.GetList();
        }

        public async Task<Book?> GetBookById(int v)
        {
            return await bookRepository.GetOne(v);
        }

        public async Task<List<Book>> GetBooks(int cateId, string? keyword)
        {
            return await bookRepository.SearchBook(cateId, keyword ?? "");
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await categoryRepository.GetOne(id);
        }
    }
}
