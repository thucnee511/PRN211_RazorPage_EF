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
    }
}
